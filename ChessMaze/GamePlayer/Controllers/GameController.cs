using ChessMaze.FileHandler;
using ChessMaze;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace ChessMaze
{
    public class GameController : IDisposable
    {
        private readonly Game _game;
        private int _levelCount;
        private int _moveCount;
        private readonly ApplicationDbContext _context;
        private Stack<Move> MoveHistory = new Stack<Move>();

        public GameController(string filePath = null, ApplicationDbContext context = null)
        {
            _context = context ?? new ApplicationDbContext();  // Initialize context, allowing for DI or default instance

            // Initialize rules, board, player, and levels
            var rules = new Rules();
            var board = new Board(8, 8, rules);
            IPosition startPosition = new Position(0, 0);
            IPosition endPosition = new Position(7, 7);
            IPlayer player = new Player(startPosition, PieceType.Rook, rules);

            List<ILevel> levels = new List<ILevel>
            {
                new Level(board, startPosition, endPosition, player)
            };

            _game = new Game(levels);
            _levelCount = 1;
            _moveCount = 0;

            // Start the first level
            _game.StartNewGame(levels[0]);

            // Optionally load game state from file
            if (!string.IsNullOrEmpty(filePath))
            {
                LoadGameFromFile(filePath);
            }
        }

        public Game GetGame()
        {
            return _game;
        }

        public void InitializeGame(string filePath = null)
        {
            if (filePath != null)
            {
                int loadedLevel;
                GameFileHandler.LoadGame(_game, filePath, out loadedLevel);
                _levelCount = loadedLevel;
            }
            else if (_game.levels.Count > 0)
            {
                _game.StartNewGame(_game.levels[0]);
                _levelCount = 1;
                _moveCount = 0;
            }
        }

        public void LoadGameFromFile(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var gameState = JsonSerializer.Deserialize<GameState>(json);

            if (_game.CurrentLevel?.Board != null && gameState != null)
            {
                // Clear the board
                for (int row = 0; row < _game.CurrentLevel.Board.Rows; row++)
                {
                    for (int col = 0; col < _game.CurrentLevel.Board.Columns; col++)
                    {
                        _game.CurrentLevel.Board.RemovePiece(new Position(row, col));
                    }
                }

                // Place pieces from the game state
                foreach (var pieceState in gameState.Board)
                {
                    var pieceType = Enum.Parse<PieceType>(pieceState.Type);
                    var position = new Position(pieceState.Position.Row, pieceState.Position.Column);
                    _game.CurrentLevel.Board.PlacePiece(new Piece(pieceType), position);
                }
            }
        }

        public bool TryMakeMove(Position targetPosition, out bool isLevelComplete)
        {
            isLevelComplete = false;
            bool isMoveSuccess = _game.MakeMove(targetPosition);

            if (isMoveSuccess)
            {
                _moveCount++;
                var player = _game.CurrentLevel.Player;

                // Cast CurrentPosition to Position if needed
                RecordMovement(DefaultPlayerId, player.PieceType, (Position)player.CurrentPosition, targetPosition);

                if (targetPosition.Equals(_game.CurrentLevel.EndPosition))
                {
                    isLevelComplete = true;
                    _levelCount++;
                }
            }
            return isMoveSuccess;
        }




        private const int DefaultPlayerId = 1;  // Default ID for the single player

        private void RecordMovement(int playerId, PieceType pieceType, Position fromPosition, Position toPosition)
        {
            var movement = new Movement
            {
                PlayerId = playerId,
                PieceMoved = pieceType.ToString(),
                FromPosition = $"{fromPosition.Row},{fromPosition.Column}",
                ToPosition = $"{toPosition.Row},{toPosition.Column}",
                Timestamp = DateTime.Now
            };

            _context.Movements.Add(movement);
            _context.SaveChanges();
        }



        public (Position? newPosition, int newMoveCount, string? errorMessage) Undo()
        {
            if (_game.moveHistory.Count > 0)
            {
                if (_game.CurrentLevel == null)
                {
                    throw new InvalidOperationException("No level is currently loaded.");
                }

                Move lastMove = _game.moveHistory.Pop();  // Pop the last move from the move history
                _game.CurrentLevel.Player.Move(lastMove.From, _game.CurrentLevel.Board);  // Move the player back to the previous position
                _moveCount--;  // Decrement move count after undo
                return ((Position)_game.CurrentLevel.Player.CurrentPosition, _moveCount, null);
            }
            else
            {
                return (null, _moveCount, "Error: No moves to undo.");
            }
        }

        public List<Position> GetValidMoves(Position currentPosition)
        {
            var piece = _game.CurrentLevel?.Board.GetPiece(currentPosition);
            return piece != null ? Rules.GetValidMoves(piece, currentPosition, _game.CurrentLevel.Board) : new List<Position>();
        }

        public void LoadNextLevel()
        {
            var nextLevelPath = $@"..\..\..\Level_{_levelCount}.json";

            if (File.Exists(nextLevelPath))
            {
                var json = File.ReadAllText(nextLevelPath);
                var gameState = JsonSerializer.Deserialize<GameState>(json);

                if (_game.CurrentLevel?.Board != null && gameState != null)
                {
                    for (int row = 0; row < _game.CurrentLevel.Board.Rows; row++)
                    {
                        for (int col = 0; col < _game.CurrentLevel.Board.Columns; col++)
                        {
                            _game.CurrentLevel.Board.RemovePiece(new Position(row, col));
                        }
                    }

                    foreach (var pieceState in gameState.Board)
                    {
                        var pieceType = Enum.Parse<PieceType>(pieceState.Type);
                        var position = new Position(pieceState.Position.Row, pieceState.Position.Column);
                        _game.CurrentLevel.Board.PlacePiece(new Piece(pieceType), position);
                    }

                    _game.CurrentLevel.Player.CurrentPosition = gameState.MoveHistory.Count > 0
                        ? gameState.MoveHistory[^1].To
                        : _game.CurrentLevel.StartPosition;

                    _moveCount = 0;
                    _game.moveHistory.Clear();
                }
            }
        }

        public bool IsValidMove(Position targetPosition, Position currentPosition)
        {
            var validMoves = GetValidMoves(currentPosition);
            return validMoves.Contains(targetPosition);
        }

        public Position GetPlayerPosition()
        {
            return (Position)_game.CurrentLevel?.Player.CurrentPosition ?? new Position(0, 0);
        }

        public IPiece GetPieceAtPosition(Position position)
        {
            return _game.CurrentLevel?.Board.GetPiece(position);
        }

        public int MoveCount => _moveCount;
        public int LevelCount => _levelCount;

        public bool SaveGame()
        {
            return GameFileHandler.SaveGame(_game, _levelCount);
        }

        public bool LoadGame()
        {
            int loadedLevel;
            bool result = GameFileHandler.LoadGame(_game, out loadedLevel);
            _levelCount = loadedLevel;
            _moveCount = _game.MoveCount;
            return result;
        }

        public void ResetMoveCount()
        {
            _moveCount = 0;
        }

        // Retrieve recent movements
        public List<Movement> GetRecentMovements(int count = 10)
        {
            return _context.Movements
                .OrderByDescending(m => m.Timestamp)
                .Take(count)
                .ToList();
        }

        // Implement IDisposable to dispose of the DbContext
        public void Dispose()
        {
            _context?.Dispose();
        }

        public void ClearMovementData()
        {
            _context.Movements.RemoveRange(_context.Movements); // Delete all rows
            _context.SaveChanges(); // Commit changes to the database
        }

        public void RemoveLastMovement()
        {
            var lastMovement = _context.Movements
                .OrderByDescending(m => m.Timestamp)
                .FirstOrDefault();

            if (lastMovement != null)
            {
                _context.Movements.Remove(lastMovement);
                _context.SaveChanges();
            }
        }


    }
}