using Chessy.Common;

namespace Chessy.Model
{
    sealed partial class UnitSystems
    {
        internal void CopyUnitFromTo(in byte cellIdxFrom, in byte cellIdxTo)
        {
            _e.UnitMainC(cellIdxTo).CopyFrom(_e.UnitMainC(cellIdxFrom));
            _e.UnitEffectsC(cellIdxTo).CopyEffects(_e.UnitEffectsC(cellIdxFrom));
            _e.UnitE(cellIdxTo).Set(_e.UnitE(cellIdxFrom));
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