using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csp_TilePlacement
{
    public class Square
    {
        public Square()
        {
            CurrentState =  new State();
            
            
        }
        public Square(Square square)
        {
            X= square.X;
            Y= square.Y;
            Index = square.Index;
            CurrentState = square.CurrentState;
            Original = square.Original;
            AssignedTile = new string(square.AssignedTile);
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Index { get; set; }
        public int NumberOfBushes { get; set; }
        public State Original { get; set; }
        public State CurrentState { get; set; }
        public string AssignedTile { get; set; }
        public List<string> AvailableTiles {  get; set; }
        public Dictionary<string,int> CountOfAllBushesAfterTile {  get; set; }


        public Dictionary<string, int> ScanSquare()
        {
            var result = new Dictionary<string, int>();

            for (int i = 0; i < CurrentState.Data.Length; i++)
            {
                for (int j = 0; j < CurrentState.Data[i].Length; j++)
                {
                    if (CurrentState.Data[i][j] < 1) continue;
                    if (result.ContainsKey(CurrentState.Data[i][j].ToString()))
                        result[CurrentState.Data[i][j].ToString()]++;
                    else
                        result.Add(CurrentState.Data[i][j].ToString(), 1);
                }
            }

            return result;
        }

        public void PutTile(string tileKey)
        {

            var newData = new int[4][];
            for (int i = 0; i < 4; i++)
            {
              
                newData[i] = new int[4]; ;
                for (int j = 0; j < 4; j++)
                {
                    newData[i][j] = CurrentState.Data[i][j];
                }
            }
            switch (tileKey)
            {
                case "OUTER_BOUNDARY":
                    Array.Fill(newData[0], -1, 0, 4);
                    newData[1][0] = -1;
                    newData[1][3] = -1;

                    newData[2][0] = -1;
                    newData[2][3] = -1;

                    Array.Fill(newData[3], -1, 0, 4);
                    break;

                case "EL_SHAPE":
                    Array.Fill(newData[0], -1, 0, 4);

                    newData[1][0] = -1;

                    newData[2][0] = -1;

                    newData[3][0] = -1;
                    break;

                case "FULL_BLOCK":
                    Array.Fill(newData[0], -1, 0, 4);

                    Array.Fill(newData[1], -1, 0, 4);

                    Array.Fill(newData[2], -1, 0, 4);

                    Array.Fill(newData[3], -1, 0, 4);

                    break;
            }


            AssignedTile = tileKey;
            
            CurrentState = new State() { Data=newData,Previous=CurrentState};
        }

        public void Revert()
        {
            CurrentState = Original;
            AssignedTile = null;
        }

        
        public List<string> GetLCV()
        {
            PutTile("EL_SHAPE");
            var el_shape = CurrentState.Data.SelectMany(x => x).Count(x => x > 0);
            Revert();

            PutTile("OUTER_BOUNDARY");
            var outer_body = CurrentState.Data.SelectMany(x => x).Count(x => x > 0);
            Revert();


            if (el_shape < outer_body)
            {
                return new List<string>()
                {
                    "EL_SHAPE",
                    "OUTER_BOUNDARY",
                    "FULL_BLOCK"
                };
            }

            return new List<string>()
            {
                "OUTER_BOUNDARY",
                "EL_SHAPE",
                "FULL_BLOCK"

            };

        }

    
        public void Print()
            {
                foreach (var row in CurrentState.Data)
                {
                    foreach (var bush in row)
                    {
                        if(bush>-1)
                            Console.Write(bush+" ");
                        else
                            Console.Write("# ");
                    }
                    Console.WriteLine();
                   
                }
                Console.WriteLine();
            }
        }
      
}
