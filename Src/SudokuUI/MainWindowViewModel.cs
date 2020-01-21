using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuUI
{
    public class MainWindowViewModel
    {
        public string Title => "Sudoku solver";

        public  int Rank => 3;

        public string[,] Array { get; set; }

        public MainWindowViewModel()
        {
            var array = new List<List<string>>();
            for (int x = 0; x < Rank*Rank; x++)
            {
                for (int y = 0; y < Rank*Rank; y++)
                {
                    
                }
            }
        }
    }
}
