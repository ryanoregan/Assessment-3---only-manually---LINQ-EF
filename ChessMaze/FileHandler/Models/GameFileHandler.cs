using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Windows.Forms;
using ChessMaze;
using System.Diagnostics;

namespace ChessMaze.FileHandler
{
    public static class GameFileHandler
    {
        private const string DefaultSaveFileName = "game_save.json";

        public static bool SaveGame(Game game, int currentLevel)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "JSON files (*.json)|*.json";
                saveFileDialog.Title = "Save Game";
                saveFileDialog.FileName = DefaultSaveFileName;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    var gameState = new GameState
                    {
                        Board = new List<PieceState>(),
                        MoveHistory = new List<MoveState>(),
                        CurrentLevel = currentLevel
                    };

                    if (game.CurrentLevel?.Board != null)
                    {
                        for (int row = 0; row < game.CurrentLevel.Board.Rows; row++)
                        {
                            for (int col = 0; col < game.CurrentLevel.Board.Columns; col++)
                            {
                                var piece = game.CurrentLevel.Board.GetPiece(new Position(row, col));
                                if (piece != null)
                                {
                                    gameState.Board.Add(new PieceState
                                    {
                                        Type = piece.Type.ToString(),
                                        Position = new Position(row, col)
                                    });
                                }
                            }
                        }
                    }

                    foreach (var move in game.moveHistory)
                    {
                        gameState.MoveHistory.Add(new MoveState(
                            new Position(move.From.Row, move.From.Column),
                            new Position(move.To.Row, move.To.Column)
                        ));
                    }

                    var json = JsonSerializer.Serialize(gameState, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(filePath, json);

                    return true; // Indicate successful save
                }
            }
            return false; // Indicate that the save was canceled or not successful
        }

        // Overloaded LoadGame method that uses a file dialog
        public static bool LoadGame(Game game, out int loadedLevel)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON files (*.json)|*.json";
                openFileDialog.Title = "Load Game";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    LoadGame(game, filePath, out loadedLevel);
                    return true;
                }
                else
                {
                    loadedLevel = 1; // Default if no file selected
                    return false;
                }
            }
        }

        // Main LoadGame method with filePath and loadedLevel parameters
        public static void LoadGame(Game game, string filePath, out int loadedLevel)
        {
            loadedLevel = 1;

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Save file not found", filePath);
            }

            var json = File.ReadAllText(filePath);
            var gameState = JsonSerializer.Deserialize<GameState>(json);

            if (gameState != null && game.CurrentLevel?.Board != null)
            {
                loadedLevel = gameState.CurrentLevel;

                for (int row = 0; row < game.CurrentLevel.Board.Rows; row++)
                {
                    for (int col = 0; col < game.CurrentLevel.Board.Columns; col++)
                    {
                        game.CurrentLevel.Board.RemovePiece(new Position(row, col));
                    }
                }

                foreach (var pieceState in gameState.Board)
                {
                    var pieceType = Enum.Parse<PieceType>(pieceState.Type);
                    var position = new Position(pieceState.Position.Row, pieceState.Position.Column);
                    game.CurrentLevel.Board.PlacePiece(new Piece(pieceType), position);
                }

                game.moveHistory.Clear();
                foreach (var moveState in gameState.MoveHistory)
                {
                    var fromPosition = new Position(moveState.From.Row, moveState.From.Column);
                    var toPosition = new Position(moveState.To.Row, moveState.To.Column);
                    game.moveHistory.Push(new Move(fromPosition, toPosition));
                }

                if (game.moveHistory.Count > 0)
                {
                    var lastMove = game.moveHistory.Peek();
                    game.CurrentLevel.Player.CurrentPosition = lastMove.To;
                }
                else
                {
                    game.CurrentLevel.Player.CurrentPosition = game.CurrentLevel.StartPosition;
                }
            }
        }
    }
}
