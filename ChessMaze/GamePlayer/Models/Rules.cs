using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChessMaze
{
    public class Rules
    {
        // Method to get the valid moves for a piece at a given position on the board
        public static List<Position> GetValidMoves(IPiece piece, IPosition position, IBoard board)
        {
            List<Position> validMoves = new List<Position>();  // List to store the valid moves

            switch (piece.Type)
            {
                case PieceType.King:
                    GetKingMoves(validMoves, position, board);
                    break;
                case PieceType.Rook:
                    GetRookMoves(validMoves, position, board);
                    break;
                case PieceType.Bishop:
                    GetBishopMoves(validMoves, position, board);
                    break;
                case PieceType.Knight:
                    GetKnightMoves(validMoves, position, board);
                    break;
                case PieceType.Pawn:
                    GetPawnMoves(validMoves, position, board);
                    break;
                default:
                    break;
            }

            return validMoves;  // Return the list of valid moves
        }

        private static void GetKingMoves(List<Position> validMoves, IPosition position, IBoard board)
        {
            for (int row = -1; row <= 1; row++)
            {
                for (int col = -1; col <= 1; col++)
                {
                    if (row == 0 && col == 0) continue; // Skip the King's current position
                    int newRow = position.Row + row;
                    int newCol = position.Column + col;
                    if (IsInBounds(newRow, newCol, board) && board.GetPiece(new Position(newRow, newCol)) != null)
                    {
                        validMoves.Add(new Position(newRow, newCol));
                    }
                }
            }
        }

        private static void GetRookMoves(List<Position> validMoves, IPosition position, IBoard board)
        {
            // Define directions for rook movement (vertical and horizontal)
            int[] rowDirections = { 1, -1, 0, 0 };
            int[] colDirections = { 0, 0, 1, -1 };

            // Iterate over each direction
            for (int i = 0; i < rowDirections.Length; i++)
            {
                for (int step = 1; step < Math.Max(board.Rows, board.Columns); step++)
                {
                    int newRow = position.Row + step * rowDirections[i];
                    int newCol = position.Column + step * colDirections[i];

                    // Stop if the new position is out of bounds
                    if (!IsInBounds(newRow, newCol, board)) break;

                    var targetPosition = new Position(newRow, newCol);
                    var targetPiece = board.GetPiece(targetPosition);

                    // Only add if there's a piece at the target position
                    if (targetPiece != null)
                    {
                        validMoves.Add(targetPosition);
                        break; // Stop after encountering the first piece
                    }
                }
            }
        }


        private static void GetBishopMoves(List<Position> validMoves, IPosition position, IBoard board)
        {
            // Diagonal moves
            int[] rowDirections = { 1, 1, -1, -1 };
            int[] colDirections = { 1, -1, 1, -1 };

            for (int i = 0; i < rowDirections.Length; i++)
            {
                for (int step = 1; step < Math.Max(board.Rows, board.Columns); step++)
                {
                    int newRow = position.Row + step * rowDirections[i];
                    int newCol = position.Column + step * colDirections[i];
                    if (!IsInBounds(newRow, newCol, board)) break;

                    if (board.GetPiece(new Position(newRow, newCol)) != null)
                    {
                        validMoves.Add(new Position(newRow, newCol));
                        break; // Stop once an occupied space is reached
                    }
                }
            }
        }

        private static void GetKnightMoves(List<Position> validMoves, IPosition position, IBoard board)
        {
            int[] rowOffsets = { -2, -1, 1, 2, -2, -1, 1, 2 };
            int[] colOffsets = { -1, -2, -2, -1, 1, 2, 2, 1 };

            for (int i = 0; i < rowOffsets.Length; i++)
            {
                int newRow = position.Row + rowOffsets[i];
                int newCol = position.Column + colOffsets[i];
                if (IsInBounds(newRow, newCol, board) && board.GetPiece(new Position(newRow, newCol)) != null)
                {
                    validMoves.Add(new Position(newRow, newCol));
                }
            }
        }

        private static void GetPawnMoves(List<Position> validMoves, IPosition position, IBoard board)
        {
            int forwardRow = position.Row + 1;
            // Only allow capturing diagonally if an opponent piece is present
            CaptureDiagonally(validMoves, position, forwardRow, board);
        }

        private static void CaptureDiagonally(List<Position> validMoves, IPosition position, int forwardRow, IBoard board)
        {
            if (position.Column - 1 >= 0 && board.GetPiece(new Position(forwardRow, position.Column - 1)) != null)
            {
                validMoves.Add(new Position(forwardRow, position.Column - 1));
            }
            if (position.Column + 1 < board.Columns && board.GetPiece(new Position(forwardRow, position.Column + 1)) != null)
            {
                validMoves.Add(new Position(forwardRow, position.Column + 1));
            }
        }

        private static bool IsInBounds(int row, int col, IBoard board)
        {
            return row >= 0 && row < board.Rows && col >= 0 && col < board.Columns;
        }
    }
}
