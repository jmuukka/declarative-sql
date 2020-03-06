module StoredProcedureCommandUnitTests

open System.Data
open Xunit
open Mutex.DeclarativeSql

module Database =

    let doSomething tenantId =
        {
            StoredProcedure = "DoSomething"
            Parameters = [
                "TenantId", Value.ofInt32 tenantId
            ]
        }

[<Fact>]
let ``all parameters having values are returned correctly`` () =
    let tenantId = 9832838

    let actual = Database.doSomething tenantId

    let expected = [
        "TenantId", { DbType = DbType.Int32; Value = 9832838 :> obj }
    ]
    Assert.Equal<(BindName * Value) list>(expected, actual.Parameters)
