using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP
{
    class JolkaConstraint
    {
        public int row, column;
        public char letter;
        public List<JolkaNode> nodesAffected;

        public JolkaConstraint(int row, int column, List<JolkaNode> nodes = null)
        {
            this.row = row;
            this.column = column;
            this.nodesAffected = nodes == null ? new List<JolkaNode>() : nodes;
        }
    }
}
