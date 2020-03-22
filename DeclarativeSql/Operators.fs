namespace Mutex.DeclarativeSql

open System

module Operators =

    let (=>) bindName (value : obj) : Parameter =
        match value with
        | null ->
            bindName, Value.ofObj value
        | :? string as v ->
            bindName, Value.ofString v
        | :? int16 as v ->
            bindName, Value.ofInt16 v
        | :? int as v ->
            bindName, Value.ofInt32 v
        | :? int64 as v ->
            bindName, Value.ofInt64 v
        | :? uint16 as v ->
            bindName, Value.ofUInt16 v
        | :? uint32 as v ->
            bindName, Value.ofUInt32 v
        | :? uint64 as v ->
            bindName, Value.ofUInt64 v
        | :? byte as v ->
            bindName, Value.ofByte v
        | :? sbyte as v ->
            bindName, Value.ofSByte v
        | :? bool as v ->
            bindName, Value.ofBool v
        | :? DateTime as v ->
            bindName, Value.ofDateTime v
        | :? decimal as v ->
            bindName, Value.ofDecimal v
        | :? float as v ->
            bindName, Value.ofFloat v
        | :? float32 as v ->
            bindName, Value.ofFloat32 v
        | :? (byte array) as v ->
            bindName, Value.ofByteArray v
        | :? Option<int16> as v ->
            bindName, Value.ofOption v
        | :? Option<int> as v ->
            bindName, Value.ofOption v
        | :? Option<int64> as v ->
            bindName, Value.ofOption v
        | :? Option<uint16> as v ->
            bindName, Value.ofOption v
        | :? Option<uint32> as v ->
            bindName, Value.ofOption v
        | :? Option<uint64> as v ->
            bindName, Value.ofOption v
        | :? Option<byte> as v ->
            bindName, Value.ofOption v
        | :? Option<sbyte> as v ->
            bindName, Value.ofOption v
        | :? Option<bool> as v ->
            bindName, Value.ofOption v
        | :? Option<DateTime> as v ->
            bindName, Value.ofOption v
        | :? Option<decimal> as v ->
            bindName, Value.ofOption v
        | :? Option<float> as v ->
            bindName, Value.ofOption v
        | :? Option<float32> as v ->
            bindName, Value.ofOption v
        | :? Option<string> as v ->
            bindName, Value.ofOption v
        | :? Option<byte array> as v ->
            bindName, Value.ofOption v
        | v ->
            bindName, Value.ofObj v
