using SudukoSolverLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudukoSolverLib.Search.Interfaces
{
    public interface ISearchGrid
    {
        bool basicSearch(List<List<Guess>> grid);
        bool nondrantSearch(List<List<Guess>> grid);
        bool checkForNakedPairs(List<List<Guess>> grid);
        bool checkForNakedTriples(List<List<Guess>> grid);

        bool rowContains(List<Guess> row, int value);
        void removeRowNakedPairOptions(List<Guess> row, HashSet<int> pair);
    }
}
