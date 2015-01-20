module DiscriminatedUnions
open System
type Result = 
   | Value of float
   | Error of String


let division (x:float) (y:float) = 
   if y = 0.0
   then Error("Cannot divide by zero")
   else Value(x/y)

//Really comes in handy if you wish toi avoid thge use of null
let maxFromList (data : int list) = 
   match data with 
   | [] -> None
   | x -> Some(List.max x)

// now you have a funbction signature that explicitly tells you 
// there may be possible input values that have no output
// sig = int list -> int option

let printMax (data: int list) = 
   match maxFromList data with
   | None -> "Empty list cannot have max"
   | Some v -> sprintf "List max = %d" v

//Discriminated unions can define recursive types like lists and trees
type list<'v> = 
   | Node of 'v
   | List of 'v * list<'v>

type tree<'v> = 
   | Leaf of 'v
   | Node of tree<'v> * tree<'v>

type mtree<'v> = 
   | MLeaf of 'v
   | MNode of tree<'v> list
