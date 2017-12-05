open System
open System.IO
open System.Collections.Generic

let tokenize (x:string) = List.ofSeq (x.Split(' '))

let readLine (sr:StreamReader) = match sr.ReadLine() with
    | null -> sr.Dispose(); None
    | str -> Some(str, sr)

let readLines (filename:string) =
    new StreamReader(filename) |> Seq.unfold readLine

let validate tokens =
    tokens
    |> Seq.countBy id
    |> Seq.toList
    |> Seq.filter (fun (x, n) -> n > 1)
    |> Seq.length
    |> (fun x -> x = 0)


[<EntryPoint>]
let main argv =
    readLines "day4.txt"
    |> Seq.map (tokenize >> validate)
    |> Seq.filter (fun x -> x)
    |> Seq.length
    |> printfn "%A"
    0
