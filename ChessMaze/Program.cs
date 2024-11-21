using System;
using System.Windows.Forms;

namespace ChessMaze
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize GameController with a file path
            string filePath = @"..\..\..\Level_1.json";
            var controller = new GameController(filePath);

            // Start the GameForm with the initialized GameController instance
            Application.Run(new GameForm(controller));
        }
    }
}
