// Simple Sudoku Solver
// Adam Davidson
// 11th August 2020

open System.Diagnostics

/// Data Structures

type Cell = Known of int | Possible of Set<int>
type Grid = Cell[,]

/// Printing text grids
        
let printPossible (possible:Set<int>) =
    for v in possible do
        printf "%i" v
    for i in 0..(8-possible.Count) do
        printf "%s" " "

let printCell cell =
    match cell with 
    | Known known -> printf "    %i    " known
    | Possible possible -> printPossible possible
    printf "%s" "|"

let printGrid (grid:Grid) =
    for r=0 to 8 do
        for c=0 to 8 do
            printCell grid.[r,c]
        printf "%s" "\n"
        for c=0 to 8 do
            printf "%s" "---------+"
        printf "%s" "\n"

/// Solving Sudoku Problem

let anything = set { 1..9 }

let createEmptyGrid size : Grid =
    Array2D.create size size (Possible anything)

exception SolutionFound of Grid
exception SolutionNotFound

let removeFromPossible (grid:Grid) r c v =
    let cell = grid.[r,c]
    match cell with
    | Possible possible -> 
        let newSet = Set.remove v possible
        if newSet.Count = 0 then
            raise SolutionNotFound
        grid.[r,c] <- Possible newSet
    | Known _ -> ()

let setKnown (grid:Grid) r c v =
    // Set known cell
    Array2D.set grid r c (Known v)

    // Remove from row possible values
    for c' in 0..8 do
        removeFromPossible grid r c' v

    // Remove from col possible values
    for r' in 0..8 do
        removeFromPossible grid r' c  v

    // Remove from square possible values
    let r' = (r/3)*3
    let c' = (c/3)*3
    for r' in r'..r'+2 do
        for c' in c'..c'+2 do
            removeFromPossible grid r' c' v        

let rec solveGrid (grid:Grid) =
    for r=0 to 8 do
        for c=0 to 8 do
            match grid.[r,c] with
            | Possible possible ->
                for v in possible do
                    let copy = Array2D.copy grid
                    try 
                        setKnown copy r c v
                        solveGrid copy
                    with
                    | SolutionNotFound -> ()
                raise SolutionNotFound
            | Known known -> ()
    raise (SolutionFound grid)

/// Parsing text grids
    
let initGrid (cells:int option[]) =
    let grid = createEmptyGrid 9
    for r=0 to 8 do
        for c=0 to 8 do
            match cells.[r*9+c] with
            | Some v -> 
                setKnown grid r c v
            | None -> ()   
    grid
        
let parseCell cell = 
    match cell with 
    | cell when cell >= '1' && cell <= '9' -> Some (int cell - int '0')
    | _ -> None
    
let parseGrid text = 
    if (String.length text) <> 81 then failwith "text input should be 81 chars"
    initGrid (Seq.toArray (Seq.map parseCell text))
   
/// Test problems

let problem1 = "\
    .....2.1.\
    ..98..35.\
    .5..6..4.\
    .4....1..\
    ...9..8..\
    19...5...\
    .3..5....\
    ...2.1...\
    .2.634..."

let problem2 = "\
    .9.7.....\
    3..56..19\
    .85..96..\
    1....7.45\
    .3.....8.\
    84.2....6\
    ..83..42.\
    91..72..3\
    .....1.9."

let problem3 = "\
    .....9.5.\
    ....58..3\
    .52..4.1.\
    6..3..8..\
    5.......7\
    ..9..5..6\
    .4.8..76.\
    3..57....\
    .6.9....."

let problem4 = "\
    .........\
    .........\
    .........\
    .........\
    .........\
    .........\
    .........\
    .........\
    ........."

/// Execute tests

let solveProblem problemText =
    let problem = parseGrid problemText 

    let timer = new Stopwatch()
    timer.Start()

    try
        solveGrid problem
    with
    | SolutionFound solution ->
        printf "Grid is solved:\n"
        printGrid solution
    | SolutionNotFound -> 
        printf "Grid cannot be solved:\n"
        printGrid problem

    timer.Stop()
    printf "%f\n" timer.Elapsed.TotalSeconds

[<EntryPoint>]
let main argv =
    solveProblem problem1
    solveProblem problem2
    solveProblem problem3
    solveProblem problem4
    
    0 // return an integer exit code
