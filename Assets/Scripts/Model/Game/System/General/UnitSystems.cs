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

        internal UnitSystems(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG)
        {
            SetNewUnitS = new SetNewUnitOnCellS(sMC, eMC, sMG, eMG);
            ShiftUnitS = new ShiftUnitS(sMC, eMC, sMG, eMG);
            ClearUnitS = new ClearUnitS(sMC, eMC, sMG, eMG);
            SetEffectsS = new SetEffectsUnitS(sMC, eMC, sMG, eMG);
            SetMainS = new SetMainUnitS(sMC, eMC, sMG, eMG);
            SetStatsS = new SetStatsUnitS(sMC, eMC, sMG, eMG);
            SetMainTWS = new SetMainToolWeaponUnitS(sMC, eMC, sMG, eMG);
            SetExtraTWS = new SetExtraToolWeaponS_M(sMC, eMC, sMG, eMG);
            SetLastDiedS = new SetLastDiedS(sMC, eMC, sMG, eMG);
            SetUnitS = new SetUnitS(sMC, eMC, sMG, eMG);
            AttackShieldS = new AttackShieldS(sMC, eMC, sMG, eMG);
            KillUnitS = new KillUnitS(sMC, eMC, sMG, eMG);
            AttackUnitS = new AttackUnitS(sMC, eMC, sMG, eMG);
        }
    }
}