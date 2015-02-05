//module ActivePatterns = 
open System
open System.Reflection
open System.Net.Mail



//module Complex = 
type complex = Argv of double * double | Cartesian of double * double

let (|Polar|) (c:complex) = 
    match c with 
    | Argv (x,y) -> Argv(x,y)
    | Cartesian (x,y) -> Argv(Math.Sqrt(x**2.0 + y**2.0), Math.Atan(y/x))

let (|Rect|) (c:complex)  = 
    match c with 
    | Argv(r,theta) -> Cartesian(r * Math.Cos(theta), r * Math.Sin(theta))
    | Cartesian(x,y) -> Cartesian(x,y)

let toPolar (c:complex) = match c with | Polar p -> p
let toRect (c) = match c with | Rect p -> p
let c = Cartesian(1.0,1.0)
let p = toPolar(c)
let c1 = toRect(p)

//module Reflection
let (|Members|) x  = x.GetType().GetMethods() |> Array.map (fun mi -> mi.Name) |> Array.toList
let (|ParameterInfo|) (x:MethodInfo) = x.GetParameters()
let (|Parameters|) (x:MethodInfo) = x.GetParameters() |> Array.map (fun pi -> pi.Name) |> Array.toList
let (|MemberInfo|) x = 
   x.GetType().GetMethods() 
      |> Array.map (fun mi -> match mi with ParameterInfo pms -> pms |> Array.map (fun (pi) -> mi.Name + "-" + pi.Name) 
                            |> Array.toList) |> List.concat

let mems x = match x with MemberInfo res -> res

mems (new System.Net.Mail.MailMessage())

// complete partition odf a set

let (|Odd|Even|) x = if x % 2 = 0 then Even else Odd
let isDivisibleByTwo x = match x with Even -> true | Odd -> false
isDivisibleByTwo 16
isDivisibleByTwo 17

// partial partition of a set
let (|DivisibleBySeven|_|) x = if x % 7 = 0 then Some() else None
let (|IsPerfectSquare|_|) x = 
   let sqrt = (int)(Math.Sqrt(float x))
   if sqrt * sqrt = x then Some() else None

let describeNumber x = 
   match x with 
   | DivisibleBySeven & IsPerfectSquare -> sprintf "%d is divisible by 7 and is a perfect square" x
   | DivisibleBySeven -> sprintf "%d is divisable by 7" x
   | IsPerfectSquare -> sprintf "%d is a perfect square" x
   | _ -> sprintf "%d is not divisable by 7 nor a perfect square" x


let (|MultipleOf|_|) x input = if input % x = 0 then Some(input / x) else None

let factorize x : (int list) * (int list) =
    let rec factorizeRec (factors:int list, remainders:int list) n i =
        let sqrt = int (Math.Sqrt(float n))
        if i > sqrt then
            ([],[])
        else
            match n with
            | MultipleOf i timesXdividesIntoI
                -> (factorizeRec ((i :: factors), (timesXdividesIntoI :: remainders)) n (i + 1))
            | _ -> factorizeRec (factors, remainders) n (i + 1)
    factorizeRec ([],[]) x 1 |> fun (fs, rs) -> (List.tail fs, List.tail rs)

factorize (7 * 15 * 40 * 239 * 500)
