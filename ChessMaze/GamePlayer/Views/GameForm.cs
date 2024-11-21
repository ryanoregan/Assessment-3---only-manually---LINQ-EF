using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ChessMaze;

namespace ChessMaze
{
    public partial class GameForm : Form, IView
    {
        private readonly GameController _controller;

        public GameForm(GameController controller)
        {
            InitializeComponent();
            _controller = controller;
            this.Resize += GameForm_Resize;

            UpdateLevelCounter();
            RefreshBoard();
            ShowCurrentPosition(_controller.GetPlayerPosition());
            DisplayRecentMoves(); // Load recent moves at startup
        }


        // Method to save the game
        public void SaveGame(object sender, EventArgs e)
        {
            bool saveSuccessful = _controller.SaveGame();
            if (saveSuccessful)
            {
                MessageBox.Show("Game saved successfully!");
            }
            else
            {
                MessageBox.Show("Save Cancelled.");
            }
        }

        // Method to load the game
        public void LoadGame(object sender, EventArgs e)
        {
            bool loadSuccessful = _controller.LoadGame();
            UpdateMoveCount();
            UpdateLevelCounter();
            RefreshBoard();
            ShowCurrentPosition(_controller.GetPlayerPosition());

            if (loadSuccessful)
            {
                MessageBox.Show("Game loaded successfully!");
            }
            else
            {
                MessageBox.Show("load Cancelled.");
            }
        }


        // Resizes the form, adjusting the board size
        public void GameForm_Resize(object sender, EventArgs e)
        {
            int topReservedSpace = 50;
            int bottomReservedSpace = 100;

            int availableHeight = this.ClientSize.Height - topReservedSpace - bottomReservedSpace;
            int availableWidth = this.ClientSize.Width;

            int squareSize = Math.Min(availableWidth, availableHeight);
            int cellSize = squareSize / 8;

            _boardPanel.Size = new Size(squareSize, squareSize);
            _boardPanel.Location = new Point(
                (this.ClientSize.Width - squareSize) / 2,
                topReservedSpace + (availableHeight - squareSize) / 2
            );

            foreach (Control control in _boardPanel.Controls)
            {
                if (control is PictureBox box)
                {
                    box.Width = cellSize;
                    box.Height = cellSize;

                    var position = box.Name.Split('_');
                    int row = int.Parse(position[1]);
                    int col = int.Parse(position[2]);
                    box.Location = new Point(col * cellSize, row * cellSize);
                }
            }
        }

        // Paints the cells based on specific conditions
        public void Box_Paint(object sender, PaintEventArgs e)
        {
            if (sender is PictureBox box)
            {
                var position = box.Name.Split('_');
                int row = int.Parse(position[1]);
                int col = int.Parse(position[2]);

                if (box.Tag is bool && (bool)box.Tag)
                {
                    using (Pen pen = new Pen(Color.LightGreen, 3))
                    {
                        e.Graphics.DrawRectangle(pen, 1, 1, box.Width - 3, box.Height - 3);
                    }
                }
                else if (box.Tag is string && (string)box.Tag == "currentPosition")
                {
                    using (Pen pen = new Pen(Color.Orange, 3))
                    {
                        e.Graphics.DrawRectangle(pen, 1, 1, box.Width - 3, box.Height - 3);
                    }
                }
                else if (row == 7 && col == 7)
                {
                    using (Pen pen = new Pen(Color.Red, 3))
                    {
                        e.Graphics.DrawRectangle(pen, 1, 1, box.Width - 3, box.Height - 3);
                    }
                }
            }
        }

