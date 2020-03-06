module StoredProcedureCommandUnitTests

open Xunit
open Mutex.DeclarativeSql

module Database =

    let doSomething =
        {
            StoredProcedure = "DoSomething"
            Parameters = []
            Value = (fun value -> value :?> int)
        }

[<Fact>]
let ``the value returned from the procedure is correctly converted to value of actual type`` () =
    let returnValueFromTheProcedure = 13

    let actual = Database.doSomething.Value (returnValueFromTheProcedure :> obj)

    Assert.Equal(returnValueFromTheProcedure, actual)
