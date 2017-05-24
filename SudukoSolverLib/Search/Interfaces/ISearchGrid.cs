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
        bool hiddenSinglesSearch(List<List<HashSet<int>>> grid);
        bool nondrantSearch(List<List<HashSet<int>>> grid);
        bool checkForNakedPairsAndTriples(List<List<HashSet<int>>> grid);

        void removeRowNakeds(List<HashSet<int>> row, int n);
        bool rowContains(List<HashSet<int>> row, int value);
        List<HashSet<int>> flattenNondrant(List<List<HashSet<int>>> grid, int x, int y);
        void removePointingRow(List<HashSet<int>> row, int indexOfBox, int need);
    }
}
