﻿namespace Game.Game
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

        UpgCenterUnits,
        UpgCenterBuild,
        UpgWater,

        UniqueAbility,

        End
    }
}