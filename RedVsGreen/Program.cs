//Vasil Kolev
/*
 * In class Grid there is an array that stores all the objects of cells from the class Cell
 * In class Cell there is a list that has all the adjacents of the current cell
 * The program checks all the adjacents and if has to change the condition in the next generation
 * It does that for every cell
 * and then the cells takes the values for the next generation 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RedVsGreen
{
    class Program
    {
        static void Main(string[] args)
        {
            //First are printed the 2 exapmles 
            // and after that a custom one can be created
                     
            //test1
            int x = 3, y = 3, x1 = 1, y1 = 0, N = 10;
            Grid gridTest1 = new Grid(x, y);
            gridTest1.CreateCell("000");
            gridTest1.CreateCell("111");
            gridTest1.CreateCell("000");
            Console.WriteLine("x = 3, y = 3, x1 = 1, y1 = 0, N = 10\n000\n111\n000");
            //making the arcs between the cells
            gridTest1.AddingAdjacent();
            //calculates how many times the cell has been green
            PlayGame(gridTest1, x, y, x1, y1, N);

            //test 2
            x = 4; y = 4; x1 = 2; y1 = 2; N = 15;
            Grid gridTest2 = new Grid(x, y);
            gridTest2.CreateCell("1001");
            gridTest2.CreateCell("1111");
            gridTest2.CreateCell("0100");
            gridTest2.CreateCell("1010");
            Console.WriteLine("\nx = 4, y = 4, x1 = 2, y1 = 2, N = 15\n1001\n1111\n0100\n1010");
            //making the arcs between the cells
            gridTest2.AddingAdjacent();
            //calculates how many times the cell has been green
            PlayGame(gridTest2, x, y, x1, y1, N);

            //Input:
            //use the input string and parse the value to the x and y
            GetXY(ref x, ref y);
            Grid grid = new Grid(x, y);
            Console.WriteLine("Grid:");
            //getting the input for the grid and filling the info into an array
            for (int i = 0; i < y; i++)
            {
                string input = Console.ReadLine();
                grid.CreateCell(input);
                Console.WriteLine("input: " + input);
            }
            //use the input string and parse the value to the x1, y1 and N 
            GetXYN(ref x1, ref y1, ref N);
            //making the arcs between the cells
            grid.AddingAdjacent();
            //calculates how many times the cell has been green
            PlayGame(grid, x, y, x1, y1, N);

            Console.Read();
        }

        /// <summary>
        /// Counts how many times the cell with cordinates x1 and y1 has been green
        /// and prints it
        /// </summary>
        /// <param name="grid">The object with the information for the grid</param>
        /// <param name="x">The width of the grid</param>
        /// <param name="y">The length of the grid</param>
        /// <param name="x1">The position of the cell</param>
        /// <param name="y1">The position of the cell</param>
        /// <param name="N">The generations</param>
        static void PlayGame(Grid grid, int x, int y, int x1, int y1, int N)
        {
            int beenGreen = 0;
            for (int i = 0; i <= N; i++)
            {
                if (grid.grid[y1, x1].Condition == 1)
                    beenGreen++;
                for (int j = 0; j < y; j++)
                {
                    for (int k = 0; k < x; k++)
                    {
                        IsItGreen(k, j, grid);
                    }
                }
                for (int j = 0; j < y; j++)
                {
                    for (int k = 0; k < x; k++)
                    {
                        grid.grid[j, k].Condition = grid.grid[j, k].NextGenCondition;
                    }
                }
            }
            Console.WriteLine("Times green: " + beenGreen +"\n\n");
        }

        /// <summary>
        /// Checks if the cell with cordinates x0 and y0 has to change its condition 
        /// in the next generation
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="grid"></param>
        /// <returns>if the cell is green returns true, red - false</returns>
        static bool IsItGreen(int x0, int y0, Grid grid)
        {
            
            int counter = 0;


            if (grid.grid[y0, x0].Condition == 1)
            {
                grid.grid[y0, x0].adjacentsList.ForEach(i =>
                {
                    if (i.Condition == 1)
                        counter++;
                });
             
                //if a green cell is surounded by 2, 3 or 6 green cells it stays green,
                //otherwise becomes red in the next generation
                if (counter != 2 && counter != 3 && counter != 6)
                {
                    grid.grid[y0, x0].NextGenCondition = 0;
                }
                return true;
            }
            //when it is 0(red)
            else
            {
                grid.grid[y0, x0].adjacentsList.ForEach(i =>
                {
                    if (i.Condition == 1)
                        counter++;
                });
                //if a red cell is surounded by 3 or 6 green cells it becomes green
                if (counter == 3 || counter == 6)
                {
                    grid.grid[y0, x0].NextGenCondition = 1;
                }
                return false;
            }

        }


        /// <summary>
        /// Takes the input from the console and assign the values to x and y
        /// </summary>
        /// <param name="x">The width of the grid</param>
        /// <param name="y">The height of the grid</param>
        static void GetXY(ref int x, ref int y)
        {
            Console.WriteLine("Enter x, y:   Example: 5, 2");
            string input = Console.ReadLine();
            int length = input.Length;
            string xStr = string.Empty;
            string yStr = string.Empty;
            int i = 0;
            //checks if how many digits is x
            //when it reaches ',', it break and 2 is added to skip ',' and the space 
            //and go to y's first digit
            while (char.IsDigit(input[i]))
            {
                xStr += input[i];
                i++;
            }
            i += 2;
            while (i < length && char.IsDigit(input[i]))
            {
                yStr += input[i];
                i++;
            }

            try
            {
                x = int.Parse(xStr);
                y = int.Parse(yStr);
                if(x > y || x>=1000 || y >= 1000)
                {
                    Console.WriteLine("Rules: x <=y<1 000");
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Console.WriteLine($"x: {x}, y: {y}");
        }

        //same as GetXY but for x1, y1, N
        static void GetXYN(ref int x1, ref int y1, ref int N)
        {
            Console.WriteLine("Input x1, y1, N:");
            string input = Console.ReadLine();
            int length = input.Length;
            string xStr = string.Empty;
            string yStr = string.Empty;
            string NStr = string.Empty;
            int i = 0;
            //checks if how many digits is x
            //when it reaches ',', it break and 2 is added to skip ',' and the space 
            //and go to y's first digit
            while (char.IsDigit(input[i]))
            {
                xStr += input[i];
                i++;
            }
            i += 2;
            while (char.IsDigit(input[i]))
            {
                yStr += input[i];
                i++;
            }
            i += 2;
            while (i < length && char.IsDigit(input[i]))
            {
                NStr += input[i];
                i++;
            }

            try
            {
                x1 = int.Parse(xStr);
                y1 = int.Parse(yStr);
                N = int.Parse(NStr);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Console.WriteLine($"x: {x1}, y: {y1}, N: {N}");
        }
    }
}
