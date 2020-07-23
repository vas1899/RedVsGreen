using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedVsGreen
{
    class Grid
    {
        //x cordinate for the width of the grid
        private int x;
        //y cordinate for the length of the grid
        private int y;
        //array with the grid 
        public Cell[,] grid;
    
        //default constructor - needed for the inheritance
        protected Grid()
        {
            x = 0;
            y = 0;
            grid = new Cell[y, x];
        }

        public Grid(int x, int y)
        {
            this.x = x;
            this.y = y;
            grid = new Cell[y, x];
        }
       
        //this variable is at the position when a row is still not created
        //and when it is, 1 is added to its value so it can move to the next one
        private int row = 0;
        /// <summary>
        /// Fills the grid with the information provided through the console.
        /// </summary>
        /// <param name="input">a row of in the grid, each char is a digit - 0 or 1</param>
        public void CreateCell(string input)
        {
            for (int i = 0; i < x; i++)
            {
                try
                {
                    grid[row, i] = new Cell(int.Parse(input.Substring(i, 1)));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            //adding 1 so we can go to the next row when this method is called
            row++;
        }


        /// <summary>
        ///creates the cells that the current one is in touch with
        ///using the AddAjacents method in the Cell class
        /// </summary>
        public void AddingAdjacent()
        {
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {

                    CheckIfAdjExists(i, j, i, j - 1); //left
                    CheckIfAdjExists(i, j, i, j + 1); //right
                    CheckIfAdjExists(i, j, i - 1, j); //up
                    CheckIfAdjExists(i, j, i + 1, j); //down
                    CheckIfAdjExists(i, j, i - 1, j - 1); //left up
                    CheckIfAdjExists(i, j, i + 1, j - 1); //left down
                    CheckIfAdjExists(i, j, i - 1, j + 1); //right up
                    CheckIfAdjExists(i, j, i + 1, j + 1); //right down

                    //foreach (var cell in grid[i, j].adjacentsList)
                    //{
                    //    Console.WriteLine(cell.Condition);
                    //}
                    //Console.WriteLine("\n\n");
                   
                }
            }
        }
        /// <summary>
        /// Checks if there is adjacent on that position and then calls AddAdjacents from the Cell class
        /// </summary>
        /// <seealso cref="Cell.AddAdjacents(Cell)"/>
        /// <param name="thisY">the cordinate y of the current cell</param>
        /// <param name="thisX">the cordinate x of the current cell</param>
        /// <param name="adjY">the cordinate y of the adjacent</param>
        /// <param name="adjX">the cordinate x of the adjacent</param>
        private void CheckIfAdjExists( int thisY, int thisX, int adjY, int adjX)
        {
            if (adjX < 0 || adjY < 0 || adjX >= x || adjY >= y)
                return;
            grid[thisY, thisX].AddAdjacents(grid[adjY, adjX]);
        }


        
    }
}
