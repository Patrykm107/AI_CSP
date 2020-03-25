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
            checkAllNodes();
        }

        public bool Solve()
        {
            SudokuNode nextNode = FindNextNodeByOrder(0, 0);

            if (nextNode != null)
            {
                for(int i = 0; i < nextNode.domain.Count; i++)
                {
                    nextNode.value = FindNextValueByOrder(nextNode, i);
                    CheckConstraintsForAllAffected(nextNode);
                    if(Solve()) return true;
                }
                nextNode.value = 0;
                CheckConstraintsForAllAffected(nextNode);

                return false;
            }
            else
            {
                Console.WriteLine(this.ToString());
                return false;
            }
        }

        private SudokuNode FindNextNodeByOrder(int startX, int startY)
        {
            bool finished = false;
            int x = startX, y = startY;

            while (sudokuNodes[x, y].value != 0 && !finished)
            {
                if(y < 8)
                {
                    y++;
                }
                else
                {
                    if (x >= 8)
                    {
                        finished = true;
                    }
                    else
                    {
                        x++;
                        y = 0;
                    }
                }

            }
            return finished ? null : sudokuNodes[x, y];
        }

        private int FindNextValueByOrder(SudokuNode node, int NoVisited)
        {
            return node.domain[NoVisited];
        }

        //Check all constraints for all nodes that might be affected after inserting new value
        private void CheckConstraintsForAllAffected(SudokuNode causingNode)
        {
            for (int i = 0; i < 9; i++) //row
            {
                SudokuNode curr = sudokuNodes[causingNode.row, i];
                if(curr.value == 0) checkConstraints(curr);
            }

            for(int i = 0; i < 9; i++)  //column
            {
                SudokuNode curr = sudokuNodes[i, causingNode.column];
                if (curr.value == 0) checkConstraints(curr);
            }

            int r = causingNode.row / 3, c = causingNode.column / 3;
            for (int i = 0; i < 3; i++) //box
            {
                for(int j = 0; j < 3; j++)
                {
                    int row = r * 3 + i, column = c * 3 + j;
                    if (row != causingNode.row && column != causingNode.column)
                    {
                        SudokuNode curr = sudokuNodes[row, column];
                        if (curr.value == 0) checkConstraints(curr);
                    }
                }
            }
        }

        //Adjust all domains at the begging
        private void checkAllNodes()
        {
            foreach (SudokuNode node in sudokuNodes)
            {
                if (node.value == 0)
                {
                    checkConstraints(node);
                }
            }
        }

        //Check constraints for single node
        private void checkConstraints(SudokuNode node)
        {
            List<int> blockedValues = new List<int>();
            for(int i = 0; i < 9; i++)  //row
            {
                SudokuNode curr = sudokuNodes[node.row, i];
                if (curr.value != 0) blockedValues.Add(curr.value);
            }
            for (int i = 0; i < 9; i++) //column
            {
                SudokuNode curr = sudokuNodes[i, node.column];
                if (curr.value != 0) blockedValues.Add(curr.value);
            }
            
            int r = node.row / 3, c = node.column / 3;
            for(int i = 0; i<3; i++)    //box
            {
                for(int j = 0; j<3; j++)
                {
                    SudokuNode curr = sudokuNodes[r * 3 + i, c * 3 + j];
                    if (curr.value != 0) blockedValues.Add(curr.value);
                }
            }

            node.domain = Enumerable.Range(1, 9).ToList();
            node.domain.RemoveAll(x => blockedValues.Distinct().Contains(x));
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
                    stringBuilder.AppendLine();
                }
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}