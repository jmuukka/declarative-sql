module ObjUnitTests

open System
open Xunit
open Mutex.DeclarativeSql.Obj

[<Fact>]
let ``simple value as obj is converted to target type`` () =
    let now = DateTime.UtcNow

    Assert.Equal(1s, 1s :> obj |> int16)
    Assert.Equal(1, 1 :> obj |> int)
    Assert.Equal(1L, 1L :> obj |> int64)
    Assert.Equal(1us, 1us :> obj |> uint16)
    Assert.Equal(1u, 1u :> obj |> uint32)
    Assert.Equal(1UL, 1UL :> obj |> uint64)
    Assert.Equal(1uy, 1uy :> obj |> byte)
    Assert.Equal(1y, 1y :> obj |> sbyte)
    Assert.Equal(true, true :> obj |> bool)
    Assert.Equal(now, now :> obj |> datetime)
    Assert.Equal(1m, 1m :> obj |> decimal)
    Assert.Equal(1.0, 1.0 :> obj |> float)
    Assert.Equal(1.0f, 1.0f :> obj |> float32)

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
    Assert.Equal(Some 1us, 1us :> obj |> option<uint16>)
    Assert.Equal(Some 1u, 1u :> obj |> option<uint32>)
    Assert.Equal(Some 1UL, 1UL :> obj |> option<uint64>)
    Assert.Equal(Some 1uy, 1uy :> obj |> option<byte>)
    Assert.Equal(Some 1y, 1y :> obj |> option<sbyte>)
    Assert.Equal(Some true, true :> obj |> option<bool>)
    Assert.Equal(Some now, now :> obj |> option<DateTime>)
    Assert.Equal(Some 1m, 1m :> obj |> option<decimal>)
    Assert.Equal(Some 1.0, 1.0 :> obj |> option<float>)
    Assert.Equal(Some 1.0f, 1.0f :> obj |> option<float32>)

[<Fact>]
let ``nullable value as obj is converted to option target type`` () =
    let now = DateTime.UtcNow

    Assert.Equal(Some 1s, Nullable<int16> 1s :> obj |> option<int16>)
    Assert.Equal(Some 1, Nullable<int> 1 :> obj |> option<int>)
    Assert.Equal(Some 1L, Nullable<int64> 1L :> obj |> option<int64>)
    Assert.Equal(Some 1us, Nullable<uint16> 1us :> obj |> option<uint16>)
    Assert.Equal(Some 1u, Nullable<uint32> 1u :> obj |> option<uint32>)
    Assert.Equal(Some 1UL, Nullable<uint64> 1UL :> obj |> option<uint64>)
    Assert.Equal(Some 1uy, Nullable<byte> 1uy :> obj |> option<byte>)
    Assert.Equal(Some 1y, Nullable<sbyte> 1y :> obj |> option<sbyte>)
    Assert.Equal(Some true, Nullable<bool> true :> obj |> option<bool>)
    Assert.Equal(Some now, Nullable<DateTime> now :> obj |> option<DateTime>)
    Assert.Equal(Some 1m, Nullable<decimal> 1m :> obj |> option<decimal>)
    Assert.Equal(Some 1.0, Nullable<float> 1.0 :> obj |> option<float>)
    Assert.Equal(Some 1.0f, Nullable<float32> 1.0f :> obj |> option<float32>)

[<Fact>]
let ``DBNull as obj is converted to option target type`` () =
    Assert.Equal(None, DBNull.Value :> obj |> option<int16>)
    Assert.Equal(None, DBNull.Value :> obj |> option<int>)
    Assert.Equal(None, DBNull.Value :> obj |> option<int64>)
    Assert.Equal(None, DBNull.Value :> obj |> option<uint16>)
    Assert.Equal(None, DBNull.Value :> obj |> option<uint32>)
    Assert.Equal(None, DBNull.Value :> obj |> option<uint64>)
    Assert.Equal(None, DBNull.Value :> obj |> option<byte>)
    Assert.Equal(None, DBNull.Value :> obj |> option<sbyte>)
    Assert.Equal(None, DBNull.Value :> obj |> option<bool>)
    Assert.Equal(None, DBNull.Value :> obj |> option<DateTime>)
    Assert.Equal(None, DBNull.Value :> obj |> option<decimal>)
    Assert.Equal(None, DBNull.Value :> obj |> option<float>)
    Assert.Equal(None, DBNull.Value :> obj |> option<float32>)

