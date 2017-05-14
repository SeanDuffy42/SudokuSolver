using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudukoSolverLib.Interfaces
{
    public interface ISudokuSolver
    {
        List<List<HashSet<int>>> SolvePuzzle(string fileName);
    }
}
