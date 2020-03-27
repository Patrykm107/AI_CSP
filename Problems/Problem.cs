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

        public bool SolveForward()
        {
            Node<T> nextNode = FindNextNodeByOrder();

            if (nextNode != null)
            {
                for (int i = 0; i < nextNode.domain.Count; i++)
                {
                    nextNode.Fill(FindNextValueByOrder(nextNode, i));
                    adjustDomainsForAllAffected(nextNode);
                    if (nodes.Find(node => node.domain.Count == 0) != null)
                    {
                        continue;
                    }
                    if (SolveForward()) return true;
                }
                nextNode.Clear();
                adjustDomainsForAllAffected(nextNode);

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

        abstract protected void adjustDomainsForAllAffected(Node<T> causingNode);
    }
}
