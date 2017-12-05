open System
open System.Collections.Generic

let map = Dictionary<int * int, int>()
let directions = [ (0, 1); (1, 0); (0, -1); (-1, 0); ]
let Add ((ax, ay):int * int) ((bx, by): int * int) = (ax + bx, ay + by)

type Walker() =
    let mutable _position = (0, 0)
    let mutable _direction = 0

    member this.Position with get() = _position
    member this.Direction with get() = directions.[_direction]
    member this.Target with get() = Add this.Position this.Direction
    member this.Move() = _position <- this.Target
    member this.Turn() =
        let direction = (_direction + 1) % 4
        let new_target = Add this.Position directions.[direction]
        if not (map.ContainsKey(new_target)) then
            _direction <- direction

let rec walk (walker:Walker, target, sum) =
    if sum = target then
        walker.Position
    else
        map.Add(walker.Position, sum)
        walker.Turn()
        walker.Move()
        walk(walker, target, (sum + 1))

[<EntryPoint>]
let main argv =
    let t = 368078
    let (x, y) = walk(Walker(), t, 1)
    printfn "%A is %A blocks away." t ((abs x) + (abs y))
    0
