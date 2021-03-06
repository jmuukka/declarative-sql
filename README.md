# Declarative SQL

Use declarative programming in F# and write your SQL without dependency to infrastructure. When you want to execute the SQL then use e.g. Mutex.DeclarativeSql.SqlClient (for SQL Server) package.

## Example

This example has a mixture of features. Normally you would not mix F# option types and .NET Nullable<'t> types in a single record type.

<pre>
type TenantId = TenantId of int

type Customer = {
    Id : int64
    Name : string
    Employees : uint32 option
    AnnualRevenue : Nullable&lt;uint64&gt;
}

module Customer =

    let update (TenantId tenantId) customer =
        {
            Sql = "update Customer set
                    Name = @Name,
                    Employees = @Employees,
                    AnnualRevenue = @AnnualRevenue
                   where TenantId = @TenantId
                    and Id = @Id"
            Parameters = [
                "TenantId", Value.ofInt32 tenantId
                "Id", Value.ofInt64 customer.Id
                "Name", Value.ofString customer.Name
                "Employees", Value.ofOption<uint32> customer.Employees
                "AnnualRevenue", Value.ofNullable<uint64> customer.AnnualRevenue
            ]
        }
</pre>

Since SQL handles data of multiple types then it's required to convert all data types to object. Nulls are handled as DBNull.Value. Value module contains multiple functions for converting values to object while preserving the DbType.

It's also possible to use => operator for parameters. It handles nullable and option types. When the value is null or None then the value will be DBNull.Value and DbType will be Object. When the mapping does not exist for the value's type then DbType.Object will be used. Here is the above example using this functionality.

<pre>

open Mutex.DeclarativeSql.Operators

module Customer =

    let update (TenantId tenantId) customer =
        {
            Sql = "update Customer set
                    Name = @Name,
                    Employees = @Employees,
                    AnnualRevenue = @AnnualRevenue
                   where TenantId = @TenantId
                    and Id = @Id"
            Parameters = [
                "TenantId" => tenantId
                "Id" => customer.Id
                "Name" => customer.Name
                "Employees" => customer.Employees
                "AnnualRevenue" => customer.AnnualRevenue
            ]
        }
</pre>

Selecting data is supported via type SelectCommand<'t>. Some people prefer using names and some prefer indexes. DeclarativeSql supports obj array, indexed and named as shown in following example:

<pre>
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
</pre>

The functions like int, string, option<'t> and nullable<'t> can be used directly, as in the example, when you open the module Mutex.DeclarativeSql.Obj. They convert obj to target datatype.

------

Copyright (c) 2020 Jarmo Muukka, Mutex Oy