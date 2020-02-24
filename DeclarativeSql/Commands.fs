﻿namespace Mutex.DeclarativeSql

type BindName = string

[<NoComparison>]
type Command = {
    Sql : string
    Parameters : (BindName * DbValue) list
}

[<NoComparison>]
[<NoEquality>]
type ScalarCommand<'ret> = {
    Sql : string
    Parameters : (BindName * DbValue) list
    ScalarValue : obj -> 'ret
}

type ZeroBasedIndex = int
type ColumnName = string

[<NoComparison>]
[<NoEquality>]
type GetValue<'ret> =
| ObjArray of (obj array -> 'ret)
| Indexed of ((ZeroBasedIndex -> obj) -> 'ret)
| Named of ((ColumnName -> obj) -> 'ret)

[<NoComparison>]
[<NoEquality>]
type SelectCommand<'ret> = {
    Sql : string
    Parameters : (BindName * DbValue) list
    Value : GetValue<'ret>
}
