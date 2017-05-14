using Microsoft.Practices.Unity;
using SudukoSolverLib;
using SudukoSolverLib.Interfaces;
using SudukoSolverLib.Search;
using SudukoSolverLib.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolverConsole
{
    public class Bootstrap
    {
        public static UnityContainer start()
        {
            var container = new UnityContainer();
            container.RegisterType<ISudokuSolver, SudokuSolver>();
            container.RegisterType<ISearchGrid, SearchGrid>();

            return container;
        }
    }
}
