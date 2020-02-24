module ObjUnitTests

open System
open Xunit
open Mutex.DeclarativeSql
open Mutex.DeclarativeSql.Obj

[<Fact>]
let ``simple value as obj is converted to target type`` () =
    let now = DateTime.UtcNow

    Assert.Equal(1s, 1s :> obj |> int16)
    Assert.Equal(1, 1 :> obj |> int)
    Assert.Equal(1L, 1L :> obj |> int64)
    Assert.Equal(true, true :> obj |> bool)
    Assert.Equal(1m, 1m :> obj |> decimal)
    Assert.Equal(now, now :> obj |> datetime)
    Assert.Equal(1.0, 1.0 :> obj |> float)
    Assert.Equal(1.0f, 1.0f :> obj |> float32)
    Assert.Equal(1uy, 1uy :> obj |> byte)
    Assert.Equal(1us, 1us :> obj |> uint16)
    Assert.Equal(1u, 1u :> obj |> uint32)
    Assert.Equal(1UL, 1UL :> obj |> uint64)
    Assert.Equal(1y, 1y :> obj |> sbyte)

[<Fact>]
let ``string as obj is converted to string`` () =
    Assert.Equal(null, null |> string)
    Assert.Equal(null, DBNull.Value :> obj |> string)
    Assert.Equal("ä", "ä" :> obj |> string)

[<Fact>]
let ``byte array as obj is converted to byte array`` () =
    let bytes = [|84uy|]

    Assert.Equal<byte array>(null, null |> byteArray)
    Assert.Equal<byte array>(null, DBNull.Value :> obj |> byteArray)
    Assert.Equal<byte array>(bytes, bytes :> obj |> byteArray)

[<Fact>]
let ``value as obj is converted to option target type`` () =
    let now = DateTime.UtcNow

    Assert.Equal(Some 1s, 1s :> obj |> option<int16>)
    Assert.Equal(Some 1, 1 :> obj |> option<int>)
    Assert.Equal(Some 1L, 1L :> obj |> option<int64>)
    Assert.Equal(Some true, true :> obj |> option<bool>)
    Assert.Equal(Some 1m, 1m :> obj |> option<decimal>)
    Assert.Equal(Some now, now :> obj |> option<DateTime>)
    Assert.Equal(Some 1.0, 1.0 :> obj |> option<float>)
    Assert.Equal(Some 1.0f, 1.0f :> obj |> option<float32>)
    Assert.Equal(Some 1uy, 1uy :> obj |> option<byte>)
    Assert.Equal(Some 1us, 1us :> obj |> option<uint16>)
    Assert.Equal(Some 1u, 1u :> obj |> option<uint32>)
    Assert.Equal(Some 1UL, 1UL :> obj |> option<uint64>)
    Assert.Equal(Some 1y, 1y :> obj |> option<sbyte>)

[<Fact>]
let ``nullable value as obj is converted to option target type`` () =
    let now = DateTime.UtcNow

    Assert.Equal(Some 1s, Nullable<int16> 1s :> obj |> option<int16>)
    Assert.Equal(Some 1, Nullable<int> 1 :> obj |> option<int>)
    Assert.Equal(Some 1L, Nullable<int64> 1L :> obj |> option<int64>)
    Assert.Equal(Some true, Nullable<bool> true :> obj |> option<bool>)
    Assert.Equal(Some 1m, Nullable<decimal> 1m :> obj |> option<decimal>)
    Assert.Equal(Some now, Nullable<DateTime> now :> obj |> option<DateTime>)
    Assert.Equal(Some 1.0, Nullable<float> 1.0 :> obj |> option<float>)
    Assert.Equal(Some 1.0f, Nullable<float32> 1.0f :> obj |> option<float32>)
    Assert.Equal(Some 1uy, Nullable<byte> 1uy :> obj |> option<byte>)
    Assert.Equal(Some 1us, Nullable<uint16> 1us :> obj |> option<uint16>)
    Assert.Equal(Some 1u, Nullable<uint32> 1u :> obj |> option<uint32>)
    Assert.Equal(Some 1UL, Nullable<uint64> 1UL :> obj |> option<uint64>)
    Assert.Equal(Some 1y, Nullable<sbyte> 1y :> obj |> option<sbyte>)

