module StoredProcedureScalarCommandUnitTests

open System
open Xunit
open Mutex.DeclarativeSql

module Database =

    let utcNow =
        {
            StoredProcedure = "getUtcNow"
            Parameters = []
            ScalarValue = (fun value -> value :?> DateTime)
        }

[<Fact>]
let ``scalar obj value from server is correctly converted to value of actual type`` () =
    let utcNow = DateTime.UtcNow

    let actual = Database.utcNow.ScalarValue (utcNow :> obj)

    Assert.Equal(utcNow, actual)
