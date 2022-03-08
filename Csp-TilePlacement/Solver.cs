using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csp_TilePlacement
{
    internal class Solver
    {
        public Solver(Landscape landscape)
        {
        
            Landscape = landscape;
            Heuristics = new Heuristics(landscape.Squares);
        }

        public Landscape Landscape { get; }
        public int Iterations = 0;

      
        public Heuristics Heuristics { get; set; }
        
        public bool Backtrack(Square square)
        {
            Iterations++;
            if (HasFoundSolution()&&Landscape.Squares.Count(x=>x.AssignedTile==null)==0)
            return true;
            
            if (square == null) return false;

            /// Get Least constraning tile type for given square
            foreach (var tileKey in square.GetLCV())
            {
                if (Landscape.AvailableTiles[tileKey] == 0)
                    continue;


                if (IsPossibleToPutTile(tileKey, square))
                {
                    //Do
                    Landscape.AvailableTiles[tileKey] -= 1;
                    square.PutTile(tileKey);

                   

                    var nextSquare = Heuristics.GetMRV();
                    if (Backtrack(nextSquare)) return true;


                    //Backtrack
                    Landscape.AvailableTiles[tileKey] += 1;
                    //Revert changes made
                    square.Revert();
                    
                }

            }

            return false;
        }

        public bool HasFoundSolution()
        {
            var currentBushes = ScanBushes(Landscape.Squares);

            foreach (var item in currentBushes.Keys)
            {
                if (currentBushes[item] != Landscape.Target[item])
                    return false;
            }

            return true;
        }


     
        /// <summary>
        /// Scans all squares and returns current state of whole landscape
        /// </summary>
        public Dictionary<string, int> ScanBushes(List<Square> squares)
        {
            var result = new Dictionary<string, int>(){
                { "1", 0},
                { "2", 0},
                { "3", 0},
                { "4", 0},
            };

            foreach (var square in squares)
            {
                var dict = square.ScanSquare();
                foreach (var key in dict.Keys)
                {
                    if (result.ContainsKey(key))
                        result[key] += dict[key];
                }
            }

            return result;
        }

        public bool IsPossibleToPutTile(string tileName, Square square)
        {

            square.PutTile(tileName);
            var currentBushes = ScanBushes(Landscape.Squares);
            foreach (var item in currentBushes.Keys)
            {
                if (currentBushes[item] < Landscape.Target[item])
                {
                    square.Revert();
                    return false;
                }

            }

            return true;
        }

        public void AC3()
        {

        }

    }
}