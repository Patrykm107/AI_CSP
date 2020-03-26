using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP
{
    class JolkaNode
    {
        public int begin, end;
        public List<string> domain;
        public List<string> fullDomain;
        public Position position;
        public List<JolkaConstraint> constraints;

        private List<int> keep; //Ids for letters to keep after cleaning (not inputted by this Node)

        public JolkaNode(int begin, int end, List<string> domain, Position position, List<JolkaConstraint> constraints = null)
        {
            this.begin = begin;
            this.end = end;
            this.fullDomain = new List<string>(domain);
            this.domain = new List<string>(domain);
            this.position = position;
            this.constraints = constraints == null ? new List<JolkaConstraint>() : constraints;
            this.keep = new List<int>();

            this.constraints.ForEach(con => con.nodesAffected.Add(this));
        }

    }
    enum Position
    {
        Horizontal, Vertical
    };
}
