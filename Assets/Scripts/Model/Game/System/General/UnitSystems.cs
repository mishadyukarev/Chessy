using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class UnitSystems
    {
        internal readonly SetNewUnitOnCellS SetNewUnitS;
        internal readonly ShiftUnitS ShiftUnitS;
        internal readonly ClearUnitS ClearUnitS;
        internal readonly SetEffectsUnitS SetEffectsS;
        internal readonly SetMainUnitS SetMainS;
        internal readonly SetStatsUnitS SetStatsS;
        internal readonly SetMainToolWeaponUnitS SetMainTWS;
        internal readonly SetExtraToolWeaponS_M SetExtraTWS;
        internal readonly SetLastDiedS SetLastDiedS;
        internal readonly AttackShieldS AttackShieldS;
        internal readonly SetUnitS SetUnitS;
        internal readonly AttackUnitS AttackUnitS;
        internal readonly KillUnitS KillUnitS;

        internal UnitSystems(in SystemsModelGame sMG, in EntitiesModelGame eMG)
        {
            SetNewUnitS = new SetNewUnitOnCellS(sMG, eMG);
            ShiftUnitS = new ShiftUnitS(sMG, eMG);
            ClearUnitS = new ClearUnitS(sMG, eMG);
            SetEffectsS = new SetEffectsUnitS(sMG, eMG);
            SetMainS = new SetMainUnitS(sMG, eMG);
            SetStatsS = new SetStatsUnitS(sMG, eMG);
            SetMainTWS = new SetMainToolWeaponUnitS(sMG, eMG);
            SetExtraTWS = new SetExtraToolWeaponS_M(sMG, eMG);
            SetLastDiedS = new SetLastDiedS(sMG, eMG);
            SetUnitS = new SetUnitS(sMG, eMG);
            AttackShieldS = new AttackShieldS(sMG, eMG);
            KillUnitS = new KillUnitS(sMG, eMG);
            AttackUnitS = new AttackUnitS(sMG, eMG);
        }
    }
}