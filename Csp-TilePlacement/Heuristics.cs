using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Csp_TilePlacement
{
    public class Heuristics
    {
        public IOrderedEnumerable<Square> Squares { get; set; }

    
        public Heuristics(List<Square> squares)
        {
            Squares = squares.OrderByDescending(x => x.NumberOfBushes);
           
        }

        /// <summary>
        /// Returns square with least  number of  bushes
        /// </summary>
        /// <returns></returns>
        public Square GetMRV()
        {
            //Squares already ordered, no need to re-order
            return Squares.Where(x => x.AssignedTile == null).FirstOrDefault();
        }

    
       
    }
}