﻿using SudukoSolverLib.Interfaces;
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

            return SolvePuzzle(grid);
        }

        public List<List<HashSet<int>>> SolvePuzzle(List<List<HashSet<int>>> grid)
        {
            var numOfOptions = numberOfOptions(grid);

            var Done = false;

            while (!Done)
            {
                numOfOptions = numberOfOptions(grid);

                searchGrid.basicSearch(grid);
                searchGrid.hiddenSinglesSearch(grid);
                searchGrid.nondrantSearch(grid);
                searchGrid.checkForNakedPairsAndTriples(grid);

                Done = isSolved(grid) || numOfOptions == numberOfOptions(grid);
            }

            return grid;
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
    }
}
