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
When run without any arguments the app will use the "defaultFileName" which is currently set to "puzzle.txt".

The app can also be supplied a file name as a first argument.
