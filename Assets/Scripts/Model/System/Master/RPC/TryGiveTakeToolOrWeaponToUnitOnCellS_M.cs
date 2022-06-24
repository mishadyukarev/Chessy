using Chessy.Model.Enum;
using Chessy.Model.Values;
using Chessy.Model.Values.Cell.Unit;
using Chessy.Model.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Model.Model.System
{
    public sealed partial class SystemsModel : IUpdate
    {
        internal void TryGiveTakeToolOrWeaponToUnitOnCellM(in ToolWeaponTypes twT, in LevelTypes levTW, in byte cellIdxForDoing, in Player sender)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? _e.WhoseMovePlayerT : sender.GetPlayer();

            if (_e.UnitT(cellIdxForDoing).Is(UnitTypes.Pawn))
            {
                if (_e.EnergyUnitC(cellIdxForDoing).Energy >= StepValues.FOR_GIVE_TAKE_TOOLWEAPON)
                {
                    if (twT == ToolWeaponTypes.BowCrossbow || twT == ToolWeaponTypes.Staff)
                    {
                        if (_e.ExtraToolWeaponT(cellIdxForDoing).HaveToolWeapon())
                        {
                            _e.ToolWeaponsC(_e.UnitPlayerT(cellIdxForDoing), _e.ExtraTWLevelT(cellIdxForDoing), _e.ExtraToolWeaponT(cellIdxForDoing))++;
                            _e.SetExtraToolWeaponT(cellIdxForDoing, ToolWeaponTypes.None);

                            _e.EnergyUnitC(cellIdxForDoing).Energy -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                        }
                        else
                        {


                            if (_e.MainToolWeaponT(cellIdxForDoing).Is(ToolWeaponTypes.Axe))
                            {


                                if (_e.MainTWLevelT(cellIdxForDoing).Is(LevelTypes.First))
                                {
                                    if (_e.PlayerInfoE(whoseMove).LevelE(levTW).ToolWeapons(twT) > 0)
                                    {
                                        _e.ToolWeaponsC(whoseMove, levTW, twT)--;

                                        _e.MainToolWeaponE(cellIdxForDoing).Set(twT, levTW);

                                        _e.EnergyUnitC(cellIdxForDoing).Energy -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                        ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);
                                    }
                                    else
                                    {
                                        var needRes = new Dictionary<ResourceTypes, float>();
                                        var canBuy = true;

                                        for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                        {
                                            var difAmountRes = _e.PlayerInfoE(whoseMove).ResourcesC(res).Resources - EconomyValues.ForBuyToolWeapon(twT, levTW, res);
                                            needRes.Add(res, EconomyValues.ForBuyToolWeapon(twT, levTW, res));

                                            if (canBuy) canBuy = difAmountRes >= 0;
                                        }

                                        if (canBuy)
                                        {
                                            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                                _e.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= EconomyValues.ForBuyToolWeapon(twT, levTW, resT);

                                            _e.EnergyUnitC(cellIdxForDoing).Energy -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                            _e.MainToolWeaponE(cellIdxForDoing).Set(twT, levTW);

                                            ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);

                                            if (_e.LessonT.Is(LessonTypes.GiveStaff, LessonTypes.GiveBowCrossbow))
                                            {
                                                _e.CommonInfoAboutGameC.SetNextLesson();
                                            }
                                        }
                                        else
                                        {
                                            MistakeEconomyToGeneral(sender, needRes);
                                        }

                                    }
                                }
                                else
                                {
                                    _e.ToolWeaponsC(whoseMove, _e.MainTWLevelT(cellIdxForDoing), _e.MainToolWeaponT(cellIdxForDoing))++;
                                    _e.MainToolWeaponE(cellIdxForDoing).Set(ToolWeaponTypes.Axe, LevelTypes.First);

                                    _e.EnergyUnitC(cellIdxForDoing).Energy -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                                }
                            }

                            else
                            {
                                _e.ToolWeaponsC(whoseMove, _e.MainTWLevelT(cellIdxForDoing), _e.MainToolWeaponT(cellIdxForDoing))++;
                                _e.MainToolWeaponE(cellIdxForDoing).Set(ToolWeaponTypes.Axe, LevelTypes.First);

                                _e.EnergyUnitC(cellIdxForDoing).Energy -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                            }
                        }
                    }

                    else if (twT == ToolWeaponTypes.Axe)
                    {
                        if (_e.MainToolWeaponT(cellIdxForDoing).Is(ToolWeaponTypes.Axe))
                        {
                            if (_e.MainTWLevelT(cellIdxForDoing).Is(LevelTypes.First))
                            {
                                if (_e.PlayerInfoE(whoseMove).LevelE(levTW).ToolWeapons(twT) > 0)
                                {
                                    _e.ToolWeaponsC(whoseMove, levTW, twT)--;
                                    _e.MainToolWeaponE(cellIdxForDoing).Set(twT, levTW);

                                    _e.EnergyUnitC(cellIdxForDoing).Energy -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                    ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);
                                }
                                else
                                {
                                    var needRes = new Dictionary<ResourceTypes, float>();
                                    var canBuy = true;

                                    for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                    {
                                        var difAmountRes = _e.PlayerInfoE(whoseMove).ResourcesC(res).Resources - EconomyValues.ForBuyToolWeapon(twT, levTW, res);
                                        needRes.Add(res, EconomyValues.ForBuyToolWeapon(twT, levTW, res));

                                        if (canBuy) canBuy = difAmountRes >= 0;
                                    }

                                    if (canBuy)
                                    {
                                        for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                            _e.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= EconomyValues.ForBuyToolWeapon(twT, levTW, resT);

                                        _e.EnergyUnitC(cellIdxForDoing).Energy -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                        _e.MainToolWeaponE(cellIdxForDoing).Set(twT, levTW);

                                        ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);

                                        if (_e.LessonT == LessonTypes.GiveIronAxe)
                                        {
                                            _e.CommonInfoAboutGameC.SetNextLesson();
                                        }

                                    }
                                    else
                                    {
                                        MistakeEconomyToGeneral(sender, needRes);
                                    }

                                }
                            }

                            else
                            {
                                _e.ToolWeaponsC(whoseMove, _e.MainTWLevelT(cellIdxForDoing), _e.MainToolWeaponT(cellIdxForDoing))++;
                                _e.MainToolWeaponE(cellIdxForDoing).Set(ToolWeaponTypes.Axe, LevelTypes.First);

                                _e.EnergyUnitC(cellIdxForDoing).Energy -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                            }
                        }

                        else
                        {
                            _e.ToolWeaponsC(whoseMove, _e.MainTWLevelT(cellIdxForDoing), _e.MainToolWeaponT(cellIdxForDoing))++;
                            _e.MainToolWeaponE(cellIdxForDoing).Set(ToolWeaponTypes.Axe, LevelTypes.First);

                            _e.EnergyUnitC(cellIdxForDoing).Energy -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                            ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);
                        }
                    }

                    else
                    {
                        var ownUnit_0 = _e.UnitPlayerT(cellIdxForDoing);

                        if (_e.MainToolWeaponT(cellIdxForDoing).Is(ToolWeaponTypes.BowCrossbow, ToolWeaponTypes.Staff))
                        {
                            _e.ToolWeaponsC(_e.UnitPlayerT(cellIdxForDoing), _e.MainTWLevelT(cellIdxForDoing), _e.MainToolWeaponT(cellIdxForDoing))++;
                            _e.MainToolWeaponE(cellIdxForDoing).Set(ToolWeaponTypes.Axe, LevelTypes.First);

                            _e.EnergyUnitC(cellIdxForDoing).Energy -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                        }

                        else
                        {
                            if (_e.MainToolWeaponT(cellIdxForDoing).Is(ToolWeaponTypes.Axe))
                            {
                                if (_e.ExtraToolWeaponT(cellIdxForDoing).HaveToolWeapon())
                                {
                                    _e.PlayerInfoE(ownUnit_0).LevelE(_e.ExtraTWLevelT(cellIdxForDoing)).ToolWeapons(_e.ExtraToolWeaponT(cellIdxForDoing))++;
                                    _e.SetExtraToolWeaponT(cellIdxForDoing, ToolWeaponTypes.None);

                                    _e.EnergyUnitC(cellIdxForDoing).Energy -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                    ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);
                                }

                                else if (_e.ToolWeaponsC(ownUnit_0, levTW, twT) > 0)
                                {
                                    _e.PlayerInfoE(ownUnit_0).LevelE(levTW).ToolWeapons(twT)--;


                                    _e.UnitExtraTWE(cellIdxForDoing).Set(twT, levTW, _e.ExtraTWProtection(cellIdxForDoing));

                                    _e.EnergyUnitC(cellIdxForDoing).Energy -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                    ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);
                                }

                                else
                                {
                                    var needRes = new Dictionary<ResourceTypes, float>();
                                    var canCreatBuild = true;

                                    for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                    {
                                        var difAmountRes = _e.PlayerInfoE(whoseMove).ResourcesC(res).Resources - EconomyValues.ForBuyToolWeapon(twT, levTW, res);
                                        needRes.Add(res, EconomyValues.ForBuyToolWeapon(twT, levTW, res));

                                        if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
                                    }

                                    if (canCreatBuild)
                                    {
                                        for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                            _e.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= EconomyValues.ForBuyToolWeapon(twT, levTW, resT);

                                        var protection = 0f;

                                        if (twT == ToolWeaponTypes.Shield)
                                        {
                                            protection = levTW == LevelTypes.First ? ToolWeaponValues.SHIELD_PROTECTION_LEVEL_FIRST
                                                : ToolWeaponValues.SHIELD_PROTECTION_LEVEL_SECOND;
                                        }

                                        _e.UnitExtraTWE(cellIdxForDoing).Set(twT, levTW, protection);

                                        _e.EnergyUnitC(cellIdxForDoing).Energy -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                        ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);


                                        if (_e.LessonT.Is(LessonTypes.GiveTakePickPawn, LessonTypes.GiveShield, LessonTypes.GiveSword))
                                        {
                                            //if (_eMG.LessonT == LessonTypes.GiveSword)
                                            //{
                                            //    _eMG.YoungForestC(StartValues.CELL_IDX_FOR_SHIFT_PAWN_TO_FIRE_ADULT_FOREST).Resources = 0;

                                            //    _eMG.AdultForestC(StartValues.CELL_IDX_FOR_SHIFT_PAWN_TO_FIRE_ADULT_FOREST).Resources = EnvironmentValues.MAX_RESOURCES;
                                            //    _eMG.BuildingTC(StartValues.CELL_IDX_FOR_SHIFT_PAWN_TO_FIRE_ADULT_FOREST).BuildingT = BuildingTypes.None;
                                            //}

                                            _e.CommonInfoAboutGameC.SetNextLesson();
                                        }
                                    }
                                    else
                                    {
                                        MistakeEconomyToGeneral(sender, needRes);
                                    }
                                }
                            }
                        }
                    }
                }

                else
                {
                    SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

        }
    }
}