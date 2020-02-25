module ValueUnitTests

open System
open System.Data
open Xunit
open Mutex.DeclarativeSql

[<Fact>]
let ``simple value using ofXxxx equals expected Value`` () =
    let now = DateTime.UtcNow

    Assert.Equal({ DbType = DbType.Int16; Value = 1s :> obj }, Value.ofInt16 1s)
    Assert.Equal({ DbType = DbType.Int32; Value = 1 :> obj }, Value.ofInt32 1)
    Assert.Equal({ DbType = DbType.Int64; Value = 1L :> obj }, Value.ofInt64 1L)
    Assert.Equal({ DbType = DbType.UInt16; Value = 1us :> obj }, Value.ofUInt16 1us)
    Assert.Equal({ DbType = DbType.UInt32; Value = 1u :> obj }, Value.ofUInt32 1u)
    Assert.Equal({ DbType = DbType.UInt64; Value = 1UL :> obj }, Value.ofUInt64 1UL)
    Assert.Equal({ DbType = DbType.Byte; Value = 1uy :> obj }, Value.ofByte 1uy)
    Assert.Equal({ DbType = DbType.SByte; Value = 1y :> obj }, Value.ofSByte 1y)
    Assert.Equal({ DbType = DbType.Boolean; Value = true :> obj }, Value.ofBool true)
    Assert.Equal({ DbType = DbType.DateTime2; Value = now :> obj }, Value.ofDateTime now)
    Assert.Equal({ DbType = DbType.Decimal; Value = 1m :> obj }, Value.ofDecimal 1m)
    Assert.Equal({ DbType = DbType.Double; Value = 1.0 :> obj }, Value.ofFloat 1.0)
    Assert.Equal({ DbType = DbType.Single; Value = 1.0f :> obj }, Value.ofFloat32 1.0f)

[<Fact>]
let ``string value using ofString equals expected Value`` () =
    Assert.Equal({ DbType = DbType.String; Value = DBNull.Value :> obj }, Value.ofString null)
    Assert.Equal({ DbType = DbType.String; Value = "ö" :> obj }, Value.ofString "ö")

[<Fact>]
let ``byte array value using ofByteArray equals expected Value`` () =
    let bytes = [|84uy|]

    Assert.Equal({ DbType = DbType.Binary; Value = DBNull.Value :> obj }, Value.ofByteArray null)
    Assert.Equal({ DbType = DbType.Binary; Value = bytes :> obj }, Value.ofByteArray bytes)

[<Fact>]
let ``Some value using ofOption<'t> equals expected Value`` () =
    let now = DateTime.UtcNow
    let object = obj()

    Assert.Equal({ DbType = DbType.Int16; Value = 1s :> obj }, Value.ofOption<int16> (Some 1s))
    Assert.Equal({ DbType = DbType.Int32; Value = 1 :> obj }, Value.ofOption<int32> (Some 1))
    Assert.Equal({ DbType = DbType.Int64; Value = 1L :> obj }, Value.ofOption<int64> (Some 1L))
    Assert.Equal({ DbType = DbType.UInt16; Value = 1us :> obj }, Value.ofOption<uint16> (Some 1us))
    Assert.Equal({ DbType = DbType.UInt32; Value = 1u :> obj }, Value.ofOption<uint32> (Some 1u))
    Assert.Equal({ DbType = DbType.UInt64; Value = 1UL :> obj }, Value.ofOption<uint64> (Some 1UL))
    Assert.Equal({ DbType = DbType.Byte; Value = 1uy :> obj }, Value.ofOption<byte> (Some 1uy))
    Assert.Equal({ DbType = DbType.SByte; Value = 1y :> obj }, Value.ofOption<sbyte> (Some 1y))
    Assert.Equal({ DbType = DbType.Boolean; Value = true :> obj }, Value.ofOption<bool> (Some true))
    Assert.Equal({ DbType = DbType.DateTime2; Value = now :> obj }, Value.ofOption<DateTime> (Some now))
    Assert.Equal({ DbType = DbType.Decimal; Value = 1m :> obj }, Value.ofOption<decimal> (Some 1m)) 
    Assert.Equal({ DbType = DbType.Double; Value = 1.0 :> obj }, Value.ofOption<float> (Some 1.0))
    Assert.Equal({ DbType = DbType.Single; Value = 1.0f :> obj }, Value.ofOption<float32> (Some 1.0f))
    Assert.Equal({ DbType = DbType.String; Value = "Ö" :> obj }, Value.ofOption<string> (Some "Ö"))
    Assert.Equal({ DbType = DbType.Object; Value = object }, Value.ofOption<obj> (Some object))

