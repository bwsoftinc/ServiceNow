using System;
using System.Linq;

namespace ServiceNow.GridNav
{
    /// <summary>
    /// Represents a rectangular grid with width and height dimensions
    /// and an array of values for each grid coordinate
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// The height of the grid
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// The width of the grid
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// The numeric values at each grid location on the board
        /// Ordered in sequence by row left-to-right and top-to-bottom
        /// </summary>
        public long[] Numbers { get; private set; }

        /// <summary>
        /// Create the board with the given height width and grid values
        /// Throws an exception if not a valid configuration of the grid
        /// </summary>
        /// <param name="height">the height of the gird</param>
        /// <param name="width">the width of the grid</param>
        /// <param name="numbers">the values for each grid location</param>
        public Grid(int height, int width, long[] numbers)
        {
            if (numbers == null)
                throw new ArgumentNullException("there must be grid values");

            if (height < 1 || width < 1)
                throw new ArgumentOutOfRangeException("invalid dimensions");

            if(numbers.Any(n => n == long.MinValue))
                throw new ArgumentOutOfRangeException("invalid node value, cannot be long.minvalue");

            //validate there are all grid values for the game board dimensions
            if (height * width != numbers.Length)
                throw new ArgumentException("Invalid board configuration");

            Numbers = numbers;
            Width = width;
            Height = height;
        }
    }
}

