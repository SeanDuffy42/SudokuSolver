using SudukoSolverLib.Interfaces;
using SudukoSolverLib.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudukoSolverLib
{
    public class SudokuSolver : ISudokuSolver
    {
        private ISearchGrid searchGrid;

        public SudokuSolver(ISearchGrid searchGrid)
        {
            this.searchGrid = searchGrid;
        }

        public List<List<HashSet<int>>> SolvePuzzle(string fileName)
        {
            var grid = readFileIntoGrid(fileName);

            while (!isSolved(grid))
            {
                searchGrid.basicSearch(grid);
                searchGrid.nondrantSearch(grid);
                searchGrid.checkForNakedPairs(grid);
                searchGrid.checkForNakedTriples(grid);
            }

            return grid;
        }

        public void printGrid(List<List<List<int>>> grid)
        {
            foreach (var row in grid)
            {
                foreach (var col in row)
                {
                    if (col.Count == 1)
                    {
                        Console.Write(col.Single());
                    }
                    else
                    {
                        Console.Write('X');
                    }
                }

                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public List<List<HashSet<int>>> readFileIntoGrid(string fileName)
        {
            var grid = new List<List<HashSet<int>>>();

            var lines = File.ReadAllLines(fileName);

            foreach (var line in lines)
            {
                var row = new List<HashSet<int>>();

                foreach (var box in line)
                {
                    if (box == 'X')
                    {
                        row.Add(new HashSet<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                    }
                    else
                    {
                        row.Add(new HashSet<int> { int.Parse(box.ToString()) });
                    }
                }

                grid.Add(row);
            }

            return grid;
        }

        public bool isSolved(List<List<HashSet<int>>> grid)
        {
            foreach (var row in grid)
            {
                foreach (var col in row)
                {
                    if (col.Count != 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public int numberOfOptions(List<List<HashSet<int>>> grid)
        {
            var result = 0;

            foreach (var row in grid)
            {
                foreach (var col in row)
                {
                    result += col.Count;
                }
            }

            return result;
        }

        public int numberSolved(List<List<HashSet<int>>> grid)
        {
            var result = 0;

            foreach (var row in grid)
            {
                foreach (var col in row)
                {
                    if(col.Count == 1)
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        public bool whoIsKillingTheOptions(List<List<HashSet<int>>> grid)
        {
            foreach (var row in grid)
            {
                foreach (var col in row)
                {
                    if (col.Count == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
