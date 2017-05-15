using Microsoft.Practices.Unity;
using SudukoSolverLib;
using SudukoSolverLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SudokuSolverConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Bootstrap.start();

            ISudokuSolver solver = (ISudokuSolver)container.Resolve(typeof(ISudokuSolver));

            var fileName = ConfigurationManager.AppSettings["defaultFileName"];

            var filesToSolve = new List<string>();

            if (args.Length > 0)
            {
                for (var i = 0; i < args.Length; i++)
                {
                    filesToSolve.Add(args[i]);
                }
            }
            else
            {
                filesToSolve.Add(fileName);
            }

            foreach(var fileToSolve in filesToSolve)
            {
                var grid = solver.SolvePuzzle(fileToSolve);

                foreach (var row in grid)
                {
                    foreach (var col in row)
                    {
                        if (col.Count == 1)
                        {
                            Console.Write(col.Single());
                        }
                        else
                        {
                            Console.Write('X');
                        }
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
            }
        }
    }
}
