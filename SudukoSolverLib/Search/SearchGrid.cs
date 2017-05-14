using SudukoSolverLib.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SudukoSolverLib.Models;

namespace SudukoSolverLib.Search
{
    public class SearchGrid : ISearchGrid
    {
        public bool basicSearch(List<List<Guess>> grid)
        {
            var foundValue = false;

            for (var i = 0; i < grid.Count; i++)
            {
                for (var j = 0; j < grid[i].Count; j++)
                {
                    if (grid[i][j].Options.Count() > 1)
                    {
                        for (var k = 1; k < 10; k++)
                        {

                            var rowContainsResult = rowContains(grid[i], k);
                            var colummContainsResult = columnContains(grid, j, k);
                            var nondrantResult = nondrantContains(grid, i, j, k);

                            if (rowContains(grid[i], k) || columnContains(grid, j, k) || nondrantContains(grid, i, j, k))
                            {
                                grid[i][j].Options.Remove(k);

                                if (grid[i][j].Options.Count() == 1)
                                {
                                    foundValue = true;
                                    return foundValue;
                                }
                            }
                        }

                        if (grid[i][j].Options.Count() == 1)
                        {
                            foundValue = true;
                        }
                    }
                }
            }

            return foundValue;
        }

        public bool nondrantSearch(List<List<Guess>> grid)
        {
            var foundValue = false;

            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    var nondrantNeeds = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                    //Get needed values
                    for (var x = i * 3; x < i * 3 + 3; x++)
                    {
                        for (var y = j * 3; y < j * 3 + 3; y++)
                        {
                            if (grid[x][y].Options.Count() == 1)
                            {
                                nondrantNeeds.Remove(grid[x][y].Options.Single());
                            }
                        }
                    }

                    //See where we could put needed values
                    foreach (var need in nondrantNeeds)
                    {
                        var placesWeCouldPutNeededValue = new List<Tuple<int, int>>();

                        for (var x = i * 3; x < i * 3 + 3; x++)
                        {
                            for (var y = j * 3; y < j * 3 + 3; y++)
                            {
                                if (grid[x][y].Options.Count != 1 && !rowContains(grid[x], need) && !columnContains(grid, y, need) && !nondrantContains(grid, x, y, need) && grid[x][y].Options.Contains(need))
                                {
                                    placesWeCouldPutNeededValue.Add(new Tuple<int, int>(x, y));
                                }
                            }
                        }

                        if (placesWeCouldPutNeededValue.Count() == 1)
                        {
                            grid[placesWeCouldPutNeededValue.Single().Item1][placesWeCouldPutNeededValue.Single().Item2].Options.Clear();
                            grid[placesWeCouldPutNeededValue.Single().Item1][placesWeCouldPutNeededValue.Single().Item2].Options.Add(need);

                            foundValue = true;
                        }
                    }
                }
            }

