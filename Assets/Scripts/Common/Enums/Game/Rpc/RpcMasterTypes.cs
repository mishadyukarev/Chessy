namespace Chessy.Game
{
    public enum RpcMasterTypes
    {
        None,

        Ready,
        Done,
        Shift,
        Attack,
        ConditionUnit,
        Mistake,
        SetUnit,
        Sound,
        GetHero,

        GiveTakeToolWeapon,

        PickFraction,

        UniqueAbility,

        End
    }
}