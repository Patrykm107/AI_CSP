using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP
{
    class Jolka : Problem<string>
    {
        private const char EMPTY = '_';

        private char[,] puzzle;
        private List<string> words;

        private List<JolkaConstraint> constraints;

        public Jolka(char[,] puzzle, List<string> words)
        {
            this.puzzle = puzzle;
            this.words = words;
            extractConstraints();
            extractNodes();
        }

        protected override void CheckConstraintsForAllAffected(Node<string> causingNode)
        {
            JolkaNode node = (JolkaNode)causingNode;
            node.constraints.ForEach(
               con => con.nodesAffected.ForEach(
                   n => {
                   if (n.IsEmpty()) n.checkConstraints();
                   })
                );
        }

        private void extractConstraints()
        {
            constraints = new List<JolkaConstraint>();

            for (int i = 0; i < puzzle.GetLength(0); i++)
            {
                for (int j = 0; j < puzzle.GetLength(1); j++)
                {
                    if (puzzle[i, j] == EMPTY)
                    {
                        if (i == 0 && puzzle[i + 1, j] != EMPTY || i == puzzle.GetLength(0) - 1 && puzzle[i - 1, j] != EMPTY
                            || j == 0 && puzzle[i, j + 1] != EMPTY || j == puzzle.GetLength(1) - 1 && puzzle[i, j - 1] != EMPTY)
                        {
                            continue;
                        }

                        if (
                            (i == 0 && puzzle[i + 1, j] == EMPTY || i == puzzle.GetLength(0) - 1 && puzzle[i - 1, j] == EMPTY
                            || puzzle[i - 1, j] == EMPTY || puzzle[i + 1, j] == EMPTY)
                            &&
                            (j == 0 && puzzle[i, j + 1] == EMPTY || j == puzzle.GetLength(1) - 1 && puzzle[i, j - 1] == EMPTY
                            || puzzle[i, j - 1] == EMPTY || puzzle[i, j + 1] == EMPTY))
                        {
                            constraints.Add(new JolkaConstraint(i, j));
                        }
                    }
                }
            }
        }

        private void extractNodes()
        {
            nodes = new List<Node<string>>();
            //Horizontal
            for(int i = 0; i < puzzle.GetLength(0); i++)
            {
                bool inProgress = false;
                int begin = 0;

                for (int j = 0; j < puzzle.GetLength(1); j++)
                {
                    if(puzzle[i, j] == EMPTY)
                    {
                        if (inProgress)
                        {
                            if(j == puzzle.GetLength(1) - 1)
                            {
                                nodes.Add(new JolkaNode(begin, j, i,ref puzzle, words.Where(word => word.Length == j - begin + 1).ToList(), Position.Horizontal,
                                    constraints.Where(constraint => constraint.row == i && constraint.column >= begin && constraint.column <= j).ToList()
                                    ));
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            inProgress = true;
                            begin = j;
                        }
                    }
                    else
                    {
                        if (inProgress)
                        {
                            if(begin != j - 1)
                            {
                                nodes.Add(new JolkaNode(begin, j - 1, i, ref puzzle, words.Where(word => word.Length == j - begin).ToList(), Position.Horizontal,
                                    constraints.Where(constraint => constraint.row == i && constraint.column >= begin && constraint.column < j).ToList()
                                    ));
                            }
                            inProgress = false;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }

            //Vertical
            for (int i = 0; i < puzzle.GetLength(1); i++)
            {
                bool inProgress = false;
                int begin = 0;

                for (int j = 0; j < puzzle.GetLength(0); j++)
                {
                    if (puzzle[j, i] == EMPTY)
                    {
                        if (inProgress)
                        {
                            if (j == puzzle.GetLength(0) - 1)
                            {
                                nodes.Add(new JolkaNode(begin, j, i, ref puzzle, words.Where(word => word.Length == j - begin + 1).ToList(), Position.Vertical,
                                    constraints.Where(constraint => constraint.column == i && constraint.row >= begin && constraint.row <= j).ToList()
                                    ));
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            inProgress = true;
                            begin = j;
                        }
                    }
                    else
                    {
                        if (inProgress)
                        {
                            if (begin != j - 1)
                            {
                                nodes.Add(new JolkaNode(begin, j - 1, i, ref puzzle, words.Where(word => word.Length == j - begin).ToList(), Position.Vertical,
                                    constraints.Where(constraint => constraint.column == i && constraint.row >= begin && constraint.row < j).ToList()
                                    ));
                            }
                            inProgress = false;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for(int i = 0; i < puzzle.GetLength(0); i++)
            {
                for (int j = 0; j < puzzle.GetLength(1); j++)
                {
                    builder.Append(puzzle[i, j]);
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}
