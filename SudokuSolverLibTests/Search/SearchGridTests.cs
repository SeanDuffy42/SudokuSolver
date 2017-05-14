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
        public void removeRowNakedPairOptions()
        {
            var row = new List<HashSet<int>>
            {
                new HashSet<int> {4},
                new HashSet<int> {1,6},
                new HashSet<int> {1,6},
                new HashSet<int> {1,2,5},
                new HashSet<int> {1,2,5,6,7},
                new HashSet<int> {2,5,6,7},
                new HashSet<int> {9},
                new HashSet<int> {3},
                new HashSet<int> {8}
            };

            searchGrid.removeRowNakedPairOptions(row, new HashSet<int> { 1, 6 });

            Assert.IsTrue(row[0].SequenceEqual(new HashSet<int> { 4 }));
            Assert.IsTrue(row[1].SequenceEqual(new HashSet<int> { 1, 6 }));
            Assert.IsTrue(row[2].SequenceEqual(new HashSet<int> { 1, 6 }));
            Assert.IsTrue(row[3].SequenceEqual(new HashSet<int> { 2, 5 }));
            Assert.IsTrue(row[4].SequenceEqual(new HashSet<int> { 2, 5, 7 }));
            Assert.IsTrue(row[5].SequenceEqual(new HashSet<int> { 2, 5, 7 }));
            Assert.IsTrue(row[6].SequenceEqual(new HashSet<int> { 9 }));
            Assert.IsTrue(row[7].SequenceEqual(new HashSet<int> { 3 }));
            Assert.IsTrue(row[8].SequenceEqual(new HashSet<int> { 8 }));

            searchGrid.removeRowNakedPairOptions(row, new HashSet<int> { 1, 6 });

            Assert.IsTrue(row[0].SequenceEqual(new HashSet<int> { 4 }));
            Assert.IsTrue(row[1].SequenceEqual(new HashSet<int> { 1, 6 }));
            Assert.IsTrue(row[2].SequenceEqual(new HashSet<int> { 1, 6 }));
            Assert.IsTrue(row[3].SequenceEqual(new HashSet<int> { 2, 5 }));
            Assert.IsTrue(row[4].SequenceEqual(new HashSet<int> { 2, 5, 7 }));
            Assert.IsTrue(row[5].SequenceEqual(new HashSet<int> { 2, 5, 7 }));
            Assert.IsTrue(row[6].SequenceEqual(new HashSet<int> { 9 }));
            Assert.IsTrue(row[7].SequenceEqual(new HashSet<int> { 3 }));
            Assert.IsTrue(row[8].SequenceEqual(new HashSet<int> { 8 }));
        }

        [TestMethod]
        public void removeRowNakedPairOptions2()
        {
            var row = new List<HashSet<int>>
            {
                new HashSet<int> {3,5,9},
                new HashSet<int> {1,3},
                new HashSet<int> {1},
                new HashSet<int> {1,5},
                new HashSet<int> {1, 3, 4, 5, 8},
                new HashSet<int> {1, 3, 8, 9},
                new HashSet<int> {6},
                new HashSet<int> {2},
                new HashSet<int> {7}
            };

            searchGrid.removeRowNakedPairOptions(row, new HashSet<int> { 1, 3 });

            Assert.IsTrue(row[0].SequenceEqual(new HashSet<int> { 3, 5, 9 }));
            Assert.IsTrue(row[1].SequenceEqual(new HashSet<int> { 1, 3 }));
            Assert.IsTrue(row[2].SequenceEqual(new HashSet<int> { 1 }));
            Assert.IsTrue(row[3].SequenceEqual(new HashSet<int> { 1, 5 }));
            Assert.IsTrue(row[4].SequenceEqual(new HashSet<int> { 1, 3, 4, 5, 8 }));
            Assert.IsTrue(row[5].SequenceEqual(new HashSet<int> { 1, 3, 8, 9 }));
            Assert.IsTrue(row[6].SequenceEqual(new HashSet<int> { 6 }));
            Assert.IsTrue(row[7].SequenceEqual(new HashSet<int> { 2 }));
            Assert.IsTrue(row[8].SequenceEqual(new HashSet<int> { 7 }));
        }

        [TestMethod]
        public void checkForNakedPairs_one()
        {
            var grid = new List<List<HashSet<int>>>
            {
                new List<HashSet<int>>
                {
                    new HashSet<int> {1,2},
                    new HashSet<int>(),
                    new HashSet<int>()
                },
                new List<HashSet<int>>
                {
                    new HashSet<int>(),
                    new HashSet<int>(),
                    new HashSet<int>()
                },
                new List<HashSet<int>>
                {
                    new HashSet<int>(),
                    new HashSet<int>(),
                    new HashSet<int>()
                },
            };

            A.CallTo(() => searchGrid.removeRowNakedPairOptions(A<List<HashSet<int>>>._, A<HashSet<int>>._)).DoesNothing();

            searchGrid.checkForNakedPairs(grid);

            A.CallTo(() => searchGrid.removeRowNakedPairOptions(A<List<HashSet<int>>>._, A<HashSet<int>>._)).MustHaveHappened(Repeated.Exactly.Times(3));
        }

        [TestMethod]
        public void checkForNakedPairs_none()
        {
            var grid = new List<List<HashSet<int>>>
            {
                new List<HashSet<int>>
                {
                    new HashSet<int> { 1 },
                    new HashSet<int>(),
                    new HashSet<int>()
                },
                new List<HashSet<int>>
                {
                    new HashSet<int>(),
                    new HashSet<int>(),
                    new HashSet<int>()
                },
                new List<HashSet<int>>
                {
                    new HashSet<int>(),
                    new HashSet<int>(),
                    new HashSet<int>()
                },
            };

            A.CallTo(() => searchGrid.removeRowNakedPairOptions(A<List<HashSet<int>>>._, A<HashSet<int>>._)).DoesNothing();

            searchGrid.checkForNakedPairs(grid);

            A.CallTo(() => searchGrid.removeRowNakedPairOptions(A<List<HashSet<int>>>._, A<HashSet<int>>._)).MustNotHaveHappened();
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

            searchGrid.removeRowNakedTriples(row);

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
    }
}
