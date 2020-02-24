module SelectCommandUnitTests

open System
open Xunit
open Mutex.DeclarativeSql
open Mutex.DeclarativeSql.Obj

type TenantId = TenantId of int

type Customer = {
    Id : int
    Name : string
    Employees : int option
    AnnualRevenue : Nullable<uint64>
}

module Customer =

    let private selectStatement =
        "select Id, Name, Employees, AnnualRevenue
        from Customer
        where TenantId = @TenantId"

    let private parameters (TenantId tenantId) =
        [
            "TenantId", DbValue.ofInt32 tenantId
        ]

    let private customerUsingObjArray (values : obj array) =
        {
            Id = int values.[0]
            Name = string values.[1]
            Employees = option<int> values.[2]
            AnnualRevenue = nullable<uint64> values.[3]
        }

    let private customerUsingIndexed get =
        {
            Id = get 0 |> int
            Name = get 1 |> string
            Employees = get 2 |> option<int>
            AnnualRevenue = get 3 |> nullable<uint64>
        }

    let private customerUsingNamed get =
        {
            Id = get "Id" |> int
            Name = get "Name" |> string
            Employees = get "Employees" |> option<int>
            AnnualRevenue = get "AnnualRevenue" |> nullable<uint64>
        }

    let getAllUsingObjArray tenantId =
        {
            Sql = selectStatement
            Parameters = parameters tenantId
            Value = ObjArray customerUsingObjArray
        }

    let getAllUsingIndexed tenantId =
        {
            Sql = selectStatement
            Parameters = parameters tenantId
            Value = Indexed customerUsingIndexed
        }

    let getAllUsingNamed tenantId =
        {
            Sql = selectStatement
            Parameters = parameters tenantId
            Value = Named customerUsingNamed
        }
