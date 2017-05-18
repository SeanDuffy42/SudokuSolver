using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudukoSolverLib.Search.Interfaces
{
    public interface ISearchGrid
    {
        bool basicSearch(List<List<HashSet<int>>> grid);
        bool hiddenSingleAndPairSearch(List<List<HashSet<int>>> grid);
        bool nondrantSearch(List<List<HashSet<int>>> grid);
        bool checkForNakedPairs(List<List<HashSet<int>>> grid);
        bool checkForNakedTriples(List<List<HashSet<int>>> grid);

        bool rowContains(List<HashSet<int>> row, int value);
        void removeRowNakedPairOptions(List<HashSet<int>> row, HashSet<int> pair);
        void removeRowNakedTriples(List<HashSet<int>> row);
        List<HashSet<int>> flattenNondrant(List<List<HashSet<int>>> grid, int x, int y);
        void removePointingRow(List<HashSet<int>> row, int indexOfBox, int need);
    }
}
