

//Foreign Data
//Sql
//JSON
//XML

(*
For this script to run properly you must have R installed
*)

#I @"..\packages"
#r @"fsharp.data.2.1.1\lib\net40\fsharp.data.dll"
#r @"R.NET.Community.1.5.16\lib\net40\RDotNet.dll"
#r @"RProvider.1.1.8\lib\net40\RProvider.dll"
#r @"RProvider.1.1.8\lib\net40\RProvider.Runtime.dll"

open System
open FSharp.Data
open RDotNet
open RProvider
open RProvider.RInterop
open RProvider.``base``
open RProvider.graphics

type Titanic = FSharp.Data.CsvProvider<"titanic.csv">
let titanic = new Titanic()
let first = titanic.Rows |> Seq.head

//we get access to live data from the world bank
// with IntelliSense for discoverability
let wb = FSharp.Data.WorldBankData.GetDataContext()
let capital = wb.Countries.Afghanistan.CapitalCity
//wb.Countries.Benin.Indicators.

// ... with documentation for all indicators

//We can now work against R with Intellisense ...
let rng = System.Random()
let data = R.c([| for i in 0 .. 1000 -> rng.NextDouble()|])
R.plot(data)
R.hist(data |> R.log)


// ... and grab live data from the World bank and 
// send it to R for visualization
let countries = [| 
      wb.Countries.Canada;
      wb.Countries.``United States``;
      wb.Countries.Mexico;
      wb.Countries.Brazil;
      wb.Countries.Argentina;
      wb.Countries.``United Kingdom``;
      wb.Countries.France;
      wb.Countries.Germany;
      wb.Countries.``South Africa``;
      wb.Countries.Kenya;
      wb.Countries.``Russian Federation``;
      wb.Countries.China;
      wb.Countries.Japan;
      wb.Countries.Australia |]

let gdp2000 = countries |> Array.map (fun c -> c.Indicators.``GDP (current US$)``.[2000])
let gdp2010 = countries |> Array.map (fun c -> c.Indicators.``GDP (current US$)``.[2010])


let series = [ "GDP2000", gdp2000; "GDP2010", gdp2010]

let dataframe = R.data_frame(namedParams series)
R.plot(dataframe)

let names = countries |> Array.map (fun c -> c.Name)
R.text(gdp2000, gdp2010, names)

//open RProvider

(* This whole section requires the R rworldmap package
to be installed (it is not part of the standard distribution).
*)

open RProvider.rworldmap


let df =
    let codes, pops = 
        query { for country in wb.Countries -> 
                    country.Code, 
                    country.Indicators.``Population, total``.[2010] }
//        query { for country in wb.Countries -> 
//                    country.Code, 
//                    country.Indicators.``Population (Total)``.[2010]/country.Indicators.``Population (Total)``.[2000] }
        |> Seq.toArray
        |> Array.unzip
    let data =
        [ "Code", codes |> box 
          "Pop", pops |> box ]
    R.data_frame (namedParams data)

let map = R.joinCountryData2Map(df, "ISO3", "Code")
R.mapDevice()
R.mapCountryData(map,"Pop")







