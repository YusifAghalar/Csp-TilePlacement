using System.Text.RegularExpressions;

namespace Csp_TilePlacement
{
    public class Landscape
    {
        public Landscape()
        {
            Layout =new int[4][];
            AvailableTiles = new Dictionary<string, int> {};
            Target  =new Dictionary<string, int>{};
            Squares=new List<Square>();
        }
        public int[][] Layout { get; set; }
        public Dictionary<string, int> AvailableTiles { get; set; }
        public Dictionary<string, int> Target { get; set; }
        public List<Square> Squares { get; set; }

        public static Landscape Build(string text)
        {
            text = text.Replace("\r", "");
            var sections = new List<string>() { "landscape", "tiles", "targets" };
            int cnt = -2;
            var lines = text.Split("\n");

            var map = new List<List<int>>();
            var tiles = new Dictionary<string, int>();
            var target = new Dictionary<string, int>();

            tiles.Add("OUTER_BOUNDARY", Convert.ToInt32(Regex.Match(text, "OUTER_BOUNDARY=(?<g>\\d*)").Groups.GetValueOrDefault("g").Value));
            tiles.Add("EL_SHAPE", Convert.ToInt32(Regex.Match(text, "EL_SHAPE=(?<g>\\d*)").Groups.GetValueOrDefault("g").Value));
            tiles.Add("FULL_BLOCK", Convert.ToInt32(Regex.Match(text, "FULL_BLOCK=(?<g>\\d*)").Groups.GetValueOrDefault("g").Value));

            foreach (var line in lines)
            {
                if (line == "")
                    continue;

                if (line.StartsWith("#"))
                {
                    cnt++;
                    continue;
                }
                if (cnt == 4)
                    break;



                var currentSection = sections[cnt];
                switch (currentSection)
                {
                    case "landscape":
                        var temp = new List<int>();
                        temp.AddRange(Tokenize(line));
                        map.Add(temp);
                        break;

                    case "targets":
                        var words = line.Split(":", StringSplitOptions.RemoveEmptyEntries);
                        target.Add(words[0], Convert.ToInt32(words[1]));
                        break;

                    default:
                        break;
                }

            }
        

            
            var landscape=  new Landscape()
            {
                AvailableTiles = tiles,
                Target = target,
                Layout = map.Select(x => x.ToArray()).ToArray()
            };
            landscape.Layout=AddAdditionalZeros(landscape.Layout);
            landscape.FillSquares();
            return landscape;
            


        }

        public static List<int> Tokenize(string line)
        {
            var result = new List<int>();
            for (int i = 0; i < line.Length; i += 2)
            {
                if (line[i] == ' ')
                    result.Add(0);
                else
                    result.Add(Convert.ToInt32(line[i].ToString()));

            }
            return result;
        }

        public static int[][]AddAdditionalZeros(int[][] arr) { 
            var maxLenght=arr.Max(x=> x.Length);
            for (int i = 0; i < arr.Length; i++)
            {
                while (arr[i].Length < maxLenght)
                {
                    arr[i]= arr[i].Append(0).ToArray();
                }
            }
            return arr;
        }

        public void FillSquares()
        {
         
            var size = Layout.Length;
            var totalSquares = (size*size / 16);

            var row = 0;
            var column = 0;

            var index=1;
          

            for (row = 0; row < size; row+=4)
            {
                for (column = 0; column< size; column+=4)
                {
                    var newSquare = new Square() { X = row, Y = column, Number = index };



                    Array.Copy(Layout, row, newSquare.State.Data, 0, 4);
                    for (int i = 0; i < newSquare.State.Data.Length; i++)
                        newSquare.State.Data[i] = newSquare.State.Data[i].Skip(column).Take(4).ToArray();
                    
                    Squares.Add(newSquare);
                    index++;
                }
            }
          ;


        }


    }
}
