namespace ChessMaze
{
    partial class GameForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel _boardPanel;
        private System.Windows.Forms.Label _moveCounterLabel;
        private System.Windows.Forms.Button _saveButton;
        private System.Windows.Forms.Button _loadButton;
        private System.Windows.Forms.Label _levelCounterLabel;
        private System.Windows.Forms.Button undoButton;
        private System.Windows.Forms.DataGridView recentMovesDataGridView;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            _boardPanel = new Panel();
            _moveCounterLabel = new Label();
            _saveButton = new Button();
            _loadButton = new Button();
            _levelCounterLabel = new Label();
            undoButton = new Button();
            SuspendLayout();

            int cellSize = 75;

            AddPictureBox("position_0_0", 0, 0, cellSize, Color.White);
            AddPictureBox("position_0_1", 0, 1, cellSize, Color.Gray);
            AddPictureBox("position_0_2", 0, 2, cellSize, Color.White);
            AddPictureBox("position_0_3", 0, 3, cellSize, Color.Gray);
            AddPictureBox("position_0_4", 0, 4, cellSize, Color.White);
            AddPictureBox("position_0_5", 0, 5, cellSize, Color.Gray);
            AddPictureBox("position_0_6", 0, 6, cellSize, Color.White);
            AddPictureBox("position_0_7", 0, 7, cellSize, Color.Gray);

            AddPictureBox("position_1_0", 1, 0, cellSize, Color.Gray);
            AddPictureBox("position_1_1", 1, 1, cellSize, Color.White);
            AddPictureBox("position_1_2", 1, 2, cellSize, Color.Gray);
            AddPictureBox("position_1_3", 1, 3, cellSize, Color.White);
            AddPictureBox("position_1_4", 1, 4, cellSize, Color.Gray);
            AddPictureBox("position_1_5", 1, 5, cellSize, Color.White);
            AddPictureBox("position_1_6", 1, 6, cellSize, Color.Gray);
            AddPictureBox("position_1_7", 1, 7, cellSize, Color.White);

            AddPictureBox("position_2_0", 2, 0, cellSize, Color.White);
            AddPictureBox("position_2_1", 2, 1, cellSize, Color.Gray);
            AddPictureBox("position_2_2", 2, 2, cellSize, Color.White);
            AddPictureBox("position_2_3", 2, 3, cellSize, Color.Gray);
            AddPictureBox("position_2_4", 2, 4, cellSize, Color.White);
            AddPictureBox("position_2_5", 2, 5, cellSize, Color.Gray);
            AddPictureBox("position_2_6", 2, 6, cellSize, Color.White);
            AddPictureBox("position_2_7", 2, 7, cellSize, Color.Gray);

            AddPictureBox("position_3_0", 3, 0, cellSize, Color.Gray);
            AddPictureBox("position_3_1", 3, 1, cellSize, Color.White);
            AddPictureBox("position_3_2", 3, 2, cellSize, Color.Gray);
            AddPictureBox("position_3_3", 3, 3, cellSize, Color.White);
            AddPictureBox("position_3_4", 3, 4, cellSize, Color.Gray);
            AddPictureBox("position_3_5", 3, 5, cellSize, Color.White);
            AddPictureBox("position_3_6", 3, 6, cellSize, Color.Gray);
            AddPictureBox("position_3_7", 3, 7, cellSize, Color.White);

            AddPictureBox("position_4_0", 4, 0, cellSize, Color.White);
            AddPictureBox("position_4_1", 4, 1, cellSize, Color.Gray);
            AddPictureBox("position_4_2", 4, 2, cellSize, Color.White);
            AddPictureBox("position_4_3", 4, 3, cellSize, Color.Gray);
            AddPictureBox("position_4_4", 4, 4, cellSize, Color.White);
            AddPictureBox("position_4_5", 4, 5, cellSize, Color.Gray);
            AddPictureBox("position_4_6", 4, 6, cellSize, Color.White);
            AddPictureBox("position_4_7", 4, 7, cellSize, Color.Gray);

            AddPictureBox("position_5_0", 5, 0, cellSize, Color.Gray);
            AddPictureBox("position_5_1", 5, 1, cellSize, Color.White);
            AddPictureBox("position_5_2", 5, 2, cellSize, Color.Gray);
            AddPictureBox("position_5_3", 5, 3, cellSize, Color.White);
            AddPictureBox("position_5_4", 5, 4, cellSize, Color.Gray);
            AddPictureBox("position_5_5", 5, 5, cellSize, Color.White);
            AddPictureBox("position_5_6", 5, 6, cellSize, Color.Gray);
            AddPictureBox("position_5_7", 5, 7, cellSize, Color.White);

            AddPictureBox("position_6_0", 6, 0, cellSize, Color.White);
            AddPictureBox("position_6_1", 6, 1, cellSize, Color.Gray);
            AddPictureBox("position_6_2", 6, 2, cellSize, Color.White);
            AddPictureBox("position_6_3", 6, 3, cellSize, Color.Gray);
            AddPictureBox("position_6_4", 6, 4, cellSize, Color.White);
            AddPictureBox("position_6_5", 6, 5, cellSize, Color.Gray);
            AddPictureBox("position_6_6", 6, 6, cellSize, Color.White);
            AddPictureBox("position_6_7", 6, 7, cellSize, Color.Gray);

