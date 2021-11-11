namespace Chessy.Game
{
    public enum RpcMasterTypes
    {
        None,

        Ready,
        Done,
        Build,
        DestroyBuild,
        Shift,
        Attack,
        ConditionUnit,
        Mistake,
        CreateUnit,
        MeltOre,
        SetUnit,
        Sound,
        PickUpgrade,
        GetHero,
        UpgradeUnit,

        ToNewUnit,
        FromToNewUnit,

        GiveTakeToolWeapon,
        BuyRes,

        UniqAbil,
    }
}