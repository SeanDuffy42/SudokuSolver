# SudokuSolver
This is a C# console app that solves Sudoku Puzzles

## User Guide
This application will read in a text file which represents a Sudoku puzzle that has the following format and print the solution to the console.

XXX15XX7X<br>
1X6XXX82X<br>
3XX86XX4X<br>
9XX4XX567<br>
XX47X83XX<br>
732XX6XX4<br>
X4XX81XX9<br>
X17XXX2X8<br>
X5XX37XXX<br>

Where an X represents an empty box.

## Arguments
When run without any arguments the app will use the "defaultFileName" app setting in the config gile as the source file, which is currently set to "puzzle.txt".

The app can also be run with n number of file names to solve.

### Project Euler Mode
When given the -p flag the app will run in Project Euler Mode.

For reference: https://projecteuler.net/problem=96

The second argument will need to be the name of a file which has the same format as: https://projecteuler.net/project/resources/p096_sudoku.txt

The app will then print to the console the solution to each puzzle in the file and whether the solution is valid.

## Algorithms

### Implemented
-Basic Search<br>
-Hidden Singles<br>
-Naked Pairs/Triples<br>

### TODO
-Pointing Pairs<br>
-Box/Line Reduction<br>

NOTE: As all base level Algorithems have not been implemented this currently cannot solve all puzzles.

See http://www.sudokuwiki.org for reference.

