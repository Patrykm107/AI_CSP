using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP
{
    abstract class Node<T>
    {
        public List<T> domain;

        abstract public void Fill(T value);
        abstract public bool IsEmpty();
        abstract public void Clear();
    }
}
