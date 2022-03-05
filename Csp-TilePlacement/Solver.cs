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
        }

        public Landscape Landscape { get; }


        public bool Solve(int x,int y)
        {
            if(HasFoundSolution())
                return true;


            foreach (var tileKey in Landscape.Tiles.Keys)
            {
                if (Landscape.Tiles[tileKey] == 0)
                    continue;
                var  temp = Helpers.CopyLayout(Landscape.Layout);

                if(IsPossibleToPutTile(tileKey,Landscape.Layout,x, y))
                {
                    Landscape.Tiles[tileKey] -= 1;
                    Landscape.Layout=PutTile(tileKey,Landscape.Layout,x,y);
                }

            
            }


            return false;
        }

     

        public bool HasFoundSolution()
        {
            var currentColors = CountColors(Landscape.Layout);
         
            foreach (var item in currentColors.Keys)
            {
                if (currentColors[item] != Landscape.Target[item])
                    return false;

            }
            return true;
        }


        public  Dictionary<string, int> CountColors(int[][] layout)
        {
            var result = new Dictionary<string, int>();

            for (int i = 0; i < layout.Length; i++)
            {
                for (int j = 0; j < layout[i].Length; j++)
                {
                    if (result.ContainsKey(layout[i][j].ToString()))
                        result[layout[i][j].ToString()]++;
                    else
                        result.Add(layout[i][j].ToString(), 1);
                }
            }
            return result;
        }

        public bool IsPossibleToPutTile(string tileName,int[][] layout, int x, int y)
        {

            var nextLayout = PutTile(tileName,layout,x,y);
            var currentColors= CountColors(layout);
            foreach (var item in currentColors.Keys)
            {
                if (currentColors[item] < Landscape.Target[item])
                    return false;

            }
            return true;

        }


        public int[][]  PutTile(string tileName,int[][] currentLayout, int x, int y)
        {
            var temp =currentLayout.CopyLayout();
            var square = temp[x..(x+4)];

            var row1=temp[0][y..(y+4)];
            var row2=temp[1][y..(y+4)];
            var row3=temp[2][y..(y+4)];
            var row4=temp[3][y..(y+4)];

            switch (tileName)
            {
                case "OUTER_BOUNDARY":
                    Fill(row1, "all");

                    Fill(row2, "sides");

                    Fill(row3, "sides");

                    Fill(row4, "all");

                    break;
                case "EL_SHAPE":
                    Fill(row1, "all");

                    Fill(row2, "start");

                    Fill(row3, "start");

                    Fill(row4, "start");

                    break;
                case "FULL_BLOCK":
                    Fill(row1, "all");

                    Fill(row2, "all");

                    Fill(row3, "all");

                    Fill(row4, "all");

                    break;

                default:
                    break;
            }
            return temp;

        }


        public void Fill(int[] arr,string method)
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


    }
}
