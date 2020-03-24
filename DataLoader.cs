using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP
{
    class DataLoader
    {
        private const string SUDOKU_FILENAME = "Resources/Sudoku.csv";

        public static List<Sudoku> LoadSudokus()
        {
            List<Sudoku> sudokus = new List<Sudoku>();
            List<string> lines = File.ReadAllLines(SUDOKU_FILENAME).ToList();

            lines.GetRange(1, lines.Count-1).ForEach(
                line =>
                {
                    string sudokuString = line.Split(';')[2];

                    SudokuNode[,] sudokuNodes = new SudokuNode[9, 9];
                    
                    for(int i = 0; i < 9; i++)
                    {
                        for(int j = 0; j < 9; j++)
                        {
                            char numberChar = sudokuString[i * 9 + j];
                            int number = Char.IsNumber(numberChar) ? numberChar - '0' : 0;
                            sudokuNodes[i, j] = new SudokuNode(i, j, number);
                        }
                    }

                    sudokus.Add(new Sudoku(sudokuNodes));
                });

            return sudokus;
        }
    }
}
