namespace Mutex.DeclarativeSql

open System
open System.Data

[<NoComparison>]
type DbValue = {
    DbType : DbType
    Value : obj
}

module DbValue =

    let private resolveDbType<'t> () =
        match typeof<'t> with
        | t when t = typeof<int16> -> DbType.Int16
        | t when t = typeof<int32> -> DbType.Int32
        | t when t = typeof<int64> -> DbType.Int64
        | t when t = typeof<bool> -> DbType.Boolean
        | t when t = typeof<string> -> DbType.String
        | t when t = typeof<DateTime> -> DbType.DateTime2
        | t when t = typeof<decimal> -> DbType.Decimal
        | t when t = typeof<float> -> DbType.Double
        | t when t = typeof<float32> -> DbType.Single
        | t when t = typeof<uint16> -> DbType.UInt16
        | t when t = typeof<uint32> -> DbType.UInt32
        | t when t = typeof<uint64> -> DbType.UInt64
        | t when t = typeof<byte> -> DbType.Byte
        | t when t = typeof<sbyte> -> DbType.SByte
        | t when t = typeof<byte array> -> DbType.Binary
        | _ -> DbType.Object

    let ofInt16 (value : int16) = {
        DbType = DbType.Int16
        Value = value :> obj
    }

    let ofInt32 (value : int) = {
        DbType = DbType.Int32
        Value = value :> obj
    }

    let ofInt64 (value : int64) = {
        DbType = DbType.Int64
        Value = value :> obj
    }

    let ofBool (value : bool) = {
        DbType = DbType.Boolean
        Value = value :> obj
    }

    let ofDateTime (value : DateTime) = {
        DbType = DbType.DateTime2
        Value = value :> obj
    }

    let ofDecimal (value : decimal) = {
        DbType = DbType.Decimal
        Value = value :> obj
    }

    let ofFloat (value : float) = {
        DbType = DbType.Double
        Value = value :> obj
    }

    let ofFloat32 (value : float32) = {
        DbType = DbType.Single
        Value = value :> obj
    }

    let ofUInt16 (value : uint16) = {
        DbType = DbType.UInt16
        Value = value :> obj
    }

    let ofUInt32 (value : uint32) = {
        DbType = DbType.UInt32
        Value = value :> obj
    }

    let ofUInt64 (value : uint64) = {
        DbType = DbType.UInt64
        Value = value :> obj
    }

    let ofByte (value : byte) = {
        DbType = DbType.Byte
        Value = value :> obj
    }

    let ofSByte (value : sbyte) = {
        DbType = DbType.SByte
        Value = value :> obj
    }

    let ofByteArray (value : byte array) = {
        DbType = DbType.Binary
        Value = match value with
                | null -> DBNull.Value :> obj
                | _ -> value :> obj
    }

    let ofString (value : string) = {
        DbType = DbType.String
        Value = match value with
                | null -> DBNull.Value :> obj
                | _ -> value :> obj
    }

    let ofOption<'t> (value : 't option) = {
        DbType = resolveDbType<'t>()
        Value = match value with
                | Some value -> value :> obj
                | None -> DBNull.Value :> obj
    }

    let ofNullable<'t when 't : struct
                       and 't :> ValueType
                       and 't : (new: unit -> 't)> (value : Nullable<'t>) = {
        DbType = resolveDbType<'t>()
        Value = match value.HasValue with
                | true -> value.Value :> obj
                | false -> DBNull.Value :> obj
    }
