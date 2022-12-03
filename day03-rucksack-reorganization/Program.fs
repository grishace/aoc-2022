open System.IO

let input =
    File.ReadAllLines "input.txt"
    |> Seq.ofArray

let priority = function
    | lc when lc >= 'a' && lc <= 'z' -> int lc - int 'a' + 1
    | uc when uc >= 'A' && uc <= 'Z' -> int uc - int 'A' + 27
    | _ -> failwith "error"

let totalPriority s = s |> Set.map priority |> Set.toSeq |> Seq.sum

let rucksacks =
    input
    |> Seq.map (fun s -> Set.ofSeq (s.Substring(0, s.Length/2)), Set.ofSeq (s.Substring(s.Length/2, s.Length - s.Length/2)))
    |> Seq.map (fun (c1, c2) -> Set.intersect c1 c2)
    |> Seq.map totalPriority
    |> Seq.sum

printfn "Part 1: %i" rucksacks

let badges =
    input
    |> Seq.chunkBySize 3
    |> Seq.map (fun s -> Set.intersect (Set.intersect (Set.ofSeq s.[0]) (Set.ofSeq s.[1])) (Set.ofSeq s.[2]))
    |> Seq.map totalPriority
    |> Seq.sum

printfn "Part 2: %i" badges
