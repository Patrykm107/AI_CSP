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

            foreach (int i in Enumerable.Range(1, 4))
            {
                Console.WriteLine(i);
                Jolka jolka = DataLoader.LoadJolka(i);
                jolka.SolveForward();
                jolka.printResearch();
            }


            //Console.WriteLine(sudokus[40]);

            /*            List<Sudoku> sudokus = DataLoader.LoadSudokus();
                        int[] ints = new int[] { 20, 35, 41 };
                        foreach (int i in ints)
                        {
                            Console.WriteLine(i);
                            sudokus[i].SolveForward();
                            sudokus[i].printResearch();
                        }*/
            /*            sudokus[3].SolveBacktracking();
                        sudokus[3].printResearch();*/

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
