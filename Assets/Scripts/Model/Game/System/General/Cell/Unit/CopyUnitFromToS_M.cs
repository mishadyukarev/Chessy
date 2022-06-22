using Chessy.Common;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitSystems
    {
        internal void CopyUnitFromTo(in byte cellIdxFrom, in byte cellIdxTo)
        {
            _e.UnitMainE(cellIdxTo).CopyFrom(_e.UnitMainE(cellIdxFrom));
            _e.UnitEffectsE(cellIdxTo).CopyEffects(_e.UnitEffectsE(cellIdxFrom));
            _e.StatsUnitE(cellIdxTo).Set(_e.StatsUnitE(cellIdxFrom));
            _e.MainToolWeaponE(cellIdxTo).CopyMainTW(_e.MainToolWeaponE(cellIdxFrom));
            CopyExtraTW(cellIdxFrom, cellIdxTo);

            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                _e.UnitButtonAbilitiesC(cellIdxTo).SetAbility(buttonT, _e.UnitButtonAbilitiesC(cellIdxFrom).Ability(buttonT));
            }

            _e.UnitCooldownAbilitiesC(cellIdxTo).Set(_e.UnitCooldownAbilitiesC(cellIdxFrom));
        }
    }
}