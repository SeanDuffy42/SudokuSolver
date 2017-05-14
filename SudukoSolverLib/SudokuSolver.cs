using SudukoSolverLib.Interfaces;
using SudukoSolverLib.Models;
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

        public List<List<Guess>> SolvePuzzle(string fileName)
        {
            var grid = readFileIntoGrid(fileName);

            while (!isSolved(grid))
            {
                var foundValue = true;

                foundValue = searchGrid.basicSearch(grid);

                foundValue = searchGrid.nondrantSearch(grid) || foundValue;

                foundValue = searchGrid.checkForNakedPairs(grid) || foundValue;

                foundValue = searchGrid.checkForNakedTriples(grid) || foundValue;
            }

            return grid;
        }

        public void printGrid(List<List<Guess>> grid)
        {
            foreach (var row in grid)
            {
                foreach (var col in row)
                {
                    if (col.Options.Count() == 1)
                    {
                        Console.Write(col.Options.Single());
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

        public List<List<Guess>> readFileIntoGrid(string fileName)
        {
            var grid = new List<List<Guess>>();

            var lines = File.ReadAllLines("puzzle.txt");

            foreach (var line in lines)
            {
                var row = new List<Guess>();

                foreach (var box in line)
                {
                    if (box == 'X')
                    {
                        row.Add(new Guess()
                        {
                            Options = new HashSet<int> { 1,2,3,4,5,6,7,8,9}
                        });
                    }
                    else
                    {
                        var singleGuess = new Guess
                        {
                            Value = int.Parse(box.ToString()),
                            Options = new HashSet<int> { int.Parse(box.ToString()) }
                        };

                        row.Add(singleGuess);
                    }
                }

                grid.Add(row);
            }

            return grid;
        }

        public bool isSolved(List<List<Guess>> grid)
        {
            foreach (var row in grid)
            {
                foreach (var col in row)
                {
                    if (col.Options.Count() != 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public int numberOfOptions(List<List<Guess>> grid)
        {
            var result = 0;

            foreach (var row in grid)
            {
                foreach (var col in row)
                {
                    result += col.Options.Count();
                }
            }

            return result;
        }

        public int numberSolved(List<List<Guess>> grid)
        {
            var result = 0;

            foreach (var row in grid)
            {
                foreach (var col in row)
                {
                    if(col.Options.Count() == 1)
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        public bool whoIsKillingTheOptions(List<List<Guess>> grid)
        {
            foreach (var row in grid)
            {
                foreach (var col in row)
                {
                    if (col.Options.Count() == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
