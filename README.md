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
                "TenantId", DbValue.ofInt32 tenantId
                "Id", DbValue.ofInt64 customer.Id
                "Name", DbValue.ofString customer.Name
                "Employees", DbValue.ofOption<uint32> customer.Employees
                "AnnualRevenue", DbValue.ofNullable<uint64> customer.AnnualRevenue
            ]
        }
</pre>

Since SQL handles data of multiple types then it's required to convert all data types to object. Nulls are handled as DBNull.Value. DbValue module contains multiple functions for converting values to object while preserving the DbType.

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
