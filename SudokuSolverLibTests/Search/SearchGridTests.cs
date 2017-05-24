using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudukoSolverLib.Search.Interfaces;
using FakeItEasy;
using System.Collections.Generic;
using SudukoSolverLib.Search;
using System.Linq;

namespace SudokuSolverLibTests.Search
{
    [TestClass]
    public class SearchGridTests
    {
        private ISearchGrid searchGrid;

        [TestInitialize]
        public void Init()
        {
            searchGrid = A.Fake<SearchGrid>(o => o.CallsBaseMethods());
        }

        [TestMethod]
        public void rowContainsTest_true()
        {
            var row = new List<HashSet<int>>
            {
                new HashSet<int>() {1},
                new HashSet<int>() {2},
                new HashSet<int>() {3}
            };

            var result = searchGrid.rowContains(row, 3);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void removeRowNakeTriples()
        {
            var row = new List<HashSet<int>>
            {
                new HashSet<int> { 4,5,6,7,9 },
                new HashSet<int> { 2 },
                new HashSet<int> { 1,5,6,7,9 },
                new HashSet<int> { 5,8,9 },
                new HashSet<int> { 5,8 },
                new HashSet<int> { 5,9 },
                new HashSet<int> { 3,5,8,9 },
                new HashSet<int> { 3,4,5,8,9 },
                new HashSet<int> { 1,6 },
            };

            searchGrid.removeRowNakeds(row, 3);

            Assert.IsTrue(row[0].SetEquals(new HashSet<int> { 4, 6, 7 }));
            Assert.IsTrue(row[1].SetEquals(new HashSet<int> { 2 }));
            Assert.IsTrue(row[2].SetEquals(new HashSet<int> { 1, 6, 7 }));
            Assert.IsTrue(row[3].SetEquals(new HashSet<int> { 5, 8, 9 }));
            Assert.IsTrue(row[4].SetEquals(new HashSet<int> { 5, 8 }));
            Assert.IsTrue(row[5].SetEquals(new HashSet<int> { 5, 9 }));
            Assert.IsTrue(row[6].SetEquals(new HashSet<int> { 3 }));
            Assert.IsTrue(row[7].SetEquals(new HashSet<int> { 3, 4 }));
            Assert.IsTrue(row[8].SetEquals(new HashSet<int> { 1, 6 }));
        }

        [TestMethod]
        public void flattenNondrantTest()
        {
            var grid = new List<List<HashSet<int>>>
            {
                new List<HashSet<int>>
                {
                    new HashSet<int> { 3, 6 },
                    new HashSet<int> { 7 },
                    new HashSet<int> { 1, 6 },
                    new HashSet<int> { 4 },
                    new HashSet<int> { 1, 3, 5},
                    new HashSet<int> { 8 }
                },
                new List<HashSet<int>>
                {
                    new HashSet<int> { 3, 6, 9 },
                    new HashSet<int> { 1, 6, 9},
                    new HashSet<int> { 2 },
                    new HashSet<int> { 1,5,7,9},
                    new HashSet<int> { 1,3,5},
                    new HashSet<int> { 7,5,6,9},
                },
                new List<HashSet<int>>
                {
                    new HashSet<int> { 8},
                    new HashSet<int> { 5},
                    new HashSet<int> { 4},
                    new HashSet<int> { 1,9},
                    new HashSet<int> { 2},
                    new HashSet<int> { 6,9},

                },
                new List<HashSet<int>>
                {
                    new HashSet<int> { 5,6,9},
                    new HashSet<int> { 1,6,9},
                    new HashSet<int> { 8},
                    new HashSet<int> { 3},
                    new HashSet<int> { 7},
                    new HashSet<int> { 4},
                },
                new List<HashSet<int>>
                {
                    new HashSet<int> { 4,5,6,7,9},
                    new HashSet<int> { 2},
                    new HashSet<int> { 1,5,6,7,9},
                    new HashSet<int> { 5,8,9},
                    new HashSet<int> { 5,8},
                    new HashSet<int> { 5,9},
                },
                new List<HashSet<int>>
                {
                    new HashSet<int> { 4,5,9},
                    new HashSet<int> { 4,9},
                    new HashSet<int> { 3},
                    new HashSet<int> { 2},
                    new HashSet<int> { 6},
                    new HashSet<int> { 1},
                }
            };

            var expected = new List<HashSet<int>>
            {
                new HashSet<int> { 3, 6 },
                new HashSet<int> { 7 },
                new HashSet<int> { 1, 6 },
                new HashSet<int> { 3, 6, 9},
                new HashSet<int> { 1,6,9},
                new HashSet<int> { 2},
                new HashSet<int> { 8},
                new HashSet<int> { 5},
                new HashSet<int> { 4},
            };

            var flatNondrant = searchGrid.flattenNondrant(grid, 0, 0);

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.IsTrue(flatNondrant[i].SetEquals(expected[i]));
            }
        }

        [TestMethod]
        public void removePointingParis()
        {
            var row = new List<HashSet<int>>
            {
                new HashSet<int> { 7 },
                new HashSet<int> { 2, 3, 4 },
                new HashSet<int> { 1,2,4,6,9 },
                new HashSet<int> { 5 },
                new HashSet<int> { 4,9 },
                new HashSet<int> { 8 },
                new HashSet<int> { 1,4,6,9 },
                new HashSet<int> { 3,4,6 },
                new HashSet<int> { 3,6,9 },
            };

            searchGrid.removePointingRow(row, 2, 3);

            Assert.IsTrue(row[0].SetEquals(new HashSet<int> { 7 }));
            Assert.IsTrue(row[1].SetEquals(new HashSet<int> { 2, 4 }));
            Assert.IsTrue(row[2].SetEquals(new HashSet<int> { 1, 2, 4, 6, 9 }));
            Assert.IsTrue(row[3].SetEquals(new HashSet<int> { 5 }));
            Assert.IsTrue(row[4].SetEquals(new HashSet<int> { 4, 9 }));
            Assert.IsTrue(row[5].SetEquals(new HashSet<int> { 8 }));
            Assert.IsTrue(row[6].SetEquals(new HashSet<int> { 1, 4, 6, 9 }));
            Assert.IsTrue(row[7].SetEquals(new HashSet<int> { 3, 4, 6 }));
            Assert.IsTrue(row[8].SetEquals(new HashSet<int> { 3, 6, 9 }));
        }
    }
}
