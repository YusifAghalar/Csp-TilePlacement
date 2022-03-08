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
                Console.WriteLine(solver.counter);
                return;
            }
               

            foreach (var key in solver.Solution.Keys.OrderBy(x=>x))
            {
                Console.WriteLine($"{key} {4} {solver.Solution[key]}");
            }
            sp.Stop();
            Console.WriteLine($"Milliseconds elapsed :{sp.ElapsedMilliseconds}");
            Console.WriteLine($"Iterations:{solver.counter}");

          

            
        }

      


    }

}