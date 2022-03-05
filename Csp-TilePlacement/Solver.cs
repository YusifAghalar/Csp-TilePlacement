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


        public bool Solve(int x, int y)
        {
            counter++;
            if (HasFoundSolution())
                return true;


            foreach (var tileKey in Landscape.Tiles.OrderBy(x=>x.Value).Select(x=>x.Key))
            {
                if (Landscape.Tiles[tileKey] == 0)
                    continue;
                var temp = Helpers.CopyLayout(Landscape.Layout);

                if (IsPossibleToPutTile(tileKey, Landscape.Layout, x, y))
                {
                    Landscape.Tiles[tileKey] -= 1;
                    Landscape.Layout = PutTile(tileKey, Landscape.Layout, x, y);
                    if (Solution.ContainsKey($"{x}-{y}"))
                        Solution[$"{x}-{y}"] = tileKey;
                    else Solution.Add($"{x}-{y}", tileKey);

                    (int oldX, int oldY) = (x, y);
                    (x,y)= NextLocation(oldX, oldY);
                    if (Solve(x, y)) return true;
                    (x,y)=(oldX, oldY);
                    Landscape.Layout = temp;
                    Landscape.Tiles[tileKey] += 1;

                }

            }


            return false;
        }



        public bool HasFoundSolution()
        {
            var currentColors = ScanBushes(Landscape.Layout);

            foreach (var item in currentColors.Keys)
            {
                if (currentColors[item] != Landscape.Target[item])
                    return false;

            }
            return true;
        }


        public Dictionary<string, int> ScanBushes(int[][] layout)
        {
            var result = new Dictionary<string, int>();

            for (int i = 0; i < layout.Length; i++)
            {
                for (int j = 0; j < layout[i].Length; j++)
                {
                    if(layout[i][j] <1 ) continue;
                    if (result.ContainsKey(layout[i][j].ToString()))
                        result[layout[i][j].ToString()]++;
                    else
                        result.Add(layout[i][j].ToString(), 1);
                }
            }
            return result;
        }

        public bool IsPossibleToPutTile(string tileName, int[][] layout, int x, int y)
        {

            var nextLayout = PutTile(tileName, layout, x, y);
            var currentBushes = ScanBushes(nextLayout);
            foreach (var item in currentBushes.Keys)
            {
                if (currentBushes[item] < Landscape.Target[item])
                    return false;

            }
            return true;

        }


        public int[][] PutTile(string tileName, int[][] currentLayout, int x, int y)
        {
            var temp = currentLayout.CopyLayout();
            var square = temp[x..(x + 4)];

            var row1 = square[0][y..(y + 4)];
            var row2 = square[1][y..(y + 4)];
            var row3 = square[2][y..(y + 4)];
            var row4 = square[3][y..(y + 4)];

            switch (tileName)
            {
                case "OUTER_BOUNDARY":
                    Fill(ref row1, "all");

                    Fill(ref row2, "sides");

                    Fill(ref row3, "sides");

                    Fill(ref row4, "all");

                    break;
                case "EL_SHAPE":
                    Fill(ref row1, "all");

                    Fill(ref row2, "start");

                    Fill(ref row3, "start");

                    Fill(ref row4, "start");

                    break;
                case "FULL_BLOCK":
                    Fill(ref row1, "all");

                    Fill(ref row2, "all");

                    Fill(ref row3, "all");

                    Fill(ref row4, "all");

                    break;

                default:
                    break;
            }
           
            Array.Copy(row1, 0, temp[x], y, 4);
            Array.Copy(row2, 0, temp[x+1], y, 4);
            Array.Copy(row3, 0, temp[x+2], y, 4);
            Array.Copy(row4, 0, temp[x+3], y, 4);
          
            return temp;

        }


        public void Fill(ref int[] arr, string method)
        {
            switch (method)
            {
                case "all":
                    {
                        for (int i = 0; i < arr.Length; i++)

                            arr[i] = -1;
                        break;
                    }

                case "start":
                    arr[0] = -1;
                    break;
                case "end":
                    arr[3] = -1;
                    break;
                case "sides":
                    arr[0] = -1;
                    arr[3] = -1;

                    break;
            }
        }

        public (int x, int y) NextLocation(int x, int y)
        {
            if (y + 4 < Landscape.Layout[0].Length)
                y = y + 4;
            else
            {
                y = 0;
                if (x + 4 < Landscape.Layout.Length)
                {
                    x += 4;
                }
            }
            return (x, y);
        }

    }
}
