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
            Solution = new Dictionary<string, string>();
            Landscape = landscape;
        }

        public Landscape Landscape { get; }
        public int counter = 0;

        public Dictionary<string, string> Solution { get; set; }


        public bool Solve(Square square)
        {
            counter++;
            if (HasFoundSolution())
            {

                return true;
            }
            if(square==null) return false;


            foreach (var tileKey in Landscape.AvailableTiles.Keys)
            {
                if (Landscape.AvailableTiles[tileKey] == 0)
                    continue;


                if (IsPossibleToPutTile(tileKey,  square))
                {
                  

                    //Do
                    Landscape.AvailableTiles[tileKey] -= 1;
                    square.PutTile(tileKey);

                    if (Solution.ContainsKey(square.Number.ToString()))
                        Solution[square.Number.ToString()] = tileKey;
                    else Solution.Add(square.Number.ToString(), tileKey);

                    var nextSquare = Landscape.Squares.FirstOrDefault(x => x.AssignedTile == null);
                   
                  
                    if (Solve(nextSquare)) return true;
                   

                    //Backtrack
                    Landscape.AvailableTiles[tileKey] += 1;
                    square.Revert();


                }


            }

            return false;
        }

        public bool HasFoundSolution()
        {
            var currentColors = ScanBushes(Landscape.Squares);

            foreach (var item in currentColors.Keys)
            {
                if (currentColors[item] != Landscape.Target[item])
                    return false;
            }

            return true;
        }


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
                var dict = square.Count();
                foreach (var key in dict.Keys)
                {
                    if (result.ContainsKey(key))
                        result[key] += dict[key];
                }
            }

            return result;
        }

        public bool IsPossibleToPutTile(string tileName,  Square square)
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

    }
}