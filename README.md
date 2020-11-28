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

| Test | Time (smaller is better) | Notes |
|---|----------|-------|
| 1 | 0.091504s | |
| 2 | 0.033452s | |
| 3 | 0.065200s | |
| 4 | 0.040063s | Empty Sudoku |
| 5 | 15.329430s | Miracle Sudoku |