            return foundValue;
        }

        public bool checkForNakedPairs(List<List<Guess>> grid)
        {
            for (var x = 0; x < 9; x++)
            {
                for (var y = 0; y < 9; y++)
                {
                    if (grid[x][y].Options.Count == 2)
                    {
                        removeRowNakedPairOptions(grid[x], grid[x][y].Options);
                        removeColumnNakedPairOptions(grid, y, grid[x][y].Options);
                        removeNondrantNakedPairOptions(grid, x, y, grid[x][y].Options);
                    }
                }
            }

            return true;
        }

        public bool checkForNakedTriples(List<List<Guess>> grid)
        {
            foreach(var row in grid)
            {
                removeRowNakedTriples(row);
            }

            return true;
        }

        private void removeRowNakedTriples(List<Guess> row)
        {
            foreach (var checkCol in row)
            {
                if(checkCol.Options.Count == 3 && howManySubsets(row, checkCol.Options) == 3)
                {
                    foreach (var removeCol in row.Where(c => !c.Options.IsSubsetOf(checkCol.Options)))
                    {
                        foreach(var opt in checkCol.Options)
                        {
                            removeCol.Options.Remove(opt);
                        }
                    }
                }
            }
        }

        private int howManySubsets(List<Guess> row, HashSet<int> options)
        {
            var result = 0;

            foreach(var col in row)
            {
                if (col.Options.IsSubsetOf(options))
                {
                    result++;
                }
            }

            return result;
        }

        public void removeRowNakedPairOptions(List<Guess> row, HashSet<int> pair)
        {
            var foundRowPair = false;

            foundRowPair = row.Where(col => col.Options.SetEquals(pair)).Count() == 2;

            if (foundRowPair)
            {
                foreach (var col in row.Where(g => !g.Options.SetEquals(pair)))
                {
                    foreach(var option in pair)
                    {
                        col.Options.Remove(option);
                    }
                }
            }
        }

        public void removeColumnNakedPairOptions(List<List<Guess>> grid, int columnIndex, HashSet<int> pair)
        {
            var matches = 0;

            for (var i = 0; i < grid.Count; i++)
            {
                if (grid[i][columnIndex].Options.SetEquals(pair))
                {
                    matches++;
                }
            }

            if(matches == 2)
            {
                for (var i = 0; i < grid.Count; i++)
                {
                    if (!grid[i][columnIndex].Options.SetEquals(pair))
                    {
                        foreach (var option in pair)
                        {
                            grid[i][columnIndex].Options.Remove(option);
                        }
                    }
                }
            }
        }

        public void removeNondrantNakedPairOptions(List<List<Guess>> grid, int rowIndex, int columIndex, HashSet<int> pair)
        {
            var nondrantRowIndex = rowIndex / 3;
            var nondrantColumnIndex = columIndex / 3;

            var searchRowIndex = nondrantRowIndex * 3;
            var searchColumnIndex = nondrantColumnIndex * 3;

            var matches = 0;

            for (var i = searchRowIndex; i < searchRowIndex + 3; i++)
            {
                for (var j = searchColumnIndex; j < searchColumnIndex + 3; j++)
                {
                    if (grid[i][j].Options.SetEquals(pair))
                    {
                        matches++;
                    }
                }
            }

            if(matches == 2)
            {
                for (var i = searchRowIndex; i < searchRowIndex + 3; i++)
                {
                    for (var j = searchColumnIndex; j < searchColumnIndex + 3; j++)
                    {
                        if (!grid[i][j].Options.SetEquals(pair) && grid[i][j].Options.Count() > 1)
                        {
                            foreach (var option in pair)
                            {
                                grid[i][j].Options.Remove(option);
                            }
                        }
                    }
                }
            }
        }

        public bool rowContains(List<Guess> row, int value)
        {
            foreach (var guesses in row)
            {
                if (guesses.Options.Count() == 1 && guesses.Options.Single() == value)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Check if nondrant contains value
        /// </summary>
        /// <param name="grid">Grid to search</param>
        /// <param name="i">Index of row in grid</param>
        /// <param name="j">Index of column in grid</param>
        /// <param name="k">Value to search for</param>
        /// <returns></returns>
        private static bool nondrantContains(List<List<Guess>> grid, int rowIndex, int columIndex, int k)
        {
            var nondrantRowIndex = rowIndex / 3;
            var nondrantColumnIndex = columIndex / 3;

            var searchRowIndex = nondrantRowIndex * 3;
            var searchColumnIndex = nondrantColumnIndex * 3;

            for (var i = searchRowIndex; i < searchRowIndex + 3; i++)
            {
                for (var j = searchColumnIndex; j < searchColumnIndex + 3; j++)
                {
                    if (grid[i][j].Options.Count() == 1 && grid[i][j].Options.Single() == k)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// See if column in grid contatain a value;
        /// </summary>
        /// <param name="grid">Grid to search</param>
        /// <param name="i">Index of column</param>
        /// <param name="j">Value to search for</param>
        /// <returns></returns>
        private static bool columnContains(List<List<Guess>> grid, int i, int j)
        {
            foreach (var row in grid)
            {
                if (row[i].Options.Count() == 1 && row[i].Options.Single() == j)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