            AddPictureBox("position_7_0", 7, 0, cellSize, Color.Gray);
            AddPictureBox("position_7_1", 7, 1, cellSize, Color.White);
            AddPictureBox("position_7_2", 7, 2, cellSize, Color.Gray);
            AddPictureBox("position_7_3", 7, 3, cellSize, Color.White);
            AddPictureBox("position_7_4", 7, 4, cellSize, Color.Gray);
            AddPictureBox("position_7_5", 7, 5, cellSize, Color.White);
            AddPictureBox("position_7_6", 7, 6, cellSize, Color.Gray);
            AddPictureBox("position_7_7", 7, 7, cellSize, Color.White);

            _boardPanel.SuspendLayout();

            // 
            // _boardPanel
            // 
            _boardPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _boardPanel.BackColor = Color.Gray;
            _boardPanel.Location = new Point(10, 50);
            _boardPanel.Name = "_boardPanel";
            _boardPanel.Size = new Size(600, 600);
            _boardPanel.TabIndex = 0;

            // 
            // _moveCounterLabel
            // 
            _moveCounterLabel.AutoSize = true;
            _moveCounterLabel.Location = new Point(10, 10);
            _moveCounterLabel.Name = "_moveCounterLabel";
            _moveCounterLabel.Size = new Size(129, 25);
            _moveCounterLabel.TabIndex = 1;
            _moveCounterLabel.Text = "Move Count: 0";
            // 
            // _saveButton
            // 
            _saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _saveButton.Location = new Point(10, 754);
            _saveButton.Name = "_saveButton";
            _saveButton.Size = new Size(75, 23);
            _saveButton.TabIndex = 2;
            _saveButton.Text = "Save Game";
            _saveButton.UseVisualStyleBackColor = true;
            _saveButton.Click += new EventHandler(SaveGame);
            // 
            // _loadButton
            // 
            _loadButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _loadButton.Location = new Point(100, 754);
            _loadButton.Name = "_loadButton";
            _loadButton.Size = new Size(75, 23);
            _loadButton.TabIndex = 3;
            _loadButton.Text = "Load Game";
            _loadButton.UseVisualStyleBackColor = true;
            _loadButton.Click += new EventHandler(LoadGame);
            // 
            // _levelCounterLabel
            // 
            _levelCounterLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _levelCounterLabel.AutoSize = true;
            _levelCounterLabel.Location = new Point(655, 9);
            _levelCounterLabel.Name = "_levelCounterLabel";
            _levelCounterLabel.Size = new Size(133, 25);
            _levelCounterLabel.TabIndex = 4;
            _levelCounterLabel.Text = "Current Level: 1";
            // 
            // undoButton
            // 
            undoButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            undoButton.Location = new Point(676, 37);
            undoButton.Name = "undoButton";
            undoButton.Size = new Size(112, 34);
            undoButton.TabIndex = 5;
            undoButton.Text = "Undo";
            undoButton.UseVisualStyleBackColor = true;
            undoButton.Click += new EventHandler(undoButton_Click);

            // 
            // GameForm
            // 
            ClientSize = new Size(800, 800);
            Controls.Add(undoButton);
            Controls.Add(_levelCounterLabel);
            Controls.Add(_boardPanel);
            Controls.Add(_moveCounterLabel);
            Controls.Add(_saveButton);
            Controls.Add(_loadButton);
            Name = "GameForm";
            Text = "ChessMaze";
            ResumeLayout(false);
            PerformLayout();


            recentMovesDataGridView = new DataGridView
            {
                Name = "recentMovesDataGridView",
                Location = new Point(ClientSize.Width - 210, 100), // Positioned to the right, below the undo button
                Size = new Size(200, ClientSize.Height - 150), // Fixed width, expandable height
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right, // Anchored to top, bottom, and right only
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // Add columns to the DataGridView
            recentMovesDataGridView.Columns.Add("PieceMoved", "Piece");
            recentMovesDataGridView.Columns.Add("FromPosition", "From");
            recentMovesDataGridView.Columns.Add("ToPosition", "To");
            recentMovesDataGridView.Columns.Add("Timestamp", "Time");

            // Add recentMovesDataGridView to the form
            Controls.Add(recentMovesDataGridView);




            // Add existing controls to the form
            Controls.Add(undoButton);
            Controls.Add(_levelCounterLabel);
            Controls.Add(_boardPanel);
            Controls.Add(_moveCounterLabel);
            Controls.Add(_saveButton);
            Controls.Add(_loadButton);

            // Set up form properties
            ClientSize = new Size(800, 800);
            Name = "GameForm";
            Text = "ChessMaze";
            ResumeLayout(false);
            PerformLayout();
        }

        private void AddPictureBox(string name, int row, int col, int cellSize, Color backColor)
        {
            var box = new PictureBox
            {
                Name = name,
                BorderStyle = BorderStyle.FixedSingle,
                Width = cellSize,
                Height = cellSize,
                Location = new Point(col * cellSize, row * cellSize),
                BackColor = backColor
            };
            box.Click += new EventHandler(OnGridCellClick);
            box.Paint += new PaintEventHandler(Box_Paint);
            _boardPanel.Controls.Add(box);
        }
    }
}
