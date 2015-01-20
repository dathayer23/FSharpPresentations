module TheTroubleWithEnums

type Boolean = 
    | True
    | False


//The mysterious defualt is gone
//When you add Maybe you have to add a new case or it won't compile
let truth (b:Boolean) = 
   match b with 
   | True -> "It's really true"
   | False -> "It's really false"
