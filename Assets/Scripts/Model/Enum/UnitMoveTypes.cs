namespace Chessy.Model.Enum
{
    public enum UnitMoveTypes : byte
    {
        None,

        MoveRight0,
        MoveRightUp45,
        MoveUp90,
        MoveUpLeft135,
        MoveLeft180,
        MoveLeftDown225,
        MoveDown270,
        MoveDownRight315,

        Protect,
        Relax,
        ResetCondition,

        End
    }
}
