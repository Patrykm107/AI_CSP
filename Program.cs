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

            Jolka jolka = DataLoader.LoadJolka(0);
            jolka.SolveBacktracking();
            jolka.printResearch();

            //Console.WriteLine(sudokus[40]);

            List<Sudoku> sudokus = DataLoader.LoadSudokus();
            sudokus[0].SolveBacktracking();
            sudokus[0].printResearch();

            /*            
                        foreach (Sudoku sudoku in sudokus)
                        {
                            DateTime start = DateTime.Now;
                            sudoku.SolveForward();
                            Console.WriteLine($"{(DateTime.Now - start)}");
                        }*/

            Console.WriteLine("xd");
            Console.ReadLine();
        }
    }
}
