open System.IO

let input =
    File.ReadAllLines "input.txt"
    |> Seq.head

let detect n input =
    input
    |> Seq.windowed n
    |> Seq.findIndex (fun s -> s |> Set.ofArray |> (fun c -> c.Count = n))
    |> (+) n

let signal = detect 4

printfn "Part 1: %i" (signal input)

let message = detect 14

printfn "Part 2: %i" (message input)
