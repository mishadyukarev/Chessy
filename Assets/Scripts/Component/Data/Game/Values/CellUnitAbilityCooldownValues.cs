﻿using System;

namespace Game.Game
{
    public sealed class CellUnitAbilityCooldownValues
    {
        internal static int NeedAfterAbility(in AbilityTypes ability)
        {
            switch (ability)
            {
                case AbilityTypes.CircularAttack: return 2;
                case AbilityTypes.BonusNear: return 3;
                case AbilityTypes.GrowAdultForest: return 5;
                case AbilityTypes.StunElfemale: return 5;
                case AbilityTypes.ChangeDirectionWind: return 6;

                case AbilityTypes.DirectWave: return 5;
                case AbilityTypes.ActiveAroundBonusSnowy: return 5;
                case AbilityTypes.IceWall: return 5;

                default: throw new Exception();
            }
        }
    }
}