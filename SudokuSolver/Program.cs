﻿using Microsoft.Practices.Unity;
using SudukoSolverLib;
using SudukoSolverLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolverConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Bootstrap.start();

            ISudokuSolver solver = (ISudokuSolver)container.Resolve(typeof(ISudokuSolver));

            var fileName = "puzzle.txt";

            if (args.Length > 1)
            {
                fileName = args[1];
            }

            var grid = solver.SolvePuzzle(fileName);

            foreach (var row in grid)
            {
                foreach (var col in row)
                {
                    if (col.Options.Count() == 1)
                    {
                        Console.Write(col.Options.Single());
                    }
                    else
                    {
                        Console.Write('X');
                    }
                }

                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
