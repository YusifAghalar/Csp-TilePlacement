


using System.Diagnostics;

namespace Csp_TilePlacement
{
    static class Program
    {
        static void Main(string[] args)
        {


            var sp = new Stopwatch();
            sp.Start();
          
            var landscape = Landscape.Build(File.ReadAllText(args[0]));
            var solver= new Solver(landscape);
            var result = solver.Solve(0,0);
            if (!result)
            {

                Console.WriteLine("Unsolved");
                return;
            }
               

            foreach (var key in solver.Solution.Keys)
            {
                Console.WriteLine($"{key} {solver.Solution[key]}");
            }
            sp.Stop();
            Console.WriteLine(sp.ElapsedMilliseconds);
            Console.WriteLine(solver.counter);


            
        }

      


    }

}