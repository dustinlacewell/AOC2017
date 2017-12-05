﻿open System
open System.IO
open System.Collections.Generic

#load "utils.fsx"

let isValid (phrase:string) =
    not (phrase
    |> Utils.tokenize ' '
    |> Seq.countBy id
    |> Seq.exists (fun (x, n) -> n > 1))

[<EntryPoint>]
let main argv =
    Utils.readLines "day4.txt"
    |> Seq.filter isValid
    |> Seq.length
    |> printfn "%A"
    0
