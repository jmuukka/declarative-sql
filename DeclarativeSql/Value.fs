namespace Mutex.DeclarativeSql

open System

[<NoComparison>]
type Value =
| Int16 of int16
| Int32 of int
| Int64 of int64
| Bool of bool
| Decimal of decimal
| DateTime of DateTime
| Float of float
| Float32 of float32
| Byte of byte
| UInt16 of uint16
| UInt32 of uint32
| UInt64 of uint64
| SByte of sbyte
| String of string
| ByteArray of byte array
| Object of obj
