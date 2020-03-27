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
        private bool firstLoop = true;
        private bool resultFound = false;

        private UInt32 visitedNodesTotal = 0;
        private UInt32 noOfReturnsTotal = 0;
        private UInt32 visitedNodesToFind = 0;
        private UInt32 noOfReturnsToFind = 0;
        private UInt32 noOfResults = 0;
        private DateTime startTime;
        private TimeSpan timeToFindResult;
        private TimeSpan totalMethodTime;

        public void SolveForward()
        {
            if (firstLoop)
            { 
                startTime = DateTime.Now;
                firstLoop = false;
            }
            Node<T> nextNode = FindNextNodeByOrder();

            if (nextNode != null)
            {
                visitedNodesTotal++;
                for (int i = 0; i < nextNode.domain.Count; i++)
                {
                    nextNode.Fill(FindNextValueByOrder(nextNode, i));
                    adjustDomainsForAllAffected(nextNode);
                    if (nodes.Find(node => node.domain.Count == 0) != null)
                    {
                        continue;
                    }
                    SolveForward();
                }
                nextNode.Clear();
                adjustDomainsForAllAffected(nextNode);
            }
            else
            {
                if (!resultFound)
                {
                    timeToFindResult = DateTime.Now - startTime;
                    visitedNodesToFind = visitedNodesTotal;
                    noOfReturnsToFind = noOfReturnsTotal;
                }
                noOfResults++;
                //Console.WriteLine(this.ToString());
            }

            totalMethodTime = DateTime.Now - startTime;
            noOfReturnsTotal++;
        }

        public void SolveBacktracking()
        {
            if (firstLoop)
            {
                startTime = DateTime.Now;
                firstLoop = false;
            }
            Node<T> nextNode = FindNextNodeByOrder();

            if (nextNode != null)
            {
                visitedNodesTotal++;
                for (int i = 0; i < nextNode.domain.Count; i++)
                {
                    nextNode.Fill(FindNextValueByOrder(nextNode, i));
                    if (constraintsFullfilled(nextNode))
                    {
                        SolveBacktracking();
                    }
                }
                nextNode.Clear();
            }
            else
            {
                if (!resultFound)
                {
                    timeToFindResult = DateTime.Now - startTime;
                    visitedNodesToFind = visitedNodesTotal;
                    noOfReturnsToFind = noOfReturnsTotal;
                }
                noOfResults++;
                //Console.WriteLine(this.ToString());
            }

            totalMethodTime = DateTime.Now - startTime;
            noOfReturnsTotal++;
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

        protected Node<T> FindNextNodeByRandom()
        {
            List<Node<T>> emptyNodes = nodes.Where(n => n.IsEmpty()).ToList();
            if(emptyNodes.Count == 0)
            {
                return null;
            }
            else
            {
                return emptyNodes[new Random().Next(emptyNodes.Count)];
            }
        }

        protected T FindNextValueByOrder(Node<T> node, int i)
        {
            return node.domain[i];
        }

        public void printResearch()
        {
            Console.Write(
                $"Time to find first result: {timeToFindResult}\n" +
                $"Number of nodes visited to find result: {visitedNodesToFind}\n" +
                $"Number of returns to find result: {noOfReturnsToFind}\n" +
                $"Total time: {totalMethodTime}\n" +
                $"Total nodes visited: {visitedNodesTotal}\n" +
                $"Total returns: {noOfReturnsTotal}\n" +    //Includes returns from finished problem
                $"Number of results: {noOfResults}\n\n");
        }


        abstract protected bool constraintsFullfilled(Node<T> node);

        abstract protected void adjustDomainsForAllAffected(Node<T> causingNode);
    }
}
