#load "utils.fsx"

let rec solve sum pos (tape:int[]) =
    if pos < 0 || pos > (tape.Length-1) then
        sum
    else
        let offset = tape.[pos]
        tape.[pos] <-  offset + if offset > 2 then -1 else 1
        solve (sum + 1) (pos + offset) tape

[<EntryPoint>]
let main argv =
    Utils.readLines "day5.txt"
    |> Seq.map int
    |> Array.ofSeq
    |> solve 0 0
    |> printfn "%A"
    0
