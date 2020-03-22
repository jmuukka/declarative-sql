module OperatorsUnitTests

open System
open System.Data
open Xunit
open Mutex.DeclarativeSql
open Mutex.DeclarativeSql.Operators

let private expect dbType value =
    "n", { DbType = dbType; Value = value :> obj }

let private expectDbNullObject =
    expect DbType.Object DBNull.Value

[<Fact>]
let ``operator returns expected parameter for simple types`` () =
    let now = DateTime.UtcNow

    Assert.Equal(expect DbType.Int16 1s, "n" => 1s)
    Assert.Equal(expect DbType.Int32 1, "n" => 1)
    Assert.Equal(expect DbType.Int64 1L, "n" => 1L)
    Assert.Equal(expect DbType.UInt16 1us, "n" => 1us)
    Assert.Equal(expect DbType.UInt32 1u, "n" => 1u)
    Assert.Equal(expect DbType.UInt64 1UL, "n" => 1UL)
    Assert.Equal(expect DbType.Byte 1uy, "n" => 1uy)
    Assert.Equal(expect DbType.SByte 1y, "n" => 1y)
    Assert.Equal(expect DbType.Boolean true, "n" => true)
    Assert.Equal(expect DbType.DateTime2 now, "n" => now)
    Assert.Equal(expect DbType.Decimal 1m, "n" => 1m)
    Assert.Equal(expect DbType.Double 1.0, "n" => 1.0)
    Assert.Equal(expect DbType.Single 1.0f, "n" => 1.0f)

[<Fact>]
let ``operator returns expected parameter for string`` () =
    let nullString : string = null

    Assert.Equal(expect DbType.String "v", "n" => "v")
    // null value does not carry a type information therefore it will yield to Object
    Assert.Equal(expectDbNullObject, "n" => nullString)

[<Fact>]
let ``operator returns expected parameter for byte array`` () =
    let bytes = [|84uy|]
    let nullArray : byte array = null

    Assert.Equal(expect DbType.Binary bytes, "n" => bytes)
    // null value does not carry a type information therefore it will yield to Object
    Assert.Equal(expectDbNullObject, "n" => nullArray)

[<Fact>]
let ``operator returns expected parameter for obj type`` () =
    let obj' = obj()

    Assert.Equal(expect DbType.Object obj', "n" => obj')
    Assert.Equal(expectDbNullObject, "n" => null)

[<Fact>]
let ``operator returns expected parameter for nullable simple types`` () =
    let now = DateTime.UtcNow

    Assert.Equal(expect DbType.Int16 1s, "n" => Nullable<int16> 1s)
    Assert.Equal(expect DbType.Int32 1, "n" => Nullable<int> 1)
    Assert.Equal(expect DbType.Int64 1L, "n" => Nullable<int64> 1L)
    Assert.Equal(expect DbType.UInt16 1us, "n" => Nullable<uint16> 1us)
    Assert.Equal(expect DbType.UInt32 1u, "n" => Nullable<uint32> 1u)
    Assert.Equal(expect DbType.UInt64 1UL, "n" => Nullable<uint64> 1UL)
    Assert.Equal(expect DbType.Byte 1uy, "n" => Nullable<byte> 1uy)
    Assert.Equal(expect DbType.SByte 1y, "n" => Nullable<sbyte> 1y)
    Assert.Equal(expect DbType.Boolean true, "n" => Nullable<bool> true)
    Assert.Equal(expect DbType.DateTime2 now, "n" => Nullable<DateTime> now)
    Assert.Equal(expect DbType.Decimal 1m, "n" => Nullable<decimal> 1m)
    Assert.Equal(expect DbType.Double 1.0, "n" => Nullable<float> 1.0)
    Assert.Equal(expect DbType.Single 1.0f, "n" => Nullable<float32> 1.0f)

[<Fact>]
let ``operator returns expected parameter for null nullable simple types`` () =
    // null nullable value does not carry a type information therefore it will yield to Object
    Assert.Equal(expectDbNullObject, "n" => Nullable<int16>())
    Assert.Equal(expectDbNullObject, "n" => Nullable<int>())
    Assert.Equal(expectDbNullObject, "n" => Nullable<int64>())
    Assert.Equal(expectDbNullObject, "n" => Nullable<uint16>())
    Assert.Equal(expectDbNullObject, "n" => Nullable<uint32>())
    Assert.Equal(expectDbNullObject, "n" => Nullable<uint64>())
    Assert.Equal(expectDbNullObject, "n" => Nullable<byte>())
    Assert.Equal(expectDbNullObject, "n" => Nullable<sbyte>())
    Assert.Equal(expectDbNullObject, "n" => Nullable<bool>())
    Assert.Equal(expectDbNullObject, "n" => Nullable<DateTime>())
    Assert.Equal(expectDbNullObject, "n" => Nullable<decimal>())
    Assert.Equal(expectDbNullObject, "n" => Nullable<float>())
    Assert.Equal(expectDbNullObject, "n" => Nullable<float32>())

[<Fact>]
let ``operator returns expected parameter for option type`` () =
    let now = DateTime.UtcNow
    let bytes = [|84uy|]

    Assert.Equal(expect DbType.Int16 1s, "n" => Some 1s)
    Assert.Equal(expect DbType.Int32 1, "n" => Some 1)
    Assert.Equal(expect DbType.Int64 1L, "n" => Some 1L)
    Assert.Equal(expect DbType.UInt16 1us, "n" => Some 1us)
    Assert.Equal(expect DbType.UInt32 1u, "n" => Some 1u)
    Assert.Equal(expect DbType.UInt64 1UL, "n" => Some 1UL)
    Assert.Equal(expect DbType.Byte 1uy, "n" => Some 1uy)
    Assert.Equal(expect DbType.SByte 1y, "n" => Some 1y)
    Assert.Equal(expect DbType.Boolean true, "n" => Some true)
    Assert.Equal(expect DbType.DateTime2 now, "n" => Some now)
    Assert.Equal(expect DbType.Decimal 1m, "n" => Some 1m)
    Assert.Equal(expect DbType.Double 1.0, "n" => Some 1.0)
    Assert.Equal(expect DbType.Single 1.0f, "n" => Some 1.0f)
    Assert.Equal(expect DbType.String "v", "n" => Some "v")
    Assert.Equal(expect DbType.Binary bytes, "n" => Some bytes)
    Assert.Equal(expectDbNullObject, "n" => None)
