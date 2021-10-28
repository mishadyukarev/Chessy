﻿namespace Scripts.Game
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
        SeedEnvironment,
        Fire,
        Sound,
        CircularAttackKing,
        BonusNearUnitKing,

        UpgradeUnit,
        OldToNewUnit,
        GiveTakeToolWeapon,

        UpgradeBuild,
    }
}