[<Fact>]
let ``nullable null as obj is converted to option target type`` () =
    Assert.Equal(None, Nullable<int16>() :> obj |> option<int16>)
    Assert.Equal(None, Nullable<int>() :> obj |> option<int>)
    Assert.Equal(None, Nullable<int64>() :> obj |> option<int64>)
    Assert.Equal(None, Nullable<uint16>() :> obj |> option<uint16>)
    Assert.Equal(None, Nullable<uint32>() :> obj |> option<uint32>)
    Assert.Equal(None, Nullable<uint64>() :> obj |> option<uint64>)
    Assert.Equal(None, Nullable<byte>() :> obj |> option<byte>)
    Assert.Equal(None, Nullable<sbyte>() :> obj |> option<sbyte>)
    Assert.Equal(None, Nullable<bool>() :> obj |> option<bool>)
    Assert.Equal(None, Nullable<DateTime>() :> obj |> option<DateTime>)
    Assert.Equal(None, Nullable<decimal>() :> obj |> option<decimal>)
    Assert.Equal(None, Nullable<float>() :> obj |> option<float>)
    Assert.Equal(None, Nullable<float32>() :> obj |> option<float32>)

[<Fact>]
let ``value as obj is converted to nullable target type`` () =
    let now = DateTime.UtcNow

    Assert.Equal(Nullable<int16> 1s, 1s :> obj |> nullable<int16>)
    Assert.Equal(Nullable<int> 1, 1 :> obj |> nullable<int>)
    Assert.Equal(Nullable<int64> 1L, 1L :> obj |> nullable<int64>)
    Assert.Equal(Nullable<uint16> 1us, 1us :> obj |> nullable<uint16>)
    Assert.Equal(Nullable<uint32> 1u, 1u :> obj |> nullable<uint32>)
    Assert.Equal(Nullable<uint64> 1UL, 1UL :> obj |> nullable<uint64>)
    Assert.Equal(Nullable<byte> 1uy, 1uy :> obj |> nullable<byte>)
    Assert.Equal(Nullable<sbyte> 1y, 1y :> obj |> nullable<sbyte>)
    Assert.Equal(Nullable<bool> true, true :> obj |> nullable<bool>)
    Assert.Equal(Nullable<DateTime> now, now :> obj |> nullable<DateTime>)
    Assert.Equal(Nullable<decimal> 1m, 1m :> obj |> nullable<decimal>)
    Assert.Equal(Nullable<float> 1.0, 1.0 :> obj |> nullable<float>)
    Assert.Equal(Nullable<float32> 1.0f, 1.0f :> obj |> nullable<float32>)

