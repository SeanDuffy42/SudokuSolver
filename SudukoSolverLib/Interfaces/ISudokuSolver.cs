using SudukoSolverLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudukoSolverLib.Interfaces
{
    public interface ISudokuSolver
    {
        List<List<Guess>> SolvePuzzle(string fileName);
    }
}