[<Fact>]
let ``DBNull as obj is converted to option target type`` () =
    Assert.Equal(None, DBNull.Value :> obj |> option<int16>)
    Assert.Equal(None, DBNull.Value :> obj |> option<int>)
    Assert.Equal(None, DBNull.Value :> obj |> option<int64>)
    Assert.Equal(None, DBNull.Value :> obj |> option<bool>)
    Assert.Equal(None, DBNull.Value :> obj |> option<decimal>)
    Assert.Equal(None, DBNull.Value :> obj |> option<DateTime>)
    Assert.Equal(None, DBNull.Value :> obj |> option<float>)
    Assert.Equal(None, DBNull.Value :> obj |> option<float32>)
    Assert.Equal(None, DBNull.Value :> obj |> option<byte>)
    Assert.Equal(None, DBNull.Value :> obj |> option<uint16>)
    Assert.Equal(None, DBNull.Value :> obj |> option<uint32>)
    Assert.Equal(None, DBNull.Value :> obj |> option<uint64>)
    Assert.Equal(None, DBNull.Value :> obj |> option<sbyte>)

[<Fact>]
let ``nullable null as obj is converted to option target type`` () =
    Assert.Equal(None, Nullable<int16>() :> obj |> option<int16>)
    Assert.Equal(None, Nullable<int>() :> obj |> option<int>)
    Assert.Equal(None, Nullable<int64>() :> obj |> option<int64>)
    Assert.Equal(None, Nullable<bool>() :> obj |> option<bool>)
    Assert.Equal(None, Nullable<decimal>() :> obj |> option<decimal>)
    Assert.Equal(None, Nullable<DateTime>() :> obj |> option<DateTime>)
    Assert.Equal(None, Nullable<float>() :> obj |> option<float>)
    Assert.Equal(None, Nullable<float32>() :> obj |> option<float32>)
    Assert.Equal(None, Nullable<byte>() :> obj |> option<byte>)
    Assert.Equal(None, Nullable<uint16>() :> obj |> option<uint16>)
    Assert.Equal(None, Nullable<uint32>() :> obj |> option<uint32>)
    Assert.Equal(None, Nullable<uint64>() :> obj |> option<uint64>)
    Assert.Equal(None, Nullable<sbyte>() :> obj |> option<sbyte>)

[<Fact>]
let ``simple value converted to obj using ofValue equals simple value converted to obj`` () =
    let now = DateTime.UtcNow

    Assert.Equal(1s :> obj, Int16 1s |> ofValue)
    Assert.Equal(1 :> obj, Int32 1 |> ofValue)
    Assert.Equal(1L :> obj, Int64 1L |> ofValue)
    Assert.Equal(true :> obj, Bool true |> ofValue)
    Assert.Equal(1m :> obj, Decimal 1m |> ofValue)
    Assert.Equal(now :> obj, DateTime now |> ofValue)
    Assert.Equal(1.0 :> obj, Float 1.0 |> ofValue)
    Assert.Equal(1.0f :> obj, Float32 1.0f |> ofValue)
    Assert.Equal(1uy :> obj, Byte 1uy |> ofValue)
    Assert.Equal(1us :> obj, UInt16 1us |> ofValue)
    Assert.Equal(1u :> obj, UInt32 1u |> ofValue)
    Assert.Equal(1UL :> obj, UInt64 1UL |> ofValue)
    Assert.Equal(1y :> obj, SByte 1y |> ofValue)

[<Fact>]
let ``String value converted to obj using ofValue equals value converted to obj`` () =
    Assert.Equal("ö" :> obj, String "ö" |> ofValue)

[<Fact>]
let ``ByteArray value converted to obj using ofValue equals value converted to obj`` () =
    let bytes = [|84uy|]

    Assert.Equal(bytes :> obj, ByteArray bytes |> ofValue)

[<Fact>]
let ``Object value converted to obj using ofValue equals value converted to obj`` () =
    let bytes = [|84uy|]

    Assert.Equal(bytes :> obj, Object bytes |> ofValue)

[<Fact>]
let ``null String value converted to obj using ofValue equals DBNull`` () =
    Assert.Equal(DBNull.Value :> obj, String null |> ofValue)

[<Fact>]
let ``null ByteArray value converted to obj using ofValue equals DBNull`` () =
    Assert.Equal(DBNull.Value :> obj, ByteArray null |> ofValue)

[<Fact>]
let ``null Object value converted to obj using ofValue equals DBNull`` () =
    Assert.Equal(DBNull.Value :> obj, Object null |> ofValue)
