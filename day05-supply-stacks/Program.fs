open System
open System.IO
open System.Text.RegularExpressions

let input =
    File.ReadAllLines "input.txt"

let emptyLine = input |> Array.findIndex (fun s -> String.IsNullOrEmpty(s))

let numberOfStacks = 
    input.[emptyLine-1].Split (' ', StringSplitOptions.RemoveEmptyEntries)
    |> Array.map int
    |> Array.max

let crates ()= [|
    for i in 0 .. (numberOfStacks-1) do
        yield  [
            for l in (emptyLine-2) .. -1 .. 0 do
                let c = input.[l].[i*4+1]
                if c <> ' ' then yield c
        ] |> List.rev
    |]

let rx = Regex("move (\d+) from (\d+) to (\d+)", RegexOptions.Compiled)

let commands  = 
    input.[emptyLine+1..input.Length-1]
    |> Array.map (fun s -> 
        let m = rx.Match s
        int m.Groups.[1].Value, int m.Groups.[2].Value, int m.Groups.[3].Value
        )

let result fn = 
    commands 
    |> Array.fold (fun (c: char list[]) (n, f, t) ->
        let toMove = c.[f-1].[..n-1] |> fn
        let remains = c.[f-1].[n..]
        c.[t-1] <- toMove @ c.[t-1]
        c.[f-1] <- remains
        c
        ) (crates())
    |> Array.map (fun l -> l |> List.head)
    |> String

printfn "Part 1: %s" (result  List.rev)

printfn "Part 2: %s" (result id)