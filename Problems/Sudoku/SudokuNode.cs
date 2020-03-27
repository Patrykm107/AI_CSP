using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP
{
    class SudokuNode : Node<int>
    {
        public int value;

        public int row;
        public int column;

        public SudokuNode(int row, int column, int value = 0)
        {
            this.row = row;
            this.column = column;
            this.value = value;
            domain = value == 0 ? Enumerable.Range(1, 9).ToList() : new List<int>{ value };
        }

        public override void Fill(int value)
        {
            this.value = value;
        }

        public override void Clear()
        {
            this.value = 0;
        }
        
        public override bool IsEmpty()
        {
            return value == 0;
        }
    }
}
