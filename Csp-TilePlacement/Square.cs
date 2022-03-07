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
            State =  new State();
        }
        public Square(Square square)
        {
            X= square.X;
            Y= square.Y;
            Number = square.Number;
            State = square.State;
            AssignedTile = new string(square.AssignedTile);
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Number { get; set; }

        public State State { get; set; }
     
        public string AssignedTile { get; set; }

        public Dictionary<string, int> Count()
        {
            var result = new Dictionary<string, int>();

            for (int i = 0; i < State.Data.Length; i++)
            {
                for (int j = 0; j < State.Data[i].Length; j++)
                {
                    if (State.Data[i][j] < 1) continue;
                    if (result.ContainsKey(State.Data[i][j].ToString()))
                        result[State.Data[i][j].ToString()]++;
                    else
                        result.Add(State.Data[i][j].ToString(), 1);
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
                    newData[i][j] = State.Data[i][j];
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
            
            State = new State() { Data=newData,Previous=State};
        }

        public void Revert()
        {
            State = State.Previous;
            AssignedTile = null;
        }
    }
  
}
