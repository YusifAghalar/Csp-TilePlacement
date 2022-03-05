using System.Text.RegularExpressions;

namespace Csp_TilePlacement
{
    public class Landscape
    {
        public List<List<int>> Layout { get; set; }
        public Dictionary<string, int> Tiles { get; set; }
        public Dictionary<string, int> Target { get; set; }

        public static Landscape Build(string text)
        {
            text = text.Replace("\r", "").Trim();
            var sections = new List<string>() { "landscape", "tiles", "targets" };
            int cnt = -2;
            var lines = text.Split("\n");

            var layout = new List<List<int>>();
            var tiles= new Dictionary<string, int>();
            var target= new Dictionary<string, int>();

         
            

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
                        layout.Add(temp);
                        break;

                    case "targets":
                        var words = line.Split(":",StringSplitOptions.RemoveEmptyEntries);
                        target.Add(words[0], Convert.ToInt32(words[1]));
                        break;

                    default:
                        break;
                }

            }

            return new Landscape()
            {
                Tiles = tiles,
                Target = target,
                Layout = layout
            };


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
        }
}
