module SelectCommandUnitTests

open System
open Xunit
open Mutex.DeclarativeSql
open Mutex.DeclarativeSql.Obj

type TenantId = TenantId of int

type Customer = {
    Id : int
    Name : string
    Employees : uint32 option
    AnnualRevenue : Nullable<uint64>
}

module Customer =

    let private selectStatement =
        "select Id, Name, Employees, AnnualRevenue
        from Customer
        where TenantId = @TenantId"

    let private parameters (TenantId tenantId) =
        [
            "TenantId", Value.ofInt32 tenantId
        ]

    let private customerUsingObjArray (values : obj array) =
        {
            Id = int values.[0]
            Name = string values.[1]
            Employees = option<uint32> values.[2]
            AnnualRevenue = nullable<uint64> values.[3]
        }

    let private customerUsingIndexed get =
        {
            Id = get 0 |> int
            Name = get 1 |> string
            Employees = get 2 |> option<uint32>
            AnnualRevenue = get 3 |> nullable<uint64>
        }

    let private customerUsingNamed get =
        {
            Id = get "Id" |> int
            Name = get "Name" |> string
            Employees = get "Employees" |> option<uint32>
            AnnualRevenue = get "AnnualRevenue" |> nullable<uint64>
        }

    let getUsingObjArray tenantId =
        {
            Sql = selectStatement
            Parameters = parameters tenantId
            Value = ObjArray customerUsingObjArray
        }

    let getUsingIndexed tenantId =
        {
            Sql = selectStatement
            Parameters = parameters tenantId
            Value = Indexed customerUsingIndexed
        }

    let getUsingNamed tenantId =
        {
            Sql = selectStatement
            Parameters = parameters tenantId
            Value = Named customerUsingNamed
        }

[<Fact>]
let ``get data from database using ObjArray`` () =
    let tenantId = TenantId 9832838
    let expected = {
        Id = 132323
        Name = "John Doe"
        Employees = Some 500u
        AnnualRevenue = Nullable<uint64> 1_000_000UL
    }

    let actual = Customer.getUsingObjArray tenantId

    match actual.Value with
    | ObjArray get ->
        // Let's assume that now we read the data from database and copy each
        // field to the array in the order they appear in a select statement.
        let customer = [|
            132323 :> obj
            "John Doe" :> obj
            500u :> obj
            1_000_000UL :> obj
        |]
        Assert.Equal(expected, get customer)
    | _ ->
        failwith "what?"

[<Fact>]
let ``get data from database using Indexed`` () =
    let tenantId = TenantId 9832838
    let expected = {
        Id = 132323
        Name = "John Doe"
        Employees = Some 500u
        AnnualRevenue = Nullable<uint64> 1_000_000UL
    }

    let actual = Customer.getUsingIndexed tenantId

    match actual.Value with
    | Indexed get ->
        // Let's assume that now we read the data from database
        // and get each value from the result by its index.
        let valueByIndex index =
            match index with
            | 0 -> 132323 :> obj
            | 1 -> "John Doe" :> obj
            | 2 -> 500u :> obj
            | 3 -> 1_000_000UL :> obj
            | _ -> failwith "no way!"
        Assert.Equal(expected, get valueByIndex)
    | _ ->
        failwith "what?"

[<Fact>]
let ``get data from database using Named`` () =
    let tenantId = TenantId 9832838
    let expected = {
        Id = 132323
        Name = "John Doe"
        Employees = Some 500u
        AnnualRevenue = Nullable<uint64> 1_000_000UL
    }

    let actual = Customer.getUsingNamed tenantId

    match actual.Value with
    | Named get ->
        // Let's assume that now we read the data from database
        // and get each value from the result by its name.
        let valueByName name =
            match name with
            | "Id" -> 132323 :> obj
            | "Name" -> "John Doe" :> obj
            | "Employees" -> 500u :> obj
            | "AnnualRevenue" -> 1_000_000UL :> obj
            | _ -> failwith "no way!"
        Assert.Equal(expected, get valueByName)
    | _ ->
        failwith "what?"
