open System
open System.IO

let input =
    File.ReadAllLines "input.txt"
    |> Seq.ofArray

let split input =
  let mutable i = 0
  input
  |> Seq.map  (fun x ->
    if x = "" then i <- i + 1
    i, x)
  |> Seq.groupBy fst
  |> Seq.map (fun (_, b) -> Seq.map snd b |> Seq.filter (fun x -> x <> "") |> Seq.map int)

let calories = input |> split |> Seq.map (Seq.sum)

printfn "Part 1: %i" (calories |> Seq.max)

printfn "Part 2: %i" (calories |> Seq.sortDescending |> Seq.take 3 |> Seq.sum)
