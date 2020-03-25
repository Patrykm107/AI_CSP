using System;
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
            List<Sudoku> sudokus = DataLoader.LoadSudokus();
            
            Console.WriteLine(sudokus[0]);

            DateTime start = DateTime.Now;
            sudokus[0].Solve();
            Console.WriteLine($"{(DateTime.Now - start)}");

            /*            foreach(Sudoku sudoku in sudokus)
                        {
                            DateTime start = DateTime.Now;
                            sudoku.Solve();
                            Console.WriteLine($"{(DateTime.Now - start)}");
                        }*/

            Console.ReadLine();
        }
    }
}
