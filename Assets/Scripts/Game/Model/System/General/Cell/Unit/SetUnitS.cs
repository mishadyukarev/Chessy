using Chessy.Common;
using Chessy.Game.Entity.Model.Cell.Unit;

namespace Chessy.Game.Model.System
{
    sealed class SetUnitS
    {
        readonly CellSs _cellSs;
        readonly UnitEs _unitEs;

        internal SetUnitS(in CellSs cellSs, in UnitEs unitEs)
        {
            _cellSs = cellSs;
            _unitEs = unitEs;
        }

        public void Set(in UnitEs unitE)
        {
            _cellSs.SetMainS.Set(unitE.MainE);
            _cellSs.SetEffectsS.Set(unitE.EffectsE);
            _cellSs.SetStatsS.Set(unitE.StatsE);
            _cellSs.SetMainTWS.Set(unitE.MainToolWeaponE);
            _cellSs.SetExtraTWS.Set(unitE.ExtraToolWeaponE);

            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                _unitEs.Ability(buttonT) = unitE.Ability(buttonT);
            }
            for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
            {
                _unitEs.CoolDownC(abilityT) = unitE.CoolDownC(abilityT);
            }
            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                _unitEs.ForPlayer(playerT) = unitE.ForPlayer(playerT);
            }
        }
    }
}