[<Fact>]
let ``nullable value as obj is converted to nullable target type`` () =
    let now = DateTime.UtcNow

    Assert.Equal(Nullable<int16> 1s, Nullable<int16> 1s :> obj |> nullable<int16>)
    Assert.Equal(Nullable<int> 1, Nullable<int> 1 :> obj |> nullable<int>)
    Assert.Equal(Nullable<int64> 1L, Nullable<int64> 1L :> obj |> nullable<int64>)
    Assert.Equal(Nullable<uint16> 1us, Nullable<uint16> 1us :> obj |> nullable<uint16>)
    Assert.Equal(Nullable<uint32> 1u, Nullable<uint32> 1u :> obj |> nullable<uint32>)
    Assert.Equal(Nullable<uint64> 1UL, Nullable<uint64> 1UL :> obj |> nullable<uint64>)
    Assert.Equal(Nullable<byte> 1uy, Nullable<byte> 1uy :> obj |> nullable<byte>)
    Assert.Equal(Nullable<sbyte> 1y, Nullable<sbyte> 1y :> obj |> nullable<sbyte>)
    Assert.Equal(Nullable<bool> true, Nullable<bool> true :> obj |> nullable<bool>)
    Assert.Equal(Nullable<DateTime> now, Nullable<DateTime> now :> obj |> nullable<DateTime>)
    Assert.Equal(Nullable<decimal> 1m, Nullable<decimal> 1m :> obj |> nullable<decimal>)
    Assert.Equal(Nullable<float> 1.0, Nullable<float> 1.0 :> obj |> nullable<float>)
    Assert.Equal(Nullable<float32> 1.0f, Nullable<float32> 1.0f :> obj |> nullable<float32>)

[<Fact>]
let ``DBNull as obj is converted to nullable target type`` () =
    Assert.Equal(Nullable<int16>(), DBNull.Value :> obj |> nullable<int16>)
    Assert.Equal(Nullable<int>(), DBNull.Value :> obj |> nullable<int>)
    Assert.Equal(Nullable<int64>(), DBNull.Value :> obj |> nullable<int64>)
    Assert.Equal(Nullable<uint16>(), DBNull.Value :> obj |> nullable<uint16>)
    Assert.Equal(Nullable<uint32>(), DBNull.Value :> obj |> nullable<uint32>)
    Assert.Equal(Nullable<uint64>(), DBNull.Value :> obj |> nullable<uint64>)
    Assert.Equal(Nullable<byte>(), DBNull.Value :> obj |> nullable<byte>)
    Assert.Equal(Nullable<sbyte>(), DBNull.Value :> obj |> nullable<sbyte>)
    Assert.Equal(Nullable<bool>(), DBNull.Value :> obj |> nullable<bool>)
    Assert.Equal(Nullable<DateTime>(), DBNull.Value :> obj |> nullable<DateTime>)
    Assert.Equal(Nullable<decimal>(), DBNull.Value :> obj |> nullable<decimal>)
    Assert.Equal(Nullable<float>(), DBNull.Value :> obj |> nullable<float>)
    Assert.Equal(Nullable<float32>(), DBNull.Value :> obj |> nullable<float32>)

[<Fact>]
let ``nullable null as obj is converted to nullable target type`` () =
    Assert.Equal(Nullable<int16>(), Nullable<int16>() :> obj |> nullable<int16>)
    Assert.Equal(Nullable<int>(), Nullable<int>() :> obj |> nullable<int>)
    Assert.Equal(Nullable<int64>(), Nullable<int64>() :> obj |> nullable<int64>)
    Assert.Equal(Nullable<uint16>(), Nullable<uint16>() :> obj |> nullable<uint16>)
    Assert.Equal(Nullable<uint32>(), Nullable<uint32>() :> obj |> nullable<uint32>)
    Assert.Equal(Nullable<uint64>(), Nullable<uint64>() :> obj |> nullable<uint64>)
    Assert.Equal(Nullable<byte>(), Nullable<byte>() :> obj |> nullable<byte>)
    Assert.Equal(Nullable<sbyte>(), Nullable<sbyte>() :> obj |> nullable<sbyte>)
    Assert.Equal(Nullable<bool>(), Nullable<bool>() :> obj |> nullable<bool>)
    Assert.Equal(Nullable<DateTime>(), Nullable<DateTime>() :> obj |> nullable<DateTime>)
    Assert.Equal(Nullable<decimal>(), Nullable<decimal>() :> obj |> nullable<decimal>)
    Assert.Equal(Nullable<float>(), Nullable<float>() :> obj |> nullable<float>)
    Assert.Equal(Nullable<float32>(), Nullable<float32>() :> obj |> nullable<float32>)
