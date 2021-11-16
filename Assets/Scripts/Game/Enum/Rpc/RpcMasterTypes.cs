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
        GetHero,
        UpgradeUnit,

        ToNewUnit,
        FromToNewUnit,

        GiveTakeToolWeapon,
        BuyRes,

        UpgUnits,

        UniqAbil,

        Sync,
    }
}