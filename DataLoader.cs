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

        public static (char[,], List<string>) loadJolka(int fileNumber)
        {
            List<string> puzzleLines = File.ReadAllLines($"Resources/Jolka/puzzle{fileNumber}").ToList();
            int rows = puzzleLines.Count;
            int columns = puzzleLines[0].Count();

            char[,] puzzle = new char[rows, columns];
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    puzzle[i, j] = puzzleLines[i][j];
                }
            }

            List<string> wordsList = File.ReadAllLines($"Resources/Jolka/words{fileNumber}").ToList();

            return (puzzle, wordsList);
        }
    }
}
