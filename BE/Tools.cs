using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    /// <summary>
    /// The class contains  Auxiliary functions used to work with data in BE layer
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// The function Flat array(2d -> 1d)
        /// </summary>
        /// <typeparam name="T">The type of the array(<paramref name="arr"/>)</typeparam>
        /// <param name="arr">The array to flat</param>
        /// <returns>The flated array(1d array, Size = rows * columns, Type: <typeparamref name="T"/>)</returns>
        ///<remarks>The function do the oposite from <seealso cref="Expand{T}(T[], int)"/></remarks>
        public static T[] Flatten<T>(this T[,] arr)
        {
            int rows = arr.GetLength(0);
            int columns = arr.GetLength(1);
            T[] arrFlattened = new T[rows * columns];
            for (int j = 0; j < columns; j++)
            {
                for (int i = 0; i < rows; i++)
                {
                    arrFlattened[i + j * rows] = arr[i, j];
                }
            }
            return arrFlattened;
        }

        /// <summary>
        /// The function Expand array(1d -> 2d)
        /// </summary>
        /// <typeparam name="T">The type of the array(<paramref name="arr"/>)</typeparam>
        /// <param name="arr">The array to flat</param>
        /// <param name="rows">The number of rows</param>
        /// <returns>The Expand array(2d array, Size = rows = <paramref name="rows"/>, columns = length / rows, Type: <typeparamref name="T"/>)</returns>
        ///<remarks>The function do the oposite from <seealso cref="Flatten{T}(T[,])"/></remarks>
        public static T[,] Expand<T>(this T[] arr, int rows)
        {
            int length = arr.GetLength(0);
            int columns = length / rows;
            T[,] arrExpanded = new T[rows, columns];
            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = 0; j < columns - 1; j++)
                {
                    arrExpanded[i, j] = arr[i + j * rows];
                }
            }
            return arrExpanded;
        }

    }
}
