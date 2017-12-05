open System
open System.Collections.Generic


type Direction = Left | Right

let degrees_to_radians (d : float) = d * System.Math.PI / 180.0

let calculate_rotation(x, y, dir) =
    let degrees = if dir = Left then -90 else 90
    let theta = degrees |> float |> degrees_to_radians
    let (cs, sn) = (cos theta, sin theta)
    let (fx, fy) = (float x, float y)
    (fx * cs - fy * sn |> int,
     fx * sn + fy * cs |> int)

[<StructuredFormatDisplay("({X}, {Y})")>]
type Vec2(x:int, y:int) =

    member val X:int = x with get, set
    member val Y:int = y with get, set
    member this.Rotated (dir) =
        let (x, y) = calculate_rotation(this.X, this.Y, dir)
        Vec2(x, y)
    member this.Add(vec:Vec2) =
        Vec2(this.X + vec.X, this.Y + vec.Y)
    member this.AsTuple() = (this.X, this.Y)

let map = Dictionary<int * int, int>()

[<StructuredFormatDisplay("({Position} - {Direction})")>]
type Walker(dx, dy) =
    member val Position = Vec2(0, 0) with get,set
    member val Direction = Vec2(dx, dy) with get,set
    member this.Target with get() = this.Position.Add this.Direction
    member this.Move() = this.Position <- this.Target
    member this.Turn(dir) = this.Direction <- this.Direction.Rotated(dir)
    member this.CanTurn() =
        let new_direction = this.Direction.Rotated(Left)
        let new_target = this.Position.Add new_direction
        let tup = new_target.AsTuple()
        if map.ContainsKey(tup) then
            false
        else
            true


let rec walk (walker:Walker, target, sum) =
    if sum = target then
        walker.Position
    else
        let a = map.Count
        map.Add(walker.Position.AsTuple(), sum)
        if walker.CanTurn() then
            walker.Turn(Left)

        walker.Move()
        walk(walker, target, (sum + 1))

[<EntryPoint>]
let main argv =
    let t = 368078
    let w = Walker(0, 1)
    let position = walk(w, t, 1)
    printfn "%A is at %A" t position
    0
