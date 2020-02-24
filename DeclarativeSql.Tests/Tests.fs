module Tests

open System
open Xunit
open Mutex.DeclarativeSql
open Mutex.DeclarativeSql.Obj

type OrganizationId = OrganizationId of int

type Customer = {
    Id : int
    Name : string
    Employees : int option
}

//module Customer =

//    let private columns = "a.Id, a.Name, c.Employees"

//    let private customer (values : obj array) =
//        {
//            Id = int values.[0]
//            Name = string values.[1]
//            Employees = option<int> values.[2]
//        }

//    let selectAll (OrganizationId organizationId) =
//        let organizationId = Int32 organizationId
//        {
//            Sql = "select " + columns + "
//from Account a
//join Company c on c.OrganizationId = a.OrganizationId
// and c.Id = a.CompanyId
//where a.OrganizationId = @OrganizationId"
//            Parameters = [
//                "OrganizationId", Some organizationId
//                // above is better than { Name = "OrganizationId"; Value = Some organizationId }
//            ]
//            Value = customer
//        }

//[<Fact>]
//let ``selectAll function returns SelectCommand with expected Parameters`` () =
//    let organizationId = OrganizationId 9832838

//    let actual = Customer.selectAll organizationId

//    Assert.Equal(1, actual.Parameters.Length)
//    let param0 = actual.Parameters.Item(0)
//    Assert.Equal("OrganizationId", fst param0)
//    match snd param0 with
//    | Some (Int32 id) -> 
//        Assert.Equal(9832838, id)
//    | _ ->
//        failwith ""

//[<Fact>]
//let ``selectAll function returns SelectCommand with Value function that takes array with null and creates correct Customer value`` () =
//    let organizationId = OrganizationId 9832838

//    let actual = Customer.selectAll organizationId

//    let customer = actual.Value [| 123; "Mutex Oy"; null |]
//    Assert.Equal({ Id = 123; Name = "Mutex Oy"; Employees = None }, customer)

//[<Fact>]
//let ``selectAll function returns SelectCommand with Value function that takes array with DBNull.Value and creates correct Customer value`` () =
//    let organizationId = OrganizationId 9832838

//    let actual = Customer.selectAll organizationId

//    let customer = actual.Value [| 123; "Mutex Oy"; DBNull.Value |]
//    Assert.Equal({ Id = 123; Name = "Mutex Oy"; Employees = None }, customer)

//[<Fact>]
//let ``selectAll function returns SelectCommand with Value function that takes array with employees and creates correct Customer value`` () =
//    let organizationId = OrganizationId 9832838

//    let actual = Customer.selectAll organizationId

//    let customer = actual.Value [| 123; "Mutex Oy"; 50 |]
//    Assert.Equal({ Id = 123; Name = "Mutex Oy"; Employees = Some 50 }, customer)
