using System.Diagnostics;
using System.Linq;

namespace Csp_TilePlacement
{
    static class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Solving....");
            var sp = new Stopwatch();
            sp.Start();
          
            var landscape = Landscape.Build(File.ReadAllText(args[0]));
            var solver= new Solver(landscape);
            var result = solver.Backtrack(solver.Heuristics.GetMRV());
            if (!result)
            {

                Console.WriteLine("Unsolved");
                Console.WriteLine($"Iterations:{solver.Iterations}");
                return;
            }
               

            foreach (var square in solver.Landscape.Squares.OrderBy(x=>x.Index))
            {
                Console.WriteLine($"{square.Index} {4} {square.AssignedTile}");
            }
            sp.Stop();
            Console.WriteLine($"Milliseconds elapsed :{sp.ElapsedMilliseconds}");
            Console.WriteLine($"Iterations:{solver.Iterations}");

          

            
        }

      


    }

}