[<Fact>]
let ``None value using ofOption<'t> equals expected Value`` () =
    let dbNull = DBNull.Value :> obj

    Assert.Equal({ DbType = DbType.Int16; Value = dbNull }, Value.ofOption<int16> None)
    Assert.Equal({ DbType = DbType.Int32; Value = dbNull }, Value.ofOption<int32> None)
    Assert.Equal({ DbType = DbType.Int64; Value = dbNull }, Value.ofOption<int64> None)
    Assert.Equal({ DbType = DbType.UInt16; Value = dbNull }, Value.ofOption<uint16> None)
    Assert.Equal({ DbType = DbType.UInt32; Value = dbNull }, Value.ofOption<uint32> None)
    Assert.Equal({ DbType = DbType.UInt64; Value = dbNull }, Value.ofOption<uint64> None)
    Assert.Equal({ DbType = DbType.Byte; Value = dbNull }, Value.ofOption<byte> None)
    Assert.Equal({ DbType = DbType.SByte; Value = dbNull }, Value.ofOption<sbyte> None)
    Assert.Equal({ DbType = DbType.Boolean; Value = dbNull }, Value.ofOption<bool> None)
    Assert.Equal({ DbType = DbType.DateTime2; Value = dbNull }, Value.ofOption<DateTime> None)
    Assert.Equal({ DbType = DbType.Decimal; Value = dbNull }, Value.ofOption<decimal> None)
    Assert.Equal({ DbType = DbType.Double; Value = dbNull }, Value.ofOption<float> None)
    Assert.Equal({ DbType = DbType.Single; Value = dbNull }, Value.ofOption<float32> None)
    Assert.Equal({ DbType = DbType.String; Value = dbNull }, Value.ofOption<string> None)
    Assert.Equal({ DbType = DbType.Object; Value = dbNull }, Value.ofOption<obj> None)

[<Fact>]
let ``Nullable<'t> value using ofNullable<'t> equals expected Value`` () =
    let now = DateTime.UtcNow

    Assert.Equal({ DbType = DbType.Int16; Value = 1s :> obj }, Value.ofNullable<int16> (Nullable<int16> 1s))
    Assert.Equal({ DbType = DbType.Int32; Value = 1 :> obj }, Value.ofNullable<int32> (Nullable<int> 1))
    Assert.Equal({ DbType = DbType.Int64; Value = 1L :> obj }, Value.ofNullable<int64> (Nullable<int64> 1L))
    Assert.Equal({ DbType = DbType.UInt16; Value = 1us :> obj }, Value.ofNullable<uint16> (Nullable<uint16> 1us))
    Assert.Equal({ DbType = DbType.UInt32; Value = 1u :> obj }, Value.ofNullable<uint32> (Nullable<uint32> 1u))
    Assert.Equal({ DbType = DbType.UInt64; Value = 1UL :> obj }, Value.ofNullable<uint64> (Nullable<uint64> 1UL))
    Assert.Equal({ DbType = DbType.Byte; Value = 1uy :> obj }, Value.ofNullable<byte> (Nullable<byte> 1uy))
    Assert.Equal({ DbType = DbType.SByte; Value = 1y :> obj }, Value.ofNullable<sbyte> (Nullable<sbyte> 1y))
    Assert.Equal({ DbType = DbType.Boolean; Value = true :> obj }, Value.ofNullable<bool> (Nullable<bool> true))
    Assert.Equal({ DbType = DbType.DateTime2; Value = now :> obj }, Value.ofNullable<DateTime> (Nullable<DateTime> now))
    Assert.Equal({ DbType = DbType.Decimal; Value = 1m :> obj }, Value.ofNullable<decimal> (Nullable<decimal> 1m)) 
    Assert.Equal({ DbType = DbType.Double; Value = 1.0 :> obj }, Value.ofNullable<float> (Nullable<float> 1.0))
    Assert.Equal({ DbType = DbType.Single; Value = 1.0f :> obj }, Value.ofNullable<float32> (Nullable<float32> 1.0f))

[<Fact>]
let ``null Nullable<'t> value using ofNullable<'t> equals expected Value`` () =
    let dbNull = DBNull.Value :> obj

    Assert.Equal({ DbType = DbType.Int16; Value = dbNull }, Value.ofNullable<int16> (Nullable<int16>()))
    Assert.Equal({ DbType = DbType.Int32; Value = dbNull }, Value.ofNullable<int32> (Nullable<int>()))
    Assert.Equal({ DbType = DbType.Int64; Value = dbNull }, Value.ofNullable<int64> (Nullable<int64>()))
    Assert.Equal({ DbType = DbType.UInt16; Value = dbNull }, Value.ofNullable<uint16> (Nullable<uint16>()))
    Assert.Equal({ DbType = DbType.UInt32; Value = dbNull }, Value.ofNullable<uint32> (Nullable<uint32>()))
    Assert.Equal({ DbType = DbType.UInt64; Value = dbNull }, Value.ofNullable<uint64> (Nullable<uint64>()))
    Assert.Equal({ DbType = DbType.Byte; Value = dbNull }, Value.ofNullable<byte> (Nullable<byte>()))
    Assert.Equal({ DbType = DbType.SByte; Value = dbNull }, Value.ofNullable<sbyte> (Nullable<sbyte>()))
    Assert.Equal({ DbType = DbType.Boolean; Value = dbNull }, Value.ofNullable<bool> (Nullable<bool>()))
    Assert.Equal({ DbType = DbType.DateTime2; Value = dbNull }, Value.ofNullable<DateTime> (Nullable<DateTime>()))
    Assert.Equal({ DbType = DbType.Decimal; Value = dbNull }, Value.ofNullable<decimal> (Nullable<decimal>())) 
    Assert.Equal({ DbType = DbType.Double; Value = dbNull }, Value.ofNullable<float> (Nullable<float>()))
    Assert.Equal({ DbType = DbType.Single; Value = dbNull }, Value.ofNullable<float32> (Nullable<float32>()))
