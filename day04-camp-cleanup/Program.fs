open System.IO

let input =
    File.ReadAllLines "input.txt"
    |> Seq.ofArray

let ranges =
    input
    |> Seq.map (fun s -> s.Split ",")
    |> Seq.map (fun x ->
        let toRange (s: string) = s.Split "-" |> Array.map int |> (fun x -> seq { for i in x.[0] .. x.[1] do i } |> Set.ofSeq)
        toRange x.[0], toRange x.[1]
        )
    |> Seq.map (fun (s1, s2) ->
        let overlap = Set.intersect s1 s2
        overlap, s1, s2
        )

let assignments =
    ranges |> Seq.filter (fun (o, s1, s2) -> o.Count = s1.Count || o.Count = s2.Count)

printfn "Part 1: %i" <| Seq.length assignments

let overlaps =
    ranges |> Seq.filter (fun (o, _, _) -> o.Count > 0)

printfn "Part 2: %i"  <| Seq.length overlaps
