using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP
{
    class JolkaNode : Node<string>
    {
        private const char EMPTY = '_';

        private char[,] puzzle; //I don't like giving access to whole board, but had no idea how to give reference to sub array of the puzzle with just spaces belonging to the Node
        public int begin, end;
        public int pos;
        public bool filled = false;

        public List<string> fullDomain;
        public Position position;
        public List<JolkaConstraint> constraints;

        private List<int> keep; //Ids for letters to keep during cleaning (not inputted by this Node)

        public JolkaNode(int begin, int end, int pos, ref char[,] puzzle, List<string> domain, Position position, List<JolkaConstraint> constraints = null)
        {
            this.begin = begin;
            this.end = end;
            this.pos = pos;
            this.puzzle = puzzle;
            this.fullDomain = new List<string>(domain);
            this.domain = new List<string>(domain);
            this.position = position;
            this.constraints = constraints == null ? new List<JolkaConstraint>() : constraints;
            this.keep = Enumerable.Range(0, end - begin + 1).ToList();

            this.constraints.ForEach(con => con.nodesAffected.Add(this));
        }

        public override void Fill(string value)
        {
            if (filled) Clear();
            keep = new List<int>();
            for(int i = 0; i < end - begin + 1; i++)
            {
                if (position == Position.Horizontal) {
                    if (puzzle[pos, begin + i] == EMPTY)
                    {
                        puzzle[pos, begin + i] = value[i];
                        JolkaConstraint constr = constraints.Find(con => con.row == pos && con.column == begin + i);
                        if (constr != null) constr.letter = value[i];
                    }
                    else
                    {
                        keep.Add(i);
                    }
                }
                else
                {
                    if (puzzle[begin + i, pos] == EMPTY)
                    {
                        puzzle[begin + i, pos] = value[i];
                        JolkaConstraint constr = constraints.Find(con => con.row == begin + i && con.column == pos);
                        if (constr != null) constr.letter = value[i];
                    }
                    else
                    {
                        keep.Add(i);
                    }
                    
                }

            }

            filled = true;
        }

        public override void Clear()
        {
            if (!filled) return;
            for (int i = 0; i < end - begin + 1; i++)
            {
                if (keep.Contains(i)) 
                {
                    keep.Remove(i);
                    continue;
                }

                if (position == Position.Horizontal)
                {
                    puzzle[pos, begin + i] = EMPTY;
                    JolkaConstraint constr = constraints.Find(con => con.row == pos && con.column == begin + i);
                    if (constr != null) constr.letter = EMPTY;
                }
                else
                {
                    puzzle[begin + i, pos] = EMPTY;
                    JolkaConstraint constr = constraints.Find(con => con.row == begin + i && con.column == pos);
                    if (constr != null) constr.letter = EMPTY;
                }

            }

            filled = false;
        }

        public override bool IsEmpty()
        {
            return !filled;
        }
        
        public void checkConstraints()
        {
            domain = new List<string>(fullDomain);

            constraints.Where(con => con.letter != EMPTY).ToList().ForEach(
                con => {
                    if(position == Position.Horizontal)
                    {
                        domain = domain.Where(word => word[con.column-begin].Equals(con.letter)).ToList();
                    }
                    else
                    {
                        domain = domain.Where(word => word[con.row - begin].Equals(con.letter)).ToList();
                    }
                });
        }
    }
    enum Position
    {
        Horizontal, Vertical
    };
}
