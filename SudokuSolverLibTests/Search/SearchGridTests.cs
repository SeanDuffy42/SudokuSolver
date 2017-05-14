using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudukoSolverLib.Search.Interfaces;
using FakeItEasy;
using System.Collections.Generic;
using SudukoSolverLib.Models;
using SudukoSolverLib.Search;

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
            var row = new List<Guess>
            {
                new Guess
                {
                    Options = new HashSet<int>() {1}
                },
                new Guess
                {
                    Options = new HashSet<int>() {2}
                },
                new Guess
                {
                    Options = new HashSet<int>() {3}
                },
            };

            var result = searchGrid.rowContains(row, 3);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void removeRowNakedPairOptions()
        {
            var row = new List<Guess>
            {
                new Guess()
                {
                    Options = new HashSet<int> {4}
                },
                new Guess()
                {
                    Options = new HashSet<int> {1,6}
                },
                new Guess()
                {
                    Options = new HashSet<int> {1,6}
                },
                new Guess()
                {
                    Options = new HashSet<int> {1,2,5}
                },
                new Guess()
                {
                    Options = new HashSet<int> {1,2,5,6,7}
                },
                new Guess()
                {
                    Options = new HashSet<int> {2,5,6,7}
                },
                new Guess()
                {
                    Options = new HashSet<int> {9}
                },
                new Guess()
                {
                    Options = new HashSet<int> {3}
                },
                new Guess()
                {
                    Options = new HashSet<int> {8}
                },
            };

            searchGrid.removeRowNakedPairOptions(row, new HashSet<int> { 1, 6 });

            Assert.IsTrue(row[0].Options.SetEquals(new HashSet<int> { 4 }));
            Assert.IsTrue(row[1].Options.SetEquals(new HashSet<int> { 1, 6 }));
            Assert.IsTrue(row[2].Options.SetEquals(new HashSet<int> { 1, 6 }));
            Assert.IsTrue(row[3].Options.SetEquals(new HashSet<int> { 2, 5 }));
            Assert.IsTrue(row[4].Options.SetEquals(new HashSet<int> { 2, 5, 7 }));
            Assert.IsTrue(row[5].Options.SetEquals(new HashSet<int> { 2, 5, 7 }));
            Assert.IsTrue(row[6].Options.SetEquals(new HashSet<int> { 9 }));
            Assert.IsTrue(row[7].Options.SetEquals(new HashSet<int> { 3 }));
            Assert.IsTrue(row[8].Options.SetEquals(new HashSet<int> { 8 }));

            searchGrid.removeRowNakedPairOptions(row, new HashSet<int> { 1, 6 });

            Assert.IsTrue(row[0].Options.SetEquals(new HashSet<int> { 4 }));
            Assert.IsTrue(row[1].Options.SetEquals(new HashSet<int> { 1, 6 }));
            Assert.IsTrue(row[2].Options.SetEquals(new HashSet<int> { 1, 6 }));
            Assert.IsTrue(row[3].Options.SetEquals(new HashSet<int> { 2, 5 }));
            Assert.IsTrue(row[4].Options.SetEquals(new HashSet<int> { 2, 5, 7 }));
            Assert.IsTrue(row[5].Options.SetEquals(new HashSet<int> { 2, 5, 7 }));
            Assert.IsTrue(row[6].Options.SetEquals(new HashSet<int> { 9 }));
            Assert.IsTrue(row[7].Options.SetEquals(new HashSet<int> { 3 }));
            Assert.IsTrue(row[8].Options.SetEquals(new HashSet<int> { 8 }));
        }

        [TestMethod]
        public void removeRowNakedPairOptions2()
        {
            var row = new List<Guess>
            {
                new Guess()
                {
                    Options = new HashSet<int> { 3, 5, 9 }
                },
                new Guess()
                {
                    Options = new HashSet<int> { 1, 3 }
                },
                new Guess()
                {
                    Options = new HashSet<int> {1}
                },
                new Guess()
                {
                    Options = new HashSet<int> {1,5}
                },
                new Guess()
                {
                    Options = new HashSet<int> { 1, 3, 4, 5, 8 }
                },
                new Guess()
                {
                    Options = new HashSet<int> { 1, 3, 8, 9 }
                },
                new Guess()
                {
                    Options = new HashSet<int> {6}
                },
                new Guess()
                {
                    Options = new HashSet<int> {2}
                },
                new Guess()
                {
                    Options = new HashSet<int> {7}
                },
            };

            searchGrid.removeRowNakedPairOptions(row, new HashSet<int> { 1, 3 });

            Assert.IsTrue(row[0].Options.SetEquals(new HashSet<int> { 3, 5, 9 }));
            Assert.IsTrue(row[1].Options.SetEquals(new HashSet<int> { 1, 3 }));
            Assert.IsTrue(row[2].Options.SetEquals(new HashSet<int> { 1 }));
            Assert.IsTrue(row[3].Options.SetEquals(new HashSet<int> { 1, 5 }));
            Assert.IsTrue(row[4].Options.SetEquals(new HashSet<int> { 1, 3, 4, 5, 8 }));
            Assert.IsTrue(row[5].Options.SetEquals(new HashSet<int> { 1, 3, 8, 9 }));
            Assert.IsTrue(row[6].Options.SetEquals(new HashSet<int> { 6 }));
            Assert.IsTrue(row[7].Options.SetEquals(new HashSet<int> { 2 }));
            Assert.IsTrue(row[8].Options.SetEquals(new HashSet<int> { 7 }));
        }
        
    }
}
