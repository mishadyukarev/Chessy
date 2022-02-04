namespace Game.Game
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
        CreateUnit,
        MeltOre,
        SetUnit,
        Sound,
        GetHero,
        UpgradeCellUnit,

        GiveTakeToolWeapon,
        BuyRes,

        UpgCenterUnits,
        UpgCenterBuild,
        UpgWater,

        UniqueAbility,

        End
    }
}