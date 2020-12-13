# SudokuSolver
Sudoku solver in F#. This version currently uses exceptions for control flow, which is frowned upon in the F# world. Next I'll attempt to change it to use a mondaic Result construct.

## Data Structures

```
type Cell = Known of int | Possible of Set<int>
type Grid = Cell[,]
```

## Algorithm

To attempt to solve a problem, an initial board state and a `makeMove` function are supplied. The `makeMove` function sets a value in a cell to a `Known` integer and updates all the `Possible` sets of integers for cells affected by the particular game rule.

`makeSudokuMove` is used for standard Sudoku.

## Miracle Sudoku

`makeMiracleSudoku` move is used for the [Miracle Sudoku](https://www.youtube.com/watch?v=yKf9aUIxdb4) problem by [Mitchell Lee](https://www.theguardian.com/science/2020/may/18/can-you-solve-it-sudoku-as-spectator-sport-is-unlikely-lockdown-hit).
 
![](docs/MiracleSudoku.png)

## Performance

This table shows the performance results running the tests a single time in Release on my [Huaweii Matebook X Pro](https://www.amazon.co.uk/gp/product/B07KCJGGW3).
For all tests the smaller time is better (faster).
This compares `solveGrid` which visits cells from top left to bottom right
vs `solveGrid2` which orders the solver by the fewest number of remaining possibilities in each cell.

| Test | Problem |solveGrid Time | solveGrid2 Time |
|---|-----------|-----------|-----|
| 1 | <img src="https://gameboardservice.azurewebsites.net/sudoku/.....2.1...98..35..5..6..4..4....1.....9..8..19...5....3..5.......2.1....2.634....svg" width="200" /> | 0.091504s | 0.073380s | 
| 2 | <img src="https://gameboardservice.azurewebsites.net/sudoku/.9.7.....3..56..19.85..96..1....7.45.3.....8.84.2....6..83..42.91..72..3.....1.9..svg" width="200" /> | 0.033452s | 0.026237s |
| 3 | <img src="https://gameboardservice.azurewebsites.net/sudoku/.....9.5.....58..3.52..4.1.6..3..8..5.......7..9..5..6.4.8..76.3..57.....6.9......svg" width="200" /> | 0.065200s | 0.061731s |
| 4 | <img src="https://gameboardservice.azurewebsites.net/sudoku/0.svg" width="200" /> <br> Empty Problem | 0.040063s | 0.036557 |
| 5 | <img src="https://gameboardservice.azurewebsites.net/sudoku/0........0........0........0........0.1.....0......2.svg" width="200" /> <br> Miracle Sudoku | 15.329430s | 0.119469s |



