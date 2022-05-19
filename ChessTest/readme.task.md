# Task

- Create a class that implements the IAttackCounter interface
- The CountUnderAttack method should count the number of squares of the chessboard under attack by the chess piece
- At the moment there are three pieces to be supported - Rook, Bishop and Knight
- The board size is set by the boardSize argument
- The number of obstacles and the size of the board can reach 100,000 elements
- the starting point of the figure for which we count the number of cells under attack - startCoords
- coordinates of obstacles are in the obstacles array
- all coordinates are counted starting from 1 from the lower left corner
- it is possible (and desirable) to add additional tests for verification to AttackSolverTest

# Examples

## Legend

O - obstacle
R - Rook
B - Bishop
K - Knight
... - empty cell

## Rook

cmType = Rook, boardSize = {3,2}, startCoords = {1, 1}, obstacles = {{2,2}, {3, 1}}

``,
.O.
R.O
``,

output: 2

## Bishop

cmType = Bishop, boardSize = {4,5}, startCoords = {2, 2}, obstacles = {{1,1}, {1, 3}}

``,
....
....
O ...
.B ..
O ...
``,

output: 3