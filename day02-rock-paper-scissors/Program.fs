open System.IO

type Choice = Rock | Paper | Scissors

type Strategy = Lose | Win | Draw

let choice = function
    | "A" | "X" -> Rock
    | "B" | "Y" -> Paper
    | "C" | "Z" -> Scissors
    | _         -> failwith "error"

let strategy = function
    | "X" -> Lose
    | "Y" -> Draw
    | "Z" -> Win
    | _   -> failwith "error"

let strategyChoice = function
    | x, Draw -> x, x
    | x, Lose -> x, match x with Rock -> Scissors | Paper -> Rock     | Scissors -> Paper
    | x, Win ->  x, match x with Rock -> Paper    | Paper -> Scissors | Scissors -> Rock

let choiceScore = function
    | Rock     -> 1
    | Paper    -> 2
    | Scissors -> 3

let winScore = function
    | (Rock,     Scissors) -> 0
    | (Paper,    Scissors) -> 6
    | (Rock,     Paper)    -> 6
    | (Scissors, Paper)    -> 0
    | (Paper,    Rock)     -> 0
    | (Scissors, Rock)     -> 6
    | _                    -> 3

let input =
    File.ReadAllLines "input.txt"
    |> Seq.ofArray

let convert (input: string seq) fn =
    input
    |> Seq.map (fun s ->
        match s.Split(" ") with
        | [| a; x |] -> choice a,  fn x
        | _ -> failwith "error"
        )

let score game =
    game |> Seq.map (fun (o, y) -> (choiceScore y) + winScore (o, y)) |> Seq.sum

printfn "Part 1: %i" (score (convert input choice))

printfn "Part 2: %i" (score (convert input strategy |> Seq.map strategyChoice))
