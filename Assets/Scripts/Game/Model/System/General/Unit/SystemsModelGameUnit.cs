using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;

namespace Chessy.Game.Model.System
{
    sealed class SystemsModelGameUnit : SystemModelGameAbs
    {
        internal readonly AttackUnitS AttackUnitS;
        internal readonly KillUnitS KillUnitS;
        internal readonly AttackShieldS AttackShieldS;
        internal readonly SetLastDiedS SetLastDiedS;
        internal readonly ShiftUnitS ShiftUnitS;
        internal readonly SetNewUnitOnCellS SetNewUnitS;
        internal readonly SetStatsUnitS SetStatsUnitS;
        internal readonly SetExtraToolWeaponS SetExtraTWS;
        internal readonly SetMainToolWeaponUnitS SetMainTWS;
        internal readonly SetMainUnitS SetMainUnitS;
        internal readonly ClearUnitS ClearUnitS;

        internal SystemsModelGameUnit(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(eMGame)
        {
            SetNewUnitS = new SetNewUnitOnCellS(sMGame, eMGame);
            SetStatsUnitS = new SetStatsUnitS(eMGame);
            SetLastDiedS = new SetLastDiedS(eMGame);
            AttackShieldS = new AttackShieldS(eMGame);
            SetExtraTWS = new SetExtraToolWeaponS(eMGame);
            SetMainTWS = new SetMainToolWeaponUnitS(eMGame);
            ShiftUnitS = new ShiftUnitS(eMGame);
            SetMainUnitS = new SetMainUnitS(eMGame);
            ClearUnitS = new ClearUnitS(eMGame);

            KillUnitS = new KillUnitS(this, eMGame);
            AttackUnitS = new AttackUnitS(KillUnitS, eMGame);
        }
    }
}