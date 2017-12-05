open System
open System.Collections.Generic

let map = Dictionary<int * int, int>()
let directions = [ (0, 1); (1, 1); (1, 0); (1, -1); (0, -1); (-1, -1); (-1, 0); (-1, 1); ]
let Add ((ax, ay):int * int) ((bx, by): int * int) = (ax + bx, ay + by)

type Walker() =
    let mutable _position = (0, 0)
    let mutable _direction = 0

    member this.Position with get() = _position
    member this.Direction with get() = directions.[_direction]
    member this.Target with get() = Add this.Position this.Direction
    member this.Move() = _position <- this.Target
    member this.Turn() =
        let direction = (_direction + 2) % 8
        let new_target = Add this.Position directions.[direction]
        if not (map.ContainsKey(new_target)) then
            _direction <- direction
    member this.Scan() =
        directions
        |> List.map (fun d -> match map.TryGetValue (Add this.Position d) with
                     | true, value -> value
                     | _ -> 0)
        |> List.sum


let rec walk (walker:Walker, target) =
    walker.Turn()
    walker.Move()
    let total = walker.Scan()
    map.Add(walker.Position, total)
    printfn "Wrote %A at %A" total walker.Position
    if total > target then
        total
    else
        walk(walker, target)

[<EntryPoint>]
let main argv =
    map.Add((0, 0), 1)
    let target = 368078
    let solution = walk(Walker(), target)
    printfn "%A" solution
    0
