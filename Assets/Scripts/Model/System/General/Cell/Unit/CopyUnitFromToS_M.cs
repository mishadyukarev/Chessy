//namespace Chessy.Model.System
//{
//    sealed partial class UnitSystems : SystemModelAbstract
//    {
//        internal void CopyUnitFromTo(in byte cellIdxFrom, in byte cellIdxTo)
//        {
//            _unitCs[cellIdxTo).CopyFrom(_unitCs[cellIdxFrom));
//            _effectsUnitCs[cellIdxTo).CopyEffects(_effectsUnitCs[cellIdxFrom));
//            _e.UnitE(cellIdxTo).Set(_e.UnitE(cellIdxFrom));
//            _e.MainToolWeaponC(cellIdxTo).CopyMainTW(_e.MainToolWeaponC(cellIdxFrom));
//            CopyExtraTW(cellIdxFrom, cellIdxTo);


//            _e.SkinInfoUnitC(cellIdxTo) = _e.SkinInfoUnitC(cellIdxFrom);

//            _unitCs[cellIdxTo).Possition = _unitCs[cellIdxFrom).Possition;

//            _unitCs[cellIdxFrom).HowManySecondUnitWasHereInRelax = 0;
//            _unitCs[cellIdxTo).HowManySecondUnitWasHereInRelax = 0;

//            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
//            {
//                _e.UnitButtonAbilitiesC(cellIdxTo).SetAbility(buttonT, _e.UnitButtonAbilitiesC(cellIdxFrom).Ability(buttonT));
//            }

//            _cooldownAbilityCs[cellIdxTo).Set(_cooldownAbilityCs[cellIdxFrom));
//        }
//    }
//}