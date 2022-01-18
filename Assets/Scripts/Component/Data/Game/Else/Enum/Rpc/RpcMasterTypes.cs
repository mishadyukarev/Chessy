namespace Game.Game
{
    public enum RpcMasterTypes
    {
        None,

        Ready,
        First = Ready,

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
        UpgradeCellUnit,

        ToNewUnit,
        CreateHeroFromTo,

        GiveTakeToolWeapon,
        BuyRes,

        UpgUnits,
        UpgCenterBuild,
        UpgWater,

        UniqAbil,

        End
    }
}