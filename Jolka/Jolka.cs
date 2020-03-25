using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP
{
    class Jolka
    {
        private const char EMPTY = '_';
        private const char BLOCKED = '#';

        private char[,] puzzle;
        private List<string> words;
        private List<JolkaNode> nodes;
        private List<JolkaConstraint> constraints;

        public Jolka(char[,] puzzle, List<string> words)
        {
            this.puzzle = puzzle;
            this.words = words;
            ExtractNodesAndConstraints();
        }

        private void ExtractNodesAndConstraints()
        {

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
