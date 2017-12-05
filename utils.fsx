[<AutoOpen>]
module Utils

open System
open System.IO
open System.Collections.Generic


let tokenize (d:char) (x:string) = List.ofSeq (x.Split(' '))

let readLine (sr:StreamReader) = match sr.ReadLine() with
    | null -> sr.Dispose(); None
    | str -> Some(str, sr)

let readLines (filename:string) =
    new StreamReader(filename) |> Seq.unfold readLine

// true if p for any two in hs
let exhaust p (hs:_ list) =
    let rec outer (o:int) =
        let rec inner (i:int) =
            if i = hs.Length then
                false
            else
                p hs.[o] hs.[i] || inner (i+1)

        if o = hs.Length - 1 then
            false
        else
            inner (o+1) || outer (o+1)
    outer 0
