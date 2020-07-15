namespace Mutex.DeclarativeSql

type BindName = string

type Parameter = BindName * Value

[<NoComparison>]
type Command = {
    Sql : string
    Parameters : Parameter list
}

[<NoComparison>]
[<NoEquality>]
type ScalarCommand<'ret> = {
    Sql : string
    Parameters : Parameter list
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
    Parameters : Parameter list
    Value : GetValue<'ret>
}

// Stored Procedure

[<NoComparison>]
type StoredProcedureCommand = {
    StoredProcedure : string
    Parameters : Parameter list
}

[<NoComparison>]
[<NoEquality>]
type StoredProcedureScalarCommand<'ret> = {
    StoredProcedure : string
    Parameters : Parameter list
    ScalarValue : obj -> 'ret
}

[<NoComparison>]
[<NoEquality>]
type StoredProcedureSelectCommand<'ret> = {
    StoredProcedure : string
    Parameters : Parameter list
    Value : GetValue<'ret>
}
