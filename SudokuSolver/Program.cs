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
                if(args[0] == "-p")
                {
                    solveProectEuler(args[1], solver);
                }
                else
                {
                    for (var i = 0; i < args.Length; i++)
                    {
                        filesToSolve.Add(args[i]);
                    }
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

#if DEBUG
            Console.ReadLine();
#endif
        }

        private static void solveProectEuler(string projectEulerFileName, ISudokuSolver solver)
        {
            var file = new StreamReader(projectEulerFileName);

            var line = "";

            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine(line);

                var grid = new List<List<HashSet<int>>>();

                for (var i = 0; i<9;i++)
                {
                    var row = new List<HashSet<int>>();

                    line = file.ReadLine();

                    foreach (var box in line)
                    {
                        if (box == 'X' || box == '0')
                        {
                            row.Add(new HashSet<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                        }
                        else
                        {
                            row.Add(new HashSet<int> { int.Parse(box.ToString()) });
                        }
                    }

                    grid.Add(row);
                }

                var result = solver.SolvePuzzle(grid);

                printGrid(result);
                Console.WriteLine(isValid(result));
            }
        }

        private static bool isValid(List<List<HashSet<int>>> result)
        {
            var total = 0;
            var magicNumber = 405;

            foreach (var row in result)
            {
                foreach (var col in row)
                {
                    foreach(var opt in col)
                    {
                        total += opt;
                    }
                }
            }

            return total == magicNumber;
        }

        public static void printGrid(List<List<HashSet<int>>> grid)
        {
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
