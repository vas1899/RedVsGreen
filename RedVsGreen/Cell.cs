using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedVsGreen
{
    class Cell : Grid
    {
        //list with the neighbours of the cell
        public List<Cell> adjacentsList = new List<Cell>();

        //if the cell is green it is 1, red - 0
        public int Condition { get; set; }
        //the condition in the next generation
        public int NextGenCondition{ get; set; }

        public Cell(int Condition)
        {
            this.Condition = Condition;
            //by the creation they are the same since we stil do not know 
            //if it will change in the next generation
            NextGenCondition = Condition;
        }

        /// <summary>
        /// Fills the list adjacentsList with the "neighbours" of the cell
        /// </summary>
        /// <param name="adj">object of a Cell that is next to the one we are in</param>
        public void AddAdjacents(Cell adj)
        {
            adjacentsList.Add(adj);
        }

        
       

        
    }
}
