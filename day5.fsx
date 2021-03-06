﻿#load "utils.fsx"

let rec solve sum pos (tape:Map<int,int>) =
    match tape.TryFind pos with
        | Some x ->
            tape.Add (pos, (x + 1))
            |> solve (sum + 1) (pos + x)
        | None -> sum

[<EntryPoint>]
let main argv =
    Utils.readLines "day5.txt"
    |> Seq.map int
    |> Seq.mapi (fun i x -> (i, x))
    |> Map.ofSeq
    |> solve 0 0
    |> printfn "%A"
    0