        // Handles cell clicks, checking for valid moves and refreshing UI accordingly
        public void OnGridCellClick(object sender, EventArgs e)
        {
            if (sender is PictureBox box)
            {
                int index = _boardPanel.Controls.IndexOf(box);
                int row = index / 8;
                int col = index % 8;
                var clickedPosition = new Position(row, col);

                if (box.Tag as string == "currentPosition")
                {
                    var validMoves = _controller.GetValidMoves(clickedPosition);
                    HighlightValidMoves(validMoves);
                }
                else
                {
                    if (_controller.IsValidMove(clickedPosition, _controller.GetPlayerPosition()))
                    {
                        bool isMoveSuccess = _controller.TryMakeMove(clickedPosition, out bool isLevelComplete);

                        if (isMoveSuccess)
                        {
                            UpdateMoveCount();
                            RefreshBoard();
                            ShowCurrentPosition(clickedPosition);
                            DisplayRecentMoves();  // Refresh recent moves

                            if (isLevelComplete)
                            {
                                MessageBox.Show("Level Complete!");
                                _controller.LoadNextLevel();
                                UpdateMoveCount();
                                UpdateLevelCounter();
                                RefreshBoard();
                                DisplayRecentMoves();  // Refresh recent moves
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Move! Please try a different position.");
                    }
                }
            }
        }



        // Highlights cells with valid moves
        public void HighlightValidMoves(List<Position> validMoves)
        {
            foreach (Control control in _boardPanel.Controls)
            {
                if (control is PictureBox box && box.Tag as string != "currentPosition")
                {
                    box.Tag = null;
                    box.Invalidate();
                }
            }

            foreach (var pos in validMoves)
            {
                int index = pos.Row * 8 + pos.Column;
                if (_boardPanel.Controls[index] is PictureBox box)
                {
                    box.Tag = true;
                    box.Invalidate();
                }
            }
        }

        // Refreshes the entire board, resetting all cells
        public void RefreshBoard()
        {
            foreach (Control control in _boardPanel.Controls)
            {
                if (control is PictureBox box)
                {
                    var position = ParsePositionFromControlName(box.Name);
                    var piece = _controller.GetPieceAtPosition(position);

                    box.Image = piece != null ? GetPieceImage(piece) : null;
                    box.SizeMode = PictureBoxSizeMode.Zoom;
                    box.BackColor = (position.Row + position.Column) % 2 == 0 ? Color.White : Color.Gray;
                    box.Tag = null;
                }
            }

            var playerPosition = _controller.GetPlayerPosition();
            ShowCurrentPosition(playerPosition);
        }

        // Updates the move count label
        public void UpdateMoveCount()
        {
            _moveCounterLabel.Text = $"Move Count: {_controller.MoveCount}";
        }

        // Updates the level counter label
        public void UpdateLevelCounter()
        {
            _levelCounterLabel.Text = $"Current Level: {_controller.LevelCount}";
        }

        // Highlights the player's current position
        public void ShowCurrentPosition(IPosition currentPosition)
        {
            foreach (Control control in _boardPanel.Controls)
            {
                if (control is PictureBox box)
                {
                    box.Tag = null;
                    box.Invalidate();
                }
            }

            Control positionControl = GetControlAtPosition(currentPosition);
            if (positionControl is PictureBox currentBox)
            {
                currentBox.Tag = "currentPosition";
                currentBox.Invalidate();
            }
        }

        // Helper to get a control at a given position
        private Control GetControlAtPosition(IPosition position)
        {
            string controlName = $"position_{position.Row}_{position.Column}";
            return _boardPanel.Controls.Find(controlName, true).FirstOrDefault();
        }

        private Position ParsePositionFromControlName(string controlName)
        {
            var parts = controlName.Split('_');
            return new Position(int.Parse(parts[1]), int.Parse(parts[2]));
        }

        private Image GetPieceImage(IPiece piece)
        {
            string imagePath = $"../../../Images/{piece.Type}.png";
            return Image.FromFile(imagePath);
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            var (newPosition, newMoveCount, errorMessage) = _controller.Undo();

            if (errorMessage != null)
            {
                MessageBox.Show(errorMessage);
            }
            else
            {
                if (newPosition != null)
                {
                    ShowCurrentPosition(newPosition);
                }
                _moveCounterLabel.Text = $"Move Count: {newMoveCount}";
                RefreshBoard();

                // Remove the last move from the database and refresh the recent moves display
                _controller.RemoveLastMovement();
                DisplayRecentMoves(); // Refresh recent moves
            }
        }


        private void DisplayRecentMoves()
        {
            var recentMoves = _controller.GetRecentMovements(10); // Fetch the 10 most recent moves
            recentMovesDataGridView.Rows.Clear(); // Clear any existing rows

            foreach (var move in recentMoves)
            {
                recentMovesDataGridView.Rows.Add(move.PieceMoved, move.FromPosition, move.ToPosition, move.Timestamp.ToString("T"));
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _controller.ClearMovementData(); // Wipe movement data upon closure
        }




    }
}
