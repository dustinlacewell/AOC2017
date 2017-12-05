#load "utils.fsx"

let lookup = [
    ('a', 2); ('b', 3); ('c', 5); ('d', 7); ('e', 11); ('f', 13); ('g', 17); ('h', 19); ('i', 23);
    ('j', 29); ('k', 31); ('l', 37); ('m', 41); ('n', 43); ('o', 47); ('p', 53); ('q', 59); ('r', 61);
    ('s', 67); ('t', 71); ('u', 73); ('v', 79); ('w', 83); ('x', 89); ('y', 97); ('z', 101);] |> dict

let computeHash (s:string) =
    s |> Seq.fold (fun acc c -> acc * lookup.[c]) 1

let isAnagram word1 word2 =
    (computeHash word1) = (computeHash word2)

[<EntryPoint>]
let main argv =
    Utils.readLines "day4.txt"
    |> Seq.map (Utils.split ' ')
    |> Seq.map (exhaust isAnagram)
    |> Seq.filter (fun x -> not x)
    |> Seq.length
    |> printfn "%A"
    0
