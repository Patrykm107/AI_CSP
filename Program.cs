﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DataLoader.LoadSudokus()[0].ToString());

            Console.ReadLine();
        }
    }
}
