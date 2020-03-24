using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP
{
    class Sudoku
    {
        private SudokuNode[,] sudokuNodes;

        public Sudoku(SudokuNode[,] sudokuNodes)
        {
            this.sudokuNodes = sudokuNodes;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            stringBuilder.Append(sudokuNodes[i * 3 + j, k * 3 + l].value);
                        }
                        stringBuilder.Append(" ");
                    }
                    stringBuilder.Append("\n");
                }
                stringBuilder.Append("\n");
            }

            return stringBuilder.ToString();
        }
    }
}