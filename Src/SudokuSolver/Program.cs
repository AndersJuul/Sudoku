using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SudokuSolver
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Choose size of the sudoku square
            const int size = 4;

            var recursionDepth = 0;
            // Get all the solutions
            var solutions = GetSolutions(size, new int[size, size], recursionDepth).ToArray();

            // Loop over all solutions, print them...
            foreach (var solution in solutions)
            {
                PrintSolution(size, solution, "Solution:", recursionDepth);
            }
        }

        private static void PrintSolution(int size, int[,] solution, string headline, int indentation)
        {
            var indentationPrefix = new string(' ',indentation);
            Console.WriteLine(indentationPrefix+ headline);
            for (var x = 0; x < size; x++)
            {
                Console.Write(indentationPrefix);
                for (var y = 0; y < size; y++)
                {
                    Console.Write(solution[x, y] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static IEnumerable<int[,]> GetSolutions(int size, int[,] array, int recursionDepth)
        {
            /* 
             * Strategy (Divide and conquer):
             * Find first blank, searching from top/left.
             * Find all possible values for that field and
             * for each value, try inserting it and call
             * recursively. 
             */

            PrintSolution(size, array, "Attempt:",recursionDepth);

            // Find first blank. If no blanks found we are done (returning).
            var blank = GetBlank(array, size);
            if (!blank.HasValue)
            {
                yield return array;
                yield break;
            }

            Console.WriteLine(new string(' ',recursionDepth)+$"Found blank at {blank.Value.X},{blank.Value.Y}");

            // Get all the possible insertions for this position...
            var legalInsertions = GetLegalInsertions(array, size, blank.Value).ToArray();

            // Print the legals
            Console.WriteLine(new string(' ', recursionDepth)+"Legals: " + string.Join(",", legalInsertions.Select(x => x.ToString())));

            // ... and try them one by one. For each possible new array, call recursively.
            foreach (var legalInsertion in legalInsertions)
            {
                // Clone the array to a new copy ...
                var workingCopy =(int[,]) array.Clone();
                // Set the legal value at blank position
                workingCopy[blank.Value.X, blank.Value.Y] = legalInsertion;

                // Return all (if any) solutions found with this new insertion
                var subSolutions = GetSolutions(size, workingCopy, recursionDepth+1).ToArray();
                foreach (var subSolution in subSolutions) yield return subSolution;
            }
        }

        private static IEnumerable<int> GetLegalInsertions(int[,] array, int size, Point blank)
        {
            /*
             * Try each of the numbers, return the number as a legal option if it passes test.
             */
            for (var possibleInsertion = 1; possibleInsertion < size + 1; possibleInsertion++)
                if (IsValidInsertion(array, size, blank, possibleInsertion))
                    yield return possibleInsertion;
        }

        private static bool IsValidInsertion(int[,] array, int size, Point blank, int possibleInsertion)
        {
            /*
             * If one of the rows have the number in the 'blank' column, it's not a legal insertion.
             */
            for (var y = 0; y < size; y++)
                if (array[blank.X, y] == possibleInsertion)
                    return false;
            /*
             * If one of the columns have the number in the 'blank' row, it's not a legal insertion.
             */
            for (var x = 0; x < size; x++)
                if (array[x, blank.Y] == possibleInsertion)
                    return false;

            //TODO Test for the 'inner squares' in 9x9 

            /* If we have not found it to be illegal by now, it's legal */
            return true;
        }

        private static Point? GetBlank(int[,] array, int size)
        {
            // Loop over all columns, all rows, return first position found to be zero (blank).
            for (var x = 0; x < size; x++)
            for (var y = 0; y < size; y++)
                if (array[x, y] == 0)
                    return new Point(x, y);

            // Didn't find a 'first one'? Then there is none!
            return null;
        }
    }
}