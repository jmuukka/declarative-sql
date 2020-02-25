namespace Mutex.DeclarativeSql

open System

module Obj =

    let private dbNull =
        DBNull.Value :> obj

    let int16 (value : obj) =
        Convert.ToInt16(value)

    let int (value : obj) =
        Convert.ToInt32(value)

    let int64 (value : obj) =
        Convert.ToInt64(value)

    let bool (value : obj) =
        Convert.ToBoolean(value)

    let decimal (value : obj) =
        Convert.ToDecimal(value)

    let datetime (value : obj) =
        Convert.ToDateTime(value)

    let float (value : obj) =
        Convert.ToDouble(value)

    let float32 (value : obj) =
        Convert.ToSingle(value)

    let byte (value : obj) =
        Convert.ToByte(value)

    let uint16 (value : obj) =
        Convert.ToUInt16(value)

    let uint32 (value : obj) =
        Convert.ToUInt32(value)

    let uint64 (value : obj) =
        Convert.ToUInt64(value)

    let sbyte (value : obj) =
        Convert.ToSByte(value)

    let string (value : obj) =
        match value with
        | null -> null
        | _ when value = dbNull -> null
        | _ -> Convert.ToString(value)

    let byteArray (value : obj) =
        match value with
        | null -> null
        | _ when value = dbNull -> null
        | _ -> value :?> (byte array)

    let option<'t> (value : obj) =
        match value with
        | null -> None
        | _ when value = dbNull -> None
        | _ -> Some (value :?> 't)

    let nullable<'t when 't : struct
                     and 't :> ValueType
                     and 't : (new: unit -> 't)> (value : obj) =
        match value with
        | null -> Nullable<'t>()
        | _ when value = dbNull -> Nullable<'t>()
        | _ -> value :?> 't |> Nullable<'t>
