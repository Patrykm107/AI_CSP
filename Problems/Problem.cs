using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP
{
    abstract class Problem<T>
    {
        public List<Node<T>> nodes;

        public bool Solve()
        {
            Node<T> nextNode = FindNextNodeByOrder();

            if (nextNode != null)
            {
                for (int i = 0; i < nextNode.domain.Count; i++)
                {
                    nextNode.Fill(FindNextValueByOrder(nextNode, i));
                    CheckConstraintsForAllAffected(nextNode);
                    if (Solve()) return true;
                }
                nextNode.Clear();
                CheckConstraintsForAllAffected(nextNode);

                return false;
            }
            else
            {
                Console.WriteLine(this.ToString());
                return false;
            }
        }

        protected Node<T> FindNextNodeByOrder() {
            foreach(Node<T> node in nodes)
            {
                if (node.IsEmpty())
                {
                    return node;
                }
            }

            return null;
        }

        protected T FindNextValueByOrder(Node<T> node, int i)
        {
            return node.domain[i];
        }

        abstract protected void CheckConstraintsForAllAffected(Node<T> causingNode);
    }
}
