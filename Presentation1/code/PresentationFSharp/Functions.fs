module Functions

type Puppy = { name : string; age : int}

//lets make a list of puppies
let puppies = [
   { name = "Gypsy"; age = 10 };
   { name = "Mattie"; age = 11 };
   { name = "Sooner"; age = 16 } ]

//Not necessary but added for clarity
type Filter = Puppy -> bool

let giveMePuppies (puppies : Puppy list) (filter:Filter) = List.filter  filter puppies

//List module

//you can declare a function like this
// notice there are no parenthesis
let youngPuppy puppy = puppy.age < 11

// signatures match so use the function no fuss
let youngOnes = giveMePuppies puppies  youngPuppy

//again no parenthesis
let youngerThan age puppy =  puppy.age < age

//why is it a good idea to use no parenthesis?
//we can crate new functions by locking in the value for one parameter and leaving the other slot free
// here we create a closure over a value for the first functional argument of (10)
let youngerThan10 = youngerThan 10

//this is handy for doing things like this
let stillYoung = giveMePuppies puppies (youngerThan 12)

//using the pipe-forward operator '|>'  you can do the following
// f x y ==> y |> f x

let oldSchool = youngerThan 7 { name = "Cocoa"; age = 16 }
let coolness = { name = "Samson"; age = 3 } |> youngerThan 5

//Why is this awesome 
//you can compose functions freely
// you get fluent interfaces everywhere





   // Inside Out programming : compose small pieces of functionality 
   // that are trivially correct into larger work flows
   // kind of like lego's

open System
open System.IO

let readlines path  = File.ReadAllLines(path)
let saveAs path lines = File.WriteAllLines(path, lines)
let lowercase (text:string) = text.ToLowerInvariant()
let split (text:string) = text.Split(' ')

let cleanup text = 
   text
   |> lowercase
   |> split
   |> Array.sort  // ...

let analyze source target = 
   readlines source
   |> Array.map lowercase
   |> Array.map split
   |> Array.concat
   |> Seq.distinct
   |> Seq.sort
   |> Seq.toArray
   |> saveAs target

let desktop = Environment.SpecialFolder.Desktop
let root = Environment.GetFolderPath(desktop)
let source = root + @"\test.txt"
let target = root + @"\words.txt"

analyze source target

    
