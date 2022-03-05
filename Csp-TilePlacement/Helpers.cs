
namespace Csp_TilePlacement
{
    internal static class Helpers
    {

      

        public static Dictionary<string,int> CountColors(List<List<int>> layout)
        {
            var result = new Dictionary<string,int>();

            for (int i = 0; i < layout.Count; i++)
            {
                for (int j = 0; j < layout[i].Count; j++)
                {
                    if (result.ContainsKey(layout[i][j].ToString()))
                        result[layout[i][j].ToString()]++;
                    else
                        result.Add(layout[i][j].ToString(), 1);
                }
            }
            return result;
        }
    }
}
