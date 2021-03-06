﻿using SudukoSolverLib.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudukoSolverLib.Search
{
    public class SearchGrid : ISearchGrid
    {
        public bool basicSearch(List<List<HashSet<int>>> grid)
        {
            var foundValue = false;

            for (var x = 0; x < grid.Count; x++)
            {
                for (var y = 0; y < grid[x].Count; y++)
                {
                    if (grid[x][y].Count > 1)
                    {
                        for (var value = 1; value <= grid.Count; value++)
                        {
                            //Check Row
                            var rowContainsResult = rowContains(grid[x], value);

                            //Check Column
                            var colummContainsResult = columnContains(grid, y, value);

                            //Check Nondrant
                            var nondrantResult = nondrantContains(grid, x, y, value);

                            if (rowContainsResult || colummContainsResult || nondrantResult)
                            {
                                grid[x][y].Remove(value);

                                if (grid[x][y].Count == 1)
                                {
                                    foundValue = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return foundValue;
        }

        public bool hiddenSinglesSearch(List<List<HashSet<int>>> grid)
        {
            foreach(var row in grid)
            {
                findRowHiddenSigles(row);
            }

            for(var i = 0; i<grid.Count; i++)
            {
                var pivotRow = pivotColumn(grid, i);
                findRowHiddenSigles(pivotRow);
            }

            return true;
        }

        public bool findRowHiddenSigles(List<HashSet<int>> row)
        {
            var rowNeeds = GetRowNeeds(row);

            foreach (var need in rowNeeds)
            {
                var placesWeCouldPutNeed = new List<HashSet<int>>();

                foreach(var col in row)
                {
                    if (col.Count != 1 && col.Contains(need))
                    {
                        placesWeCouldPutNeed.Add(col);
                    }
                }

                if(placesWeCouldPutNeed.Count == 1)
                {
                    placesWeCouldPutNeed.Single().Clear();
                    placesWeCouldPutNeed.Single().Add(need);
                }
            }

            return true;
        }

        public bool nondrantSearch(List<List<HashSet<int>>> grid)
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
                            if (grid[x][y].Count == 1)
                            {
                                nondrantNeeds.Remove(grid[x][y].Single());
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
                                if (grid[x][y].Count != 1 && !rowContains(grid[x], need) && !columnContains(grid, y, need) && !nondrantContains(grid, x, y, need) && grid[x][y].Contains(need))
                                {
                                    placesWeCouldPutNeededValue.Add(new Tuple<int, int>(x, y));
                                }
                            }
                        }

                        if (placesWeCouldPutNeededValue.Count == 1)
                        {
                            grid[placesWeCouldPutNeededValue.Single().Item1][placesWeCouldPutNeededValue.Single().Item2].Clear();
                            grid[placesWeCouldPutNeededValue.Single().Item1][placesWeCouldPutNeededValue.Single().Item2].Add(need);

                            foundValue = true;
                        }
                        else if (placesWeCouldPutNeededValue.Count == 2 || placesWeCouldPutNeededValue.Count == 3)
                        {
                            var firstX = placesWeCouldPutNeededValue.First().Item1;
                            var firstY = placesWeCouldPutNeededValue.First().Item2;

                            if (placesWeCouldPutNeededValue.All(p => p.Item1 == firstX))
                            {
                                removePointingRow(grid[firstX], j, need);
                            }
                            else if (placesWeCouldPutNeededValue.All(p => p.Item2 == firstY))
                            {
                                var pivotRow = pivotColumn(grid, firstY);
                                removePointingRow(pivotRow, i, need);
                            }
                        }
                    }
                }
            }

            return foundValue;
        }

        public void removePointingRow(List<HashSet<int>> row, int indexOfBox, int need)
        {
            for(var i = 0; i<row.Count; i++)
            {
                if(i/3 != indexOfBox)
                {
                    row[i].Remove(need);
                }
            }
        }

        private bool nondrantContains(List<List<HashSet<int>>> grid, int x, int y, int value)
        {
            var flatNondrant = flattenNondrant(grid, x, y);
            return rowContains(flatNondrant, value);
        }

        private bool columnContains(List<List<HashSet<int>>> grid, int y, int value)
        {
            var pivotedColumn = pivotColumn(grid, y);
            return rowContains(pivotedColumn, value);
        }

        public bool checkForNakedPairsAndTriples(List<List<HashSet<int>>> grid)
        {
            foreach (var row in grid)
            {
                removeRowNakeds(row, 2);
                removeRowNakeds(row, 3);
            }

            for (var columnIndex = 0; columnIndex < grid.First().Count; columnIndex++)
            {
                var pivotedColumn = pivotColumn(grid, columnIndex);
                removeRowNakeds(pivotedColumn, 2);
                removeRowNakeds(pivotedColumn, 3);
            }

            for (var x = 0; x < 3; x++)
            {
                for(var y = 0; y < 3; y++)
                {
                    var flatNondrant = flattenNondrant(grid, x, y);
                    removeRowNakeds(flatNondrant, 2);
                    removeRowNakeds(flatNondrant, 3);
                }
            }

            return true;
        }

        public List<HashSet<int>> flattenNondrant(List<List<HashSet<int>>> grid, int x, int y)
        {
            var flatNondrant = new List<HashSet<int>>();

            var searchRowIndex = (x/3) * 3;
            var searchColumnIndex = (y/3) * 3;

            for (var i = searchRowIndex; i < searchRowIndex + 3; i++)
            {
                for (var j = searchColumnIndex; j < searchColumnIndex + 3; j++)
                {
                    flatNondrant.Add(grid[i][j]);
                }
            }

            return flatNondrant;
        }

        private List<HashSet<int>> pivotColumn(List<List<HashSet<int>>> grid, int columnIndex)
        {
            var pivotedColumn = new List<HashSet<int>>();

            foreach (var row in grid)
            {
                pivotedColumn.Add(row[columnIndex]);
            }

            return pivotedColumn;
        }

        public void removeRowNakeds(List<HashSet<int>> row, int n)
        {
            foreach (var checkCol in row)
            {
                if (checkCol.Count == n && howManySubsets(row, checkCol) == n)
                {
                    foreach (var removeCol in row.Where(c => !c.IsSubsetOf(checkCol)))
                    {
                        foreach (var opt in checkCol)
                        {
                            removeCol.Remove(opt);
                        }
                    }
                }
            }
        }

        private int howManySubsets(List<HashSet<int>> row, HashSet<int> options)
        {
            var result = 0;

            foreach (var col in row)
            {
                if (col.IsSubsetOf(options))
                {
                    result++;
                }
            }

            return result;
        }

        public bool rowContains(List<HashSet<int>> row, int value)
        {
            foreach (var box in row)
            {
                if (box.Count == 1 && box.Single() == value)
                    return true;
            }

            return false;
        }

        public List<int> GetRowNeeds(List<HashSet<int>> row)
        {
            var rowNeeds = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            foreach (var col in row)
            {
                if (col.Count == 1)
                {
                    rowNeeds.Remove(col.Single());
                }
            }

            return rowNeeds;
        }
    }
}
