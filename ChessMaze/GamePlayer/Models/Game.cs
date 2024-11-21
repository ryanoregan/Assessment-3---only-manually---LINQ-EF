using System.Collections.Generic;
using System.Diagnostics;

namespace ChessMaze
{
    public class Game : IGame  // The Game class implements the IGame interface
    {
        public ILevel? CurrentLevel { get; private set; }  // Property for the current level of the game
        public Stack<Move> moveHistory;  // Stack to keep track of the history of moves
        private bool isGameInProgress;
        public int currentLevelNumber;
        private int levelsCompleted;
        private DateTime startTime;
        private DateTime endTime;
        public List<ILevel> levels;  // List to store the levels

        public int MoveCount => moveHistory.Count;

        public bool IsGameOver
        {
            get
            {
                return CurrentLevel?.IsCompleted ?? false;
            }
        } // Return true if the current level is completed

        // Constructor to initialize a new Game instance
        public Game(List<ILevel> levels)
        {
            moveHistory = new Stack<Move>();  // Initialize the moveHistory stack
            isGameInProgress = false; // reset the game progress flag
            currentLevelNumber = 1;
            levelsCompleted = 0;
            startTime = DateTime.MinValue; // Initialize startTime
            this.levels = levels;
            
        }

        // Method to load a level into the game
        public void LoadLevel(ILevel level)
        {
            CurrentLevel = level;  // Set the current level
            moveHistory.Clear();  // Clear the move history
            isGameInProgress = true;
            startTime = DateTime.Now; // Set startTime to the current time
            CurrentLevel.Player.CurrentPosition = CurrentLevel.StartPosition; // Reset the player's position to the start position 
            CurrentLevel.Board.PlacePiece(new Piece(CurrentLevel.Player.PieceType), CurrentLevel.StartPosition);
            // Place the player piece on the board
        }

        // Method to make a move to a new position
        public bool MakeMove(IPosition newPosition)
        {
            if (CurrentLevel == null)
            {
                throw new InvalidOperationException("No level is currently loaded.");
            }

            // Check if the player can move to the new position
            bool canMove = CurrentLevel.Player.CanMove(newPosition, CurrentLevel.Board);

            if (canMove)
            {
                // Save the current position before making the move
                IPosition currentPosition = CurrentLevel.Player.CurrentPosition;
                moveHistory.Push(new Move(currentPosition, newPosition));  // Push the move to the move history

                // Make the move
                CurrentLevel.Player.Move(newPosition, CurrentLevel.Board);

                // Check for level completion
                if (newPosition.Equals(CurrentLevel.EndPosition))
                {
                    
                    LoadNextLevel(); // Load the next level
                }

                return true;  // Return true if the move was successful
            }

            return false;  // Return false if the move was not successful
        }


        public bool ValidateMove(IPosition newPosition)
        {
            return CurrentLevel?.Player.CanMove(newPosition, CurrentLevel.Board) == true;
        }

        // Method to get the count of moves made
        public int GetMoveCount()
        {
            return moveHistory.Count;  // Return the count of moves in the move history
        }

        // Method to undo the last move
        public string? Undo()
    {
        if (moveHistory.Count > 0)
        {
             if (CurrentLevel == null)
            {
             throw new InvalidOperationException("No level is currently loaded.");
            }
            Move lastMove = moveHistory.Pop();  // Pop the last move from the move history
            CurrentLevel.Player.Move(lastMove.From, CurrentLevel.Board);  // Move the player back to the previous position
            return null;
        }
        else
        {
            return "Error: No moves to undo.";
        }
    }

        public void StartNewGame(ILevel level)
    {
        if (isGameInProgress)
        {
            Console.WriteLine("Warning: Starting a new game will reset the current game.");
        }
         currentLevelNumber = 1;
         levelsCompleted = 0;
         startTime = DateTime.Now; // Set startTime to the current time
        LoadLevel(level);
    }

      private void LoadNextLevel()
        {
            currentLevelNumber++;
            levelsCompleted++;
            if (currentLevelNumber <= levels.Count)
            {
                LoadLevel(levels[currentLevelNumber - 1]);
            }
            else
            {
                Console.WriteLine("Congratulations! You have completed all levels.");
                isGameInProgress = false;
                endTime = DateTime.Now;
            }
        }

        public string ShowTracer()
        {
             if (CurrentLevel == null)
            {
                throw new InvalidOperationException("No level is currently loaded.");
            }   
            return CurrentLevel.Player.ShowTracer();
        }

        public List<Position> ShowPossibleMoves()
        {
             if (CurrentLevel == null)
            {
                throw new InvalidOperationException("No level is currently loaded.");
            }
            return CurrentLevel.Board.ShowPossibleMoves(CurrentLevel.Player);
        }


        public string DisplayStatistics()
    {
        if (!isGameInProgress && levelsCompleted == 0)
        {
            return "No statistics are available.";
        }

        var timeTaken = isGameInProgress ? DateTime.Now - startTime : endTime - startTime;
        return $"Moves Made: {moveHistory.Count}\nTime Taken: {timeTaken}\nLevels Completed: {levelsCompleted}";
    }

        // Method to restart the game
        public void Restart()
        {
            if (CurrentLevel == null)
        {
            throw new InvalidOperationException("No level is currently loaded.");
        }
            LoadLevel(CurrentLevel);  // Reload the current level
        }

        // Method to display the game instructions
        public string DisplayInstructions()
        {
            return "Instructions for ChessMaze:\n" +
                   "1. The game is played on a chessboard with special rules.\n" +
                   "2. The player starts as a specific chess piece and can move according to the rules of that piece.\n" +
                   "3. When a player lands on a square occupied by another piece, the player becomes that piece.\n" +
                   "4. The goal of the game is to reach a specific position on the board.\n" +
                   "5. The game ends when the goal is reached or there are no valid moves left.";
        }
    }
}
