using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        internal void TryGiveTakeToolOrWeaponToUnitOnCellM(in ToolWeaponTypes twT, in LevelTypes levTW, in byte cell_0, in Player sender)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? _eMG.WhoseMovePlayerT : sender.GetPlayer();

            if (_eMG.UnitTC(cell_0).Is(UnitTypes.Pawn))
            {
                if (_eMG.StepUnitC(cell_0).Steps >= StepValues.FOR_GIVE_TAKE_TOOLWEAPON)
                {
                    if (twT == ToolWeaponTypes.BowCrossbow || twT == ToolWeaponTypes.Staff)
                    {
                        if (_eMG.ExtraToolWeaponTC(cell_0).HaveToolWeapon)
                        {
                            _eMG.ToolWeaponsC(_eMG.UnitPlayerTC(cell_0).PlayerT, _eMG.ExtraTWLevelTC(cell_0).LevelT, _eMG.ExtraToolWeaponTC(cell_0).ToolWeaponT)++;
                            _eMG.ExtraToolWeaponTC(cell_0).ToolWeaponT = ToolWeaponTypes.None;

                            _eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                        }
                        else
                        {


                            if (_eMG.MainToolWeaponTC(cell_0).Is(ToolWeaponTypes.Axe))
                            {


                                if (_eMG.MainTWLevelTC(cell_0).Is(LevelTypes.First))
                                {
                                    if (_eMG.PlayerInfoE(whoseMove).LevelE(levTW).ToolWeapons(twT) > 0)
                                    {
                                        _eMG.ToolWeaponsC(whoseMove, levTW, twT)--;

                                        UnitSs.SetMainToolWeapon(cell_0, twT, levTW);

                                        _eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                        _eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                    }
                                    else
                                    {
                                        var needRes = new Dictionary<ResourceTypes, float>();
                                        var canBuy = true;

                                        for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                        {
                                            var difAmountRes = _eMG.PlayerInfoE(whoseMove).ResourcesC(res).Resources - EconomyValues.ForBuyToolWeapon(twT, levTW, res);
                                            needRes.Add(res, EconomyValues.ForBuyToolWeapon(twT, levTW, res));

                                            if (canBuy) canBuy = difAmountRes >= 0;
                                        }

                                        if (canBuy)
                                        {
                                            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                                _eMG.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= EconomyValues.ForBuyToolWeapon(twT, levTW, resT);

                                            _eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                            UnitSs.SetMainToolWeapon(cell_0, twT, levTW);

                                            _eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);

                                            if (_eMG.LessonTC.Is(LessonTypes.GiveStaff, LessonTypes.GiveBowCrossbow))
                                            {
                                                _eMG.LessonTC.SetNextLesson();
                                            }
                                        }
                                        else
                                        {
                                            _eMG.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
                                        }

                                    }
                                }
                                else
                                {
                                    _eMG.ToolWeaponsC(whoseMove, _eMG.MainTWLevelTC(cell_0).LevelT, _eMG.MainToolWeaponTC(cell_0).ToolWeaponT)++;
                                    UnitSs.SetMainToolWeapon(cell_0, ToolWeaponTypes.Axe, LevelTypes.First);

                                    _eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                                }
                            }

                            else
                            {
                                _eMG.ToolWeaponsC(whoseMove, _eMG.MainTWLevelTC(cell_0).LevelT, _eMG.MainToolWeaponTC(cell_0).ToolWeaponT)++;
                                UnitSs.SetMainToolWeapon(cell_0, ToolWeaponTypes.Axe, LevelTypes.First);

                                _eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                            }
                        }
                    }

                    else if (twT == ToolWeaponTypes.Axe)
                    {
                        if (_eMG.MainToolWeaponTC(cell_0).Is(ToolWeaponTypes.Axe))
                        {
                            if (_eMG.MainTWLevelTC(cell_0).Is(LevelTypes.First))
                            {
                                if (_eMG.PlayerInfoE(whoseMove).LevelE(levTW).ToolWeapons(twT) > 0)
                                {
                                    _eMG.ToolWeaponsC(whoseMove, levTW, twT)--;
                                    UnitSs.SetMainToolWeapon(cell_0, twT, levTW);

                                    _eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                    _eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                }
                                else
                                {
                                    var needRes = new Dictionary<ResourceTypes, float>();
                                    var canBuy = true;

                                    for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                    {
                                        var difAmountRes = _eMG.PlayerInfoE(whoseMove).ResourcesC(res).Resources - EconomyValues.ForBuyToolWeapon(twT, levTW, res);
                                        needRes.Add(res, EconomyValues.ForBuyToolWeapon(twT, levTW, res));

                                        if (canBuy) canBuy = difAmountRes >= 0;
                                    }

                                    if (canBuy)
                                    {
                                        for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                            _eMG.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= EconomyValues.ForBuyToolWeapon(twT, levTW, resT);

                                        _eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                        UnitSs.SetMainToolWeapon(cell_0, twT, levTW);

                                        _eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);

                                        if (_eMG.LessonT == LessonTypes.GiveIronAxe)
                                        {
                                            _eMG.LessonTC.SetNextLesson();
                                        }

                                    }
                                    else
                                    {
                                        _eMG.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
                                    }

                                }
                            }

                            else
                            {
                                _eMG.ToolWeaponsC(whoseMove, _eMG.MainTWLevelTC(cell_0).LevelT, _eMG.MainToolWeaponTC(cell_0).ToolWeaponT)++;
                                UnitSs.SetMainToolWeapon(cell_0, ToolWeaponTypes.Axe, LevelTypes.First);

                                _eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                            }
                        }

                        else
                        {
                            _eMG.ToolWeaponsC(whoseMove, _eMG.MainTWLevelTC(cell_0).LevelT, _eMG.MainToolWeaponTC(cell_0).ToolWeaponT)++;
                            UnitSs.SetMainToolWeapon(cell_0, ToolWeaponTypes.Axe, LevelTypes.First);

                            _eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                            _eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }
                    }

                    else
                    {
                        var ownUnit_0 = _eMG.UnitPlayerTC(cell_0).PlayerT;

                        if (_eMG.MainToolWeaponTC(cell_0).Is(ToolWeaponTypes.BowCrossbow, ToolWeaponTypes.Staff))
                        {
                            _eMG.ToolWeaponsC(_eMG.UnitPlayerTC(cell_0).PlayerT, _eMG.MainTWLevelTC(cell_0).LevelT, _eMG.MainToolWeaponTC(cell_0).ToolWeaponT)++;
                            UnitSs.SetMainToolWeapon(cell_0, ToolWeaponTypes.Axe, LevelTypes.First);

                            _eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                        }

                        else
                        {
                            if (_eMG.MainToolWeaponTC(cell_0).Is(ToolWeaponTypes.Axe))
                            {
                                if (_eMG.ExtraToolWeaponTC(cell_0).HaveToolWeapon)
                                {
                                    _eMG.PlayerInfoE(ownUnit_0).LevelE(_eMG.ExtraTWLevelTC(cell_0).LevelT).ToolWeapons(_eMG.ExtraToolWeaponTC(cell_0).ToolWeaponT)++;
                                    _eMG.ExtraToolWeaponTC(cell_0).ToolWeaponT = ToolWeaponTypes.None;

                                    _eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                    _eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                }

                                else if (_eMG.ToolWeaponsC(ownUnit_0, levTW, twT) > 0)
                                {
                                    _eMG.PlayerInfoE(ownUnit_0).LevelE(levTW).ToolWeapons(twT)--;



                                    UnitSs.SetExtraToolWeapon(cell_0, twT, levTW, _eMG.ExtraTWProtectionC(cell_0).Protection);

                                    _eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                    _eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                }

                                else
                                {
                                    var needRes = new Dictionary<ResourceTypes, float>();
                                    var canCreatBuild = true;

                                    for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                    {
                                        var difAmountRes = _eMG.PlayerInfoE(whoseMove).ResourcesC(res).Resources - EconomyValues.ForBuyToolWeapon(twT, levTW, res);
                                        needRes.Add(res, EconomyValues.ForBuyToolWeapon(twT, levTW, res));

                                        if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
                                    }

                                    if (canCreatBuild)
                                    {
                                        for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                            _eMG.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= EconomyValues.ForBuyToolWeapon(twT, levTW, resT);

                                        var protection = 0f;

                                        if (twT == ToolWeaponTypes.Shield)
                                        {
                                            protection = levTW == LevelTypes.First ? ToolWeaponValues.SHIELD_PROTECTION_LEVEL_FIRST
                                                : ToolWeaponValues.SHIELD_PROTECTION_LEVEL_SECOND;
                                        }

                                        UnitSs.SetExtraToolWeapon(cell_0, twT, levTW, protection);

                                        _eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                        _eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);


                                        if (_eMG.LessonTC.Is(LessonTypes.GiveTakePickPawn, LessonTypes.GiveShield, LessonTypes.GiveSword))
                                        {
                                            //if (_eMG.LessonT == LessonTypes.GiveSword)
                                            //{
                                            //    _eMG.YoungForestC(StartValues.CELL_IDX_FOR_SHIFT_PAWN_TO_FIRE_ADULT_FOREST).Resources = 0;

                                            //    _eMG.AdultForestC(StartValues.CELL_IDX_FOR_SHIFT_PAWN_TO_FIRE_ADULT_FOREST).Resources = EnvironmentValues.MAX_RESOURCES;
                                            //    _eMG.BuildingTC(StartValues.CELL_IDX_FOR_SHIFT_PAWN_TO_FIRE_ADULT_FOREST).BuildingT = BuildingTypes.None;
                                            //}

                                            _eMG.LessonTC.SetNextLesson();
                                        }
                                    }
                                    else
                                    {
                                        _eMG.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
                                    }
                                }
                            }
                        }
                    }
                }

                else
                {
                    _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

        }
    }
}