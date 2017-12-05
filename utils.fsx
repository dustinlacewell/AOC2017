[<AutoOpen>]
module Utils

open System
open System.IO
open System.Collections.Generic


let split (d:char) (x:string) = Seq.ofArray (x.Split(d))

let readLine (sr:StreamReader) = match sr.ReadLine() with
                                 | null -> sr.Dispose(); None
                                 | str -> Some(str, sr)

let readLines (filename:string) =
    new StreamReader(filename) |> Seq.unfold readLine

// true if p for any two in hs
let exhaust p (hs:seq<_>) =
    let rec outer (o:int) =
        let rec inner (i:int) =
            if i = Seq.length hs then
                false
            else
                p (Seq.item o hs) (Seq.item i hs) || inner (i+1)

        if o = (Seq.length hs) - 1 then
            false
        else
            inner (o+1) || outer (o+1)
    outer 0
