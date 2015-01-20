module ShoppingCart
open System
open Microsoft. VisualStudio.TestTools.UnitTesting

type CartItem = string    // placeholder for a more complicated type

// don't use empty list! We want to force clients to handle this as a separate case. E.g. "you have no items in your cart"
type EmptyState = NoItems 
type ActiveState = { UnpaidItems : CartItem list; }
type PaidForState = { PaidItems : CartItem list; Payment : decimal}

type Cart = 
    | Empty of EmptyState 
    | Active of ActiveState 
    | PaidFor of PaidForState 

// =============================
// operations on empty state
// =============================
// returns a new Active Cart
let addToEmptyState item =  Cart.Active {UnpaidItems=[item]}

// =============================
// operations on active state
// =============================

let addToActiveState state itemToAdd = 
   let newList = itemToAdd :: state.UnpaidItems
   Cart.Active {state with UnpaidItems=newList }

let removeFromActiveState state itemToRemove = 
   let newList = state.UnpaidItems |> List.filter (fun i -> i<>itemToRemove)
                
   match newList with
   | [] -> Cart.Empty NoItems
   | _ -> Cart.Active {state with UnpaidItems=newList} 

// returns a new PaidFor Cart
let payForActiveState state amount = Cart.PaidFor { PaidItems=state.UnpaidItems; Payment=amount}

type EmptyState with
   member this.Add = addToEmptyState 

type ActiveState with
   member this.Add = addToActiveState this 
   member this.Remove = removeFromActiveState this 
   member this.Pay = payForActiveState this 

let addItemToCart cart item =  
   match cart with
   | Empty state -> state.Add item
   | Active state -> state.Add item
   | PaidFor state -> printfn "ERROR: The cart is paid for"; cart   

let removeItemFromCart cart item =  
   match cart with
   | Empty state ->  printfn "ERROR: The cart is empty"; cart   // return the cart 
   | Active state -> state.Remove item
   | PaidFor state ->  printfn "ERROR: The cart is paid for"; cart   // return the cart

let displayCart cart  =  
   match cart with
   | Empty state -> printfn "The cart is empty"   // can't do state.Items
   | Active state -> printfn "The cart contains %A unpaid items" state.UnpaidItems
   | PaidFor state -> printfn "The cart contains %A paid items. Amount paid: %f"  state.PaidItems state.Payment

let payForCart cart payment = 
   match cart with 
   | Empty _ -> printfn "ERROR: Cannot pay for empty cart"; cart
   | Active state -> state.Pay(payment)
   | PaidFor _ ->  printfn "ERROR: Cart is already paid for"; cart

type Cart with
   static member NewCart = Cart.Empty NoItems
   member this.Add = addItemToCart this 
   member this.Remove = removeItemFromCart this 
   member this.Display = displayCart this 
   member this.Pay payment = payForCart this payment

//let badFunction cartABPaid = 
//   match cartABPaid with
//   | Empty state -> state.Pay 100m
//   | PaidFor state -> state.Pay 100m
//   | Active state -> state.Pay 100m

[<TestClass>]
type TestShoppingCart() = 
   
   [<TestMethod>]
   member x.TestCart() = 
      let emptyCart = Cart.NewCart
      do emptyCart.Display

      let cartA = emptyCart.Add("A")
      do cartA.Display

      let cartAb = cartA.Add("B")
      do cartAb.Display

      let cartB = cartAb.Remove("A")
      do cartB.Display

      let emptyCart2 = cartB.Remove("B")
      do emptyCart2.Display

      do Console.WriteLine("Removing from emptyCart")
      let x = emptyCart.Remove("B");

      let paidCart = cartA.Pay (new Decimal(1000))
      do paidCart.Display

      do Console.WriteLine("Adding to paidCart")
      let y =  paidCart.Add("C")

      do Console.WriteLine("paying for emptyCart")
      let z = emptyCart.Pay (new Decimal(1000))
      do emptyCart.Display