using System;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            const int size = 2;
            int[,] array = new int[size, size];


            for (var x = 0; x < size; x++)
            {
                for (var y = 0; y < size; y++)
                {
                    Console.Write(array[x, y]+" ");
                }
                Console.WriteLine();
            }
        }
    }
}
