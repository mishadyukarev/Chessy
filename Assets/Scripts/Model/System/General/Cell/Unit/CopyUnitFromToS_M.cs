//namespace Chessy.Model.System
//{
//    sealed partial class UnitSystems : SystemModelAbstract
//    {
//        internal void CopyUnitFromTo(in byte cellIdxFrom, in byte cellIdxTo)
//        {
//            _e.UnitMainC(cellIdxTo).CopyFrom(_e.UnitMainC(cellIdxFrom));
//            _e.UnitEffectsC(cellIdxTo).CopyEffects(_e.UnitEffectsC(cellIdxFrom));
//            _e.UnitE(cellIdxTo).Set(_e.UnitE(cellIdxFrom));
//            _e.MainToolWeaponC(cellIdxTo).CopyMainTW(_e.MainToolWeaponC(cellIdxFrom));
//            CopyExtraTW(cellIdxFrom, cellIdxTo);


//            _e.SkinInfoUnitC(cellIdxTo) = _e.SkinInfoUnitC(cellIdxFrom);

//            _e.UnitMainC(cellIdxTo).Possition = _e.UnitMainC(cellIdxFrom).Possition;

//            _e.UnitMainC(cellIdxFrom).HowManySecondUnitWasHereInRelax = 0;
//            _e.UnitMainC(cellIdxTo).HowManySecondUnitWasHereInRelax = 0;

//            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
//            {
//                _e.UnitButtonAbilitiesC(cellIdxTo).SetAbility(buttonT, _e.UnitButtonAbilitiesC(cellIdxFrom).Ability(buttonT));
//            }

//            _e.UnitCooldownAbilitiesC(cellIdxTo).Set(_e.UnitCooldownAbilitiesC(cellIdxFrom));
//        }
//    }
//}