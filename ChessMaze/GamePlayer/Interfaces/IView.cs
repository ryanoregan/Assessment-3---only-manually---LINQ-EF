using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ChessMaze;

namespace ChessMaze
{
    public interface IView
    {

        // Game Control Methods
        void SaveGame(object sender, EventArgs e);
        void LoadGame(object sender, EventArgs e);

        // UI Update Methods
        void UpdateMoveCount();
        void UpdateLevelCounter();
        void RefreshBoard();
        void ShowCurrentPosition(IPosition currentPosition);
        void HighlightValidMoves(List<Position> validMoves);

        // Event Handlers
        void GameForm_Resize(object sender, EventArgs e);
        void Box_Paint(object sender, PaintEventArgs e);
        void OnGridCellClick(object sender, EventArgs e);
    }
}
