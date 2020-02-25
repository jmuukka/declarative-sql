module CommandUnitTests

open System
open System.Data
open Xunit
open Mutex.DeclarativeSql

type TenantId = TenantId of int

type Customer = {
    Id : int64
    Name : string
    Employees : uint32 option
    AnnualRevenue : Nullable<uint64>
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

[<Fact>]
let ``parameters with null/None data are returned correctly`` () =
    let tenantId = TenantId 9832838
    let customer = {
        Id = 132323L
        Name = null
        Employees = None
        AnnualRevenue = Nullable<uint64>()
    }

    let actual = Customer.update tenantId customer

    let expected = [
        "TenantId", { DbType = DbType.Int32; Value = 9832838 :> obj }
        "Id", { DbType = DbType.Int64; Value = 132323L :> obj }
        "Name", { DbType = DbType.String; Value = DBNull.Value :> obj }
        "Employees", { DbType = DbType.UInt32; Value = DBNull.Value :> obj }
        "AnnualRevenue", { DbType = DbType.UInt64; Value = DBNull.Value :> obj }
    ]
    Assert.Equal<(BindName * Value) list>(expected, actual.Parameters)

[<Fact>]
let ``all parameters having values are returned correctly`` () =
    let tenantId = TenantId 9832838
    let customer = {
        Id = 132323L
        Name = "John Doe"
        Employees = Some 500u
        AnnualRevenue = Nullable<uint64> 1_000_000UL
    }

    let actual = Customer.update tenantId customer

    let expected = [
        "TenantId", { DbType = DbType.Int32; Value = 9832838 :> obj }
        "Id", { DbType = DbType.Int64; Value = 132323L :> obj }
        "Name", { DbType = DbType.String; Value = "John Doe" :> obj }
        "Employees", { DbType = DbType.UInt32; Value = 500u :> obj }
        "AnnualRevenue", { DbType = DbType.UInt64; Value = 1_000_000UL :> obj }
    ]
    Assert.Equal<(BindName * Value) list>(expected, actual.Parameters)
