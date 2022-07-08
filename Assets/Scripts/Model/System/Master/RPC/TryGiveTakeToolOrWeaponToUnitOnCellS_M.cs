using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void TryGiveTakeToolOrWeaponToUnitOnCellM(in ToolsWeaponsWarriorTypes twT, in LevelTypes levTW, in byte cellIdxForDoing, in Player sender)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? PlayerTypes.First : sender.GetPlayer();

            if (_e.UnitT(cellIdxForDoing).Is(UnitTypes.Pawn))
            {
                //if (_e.EnergyUnitC(cellIdxForDoing).Energy >= StepValues.FOR_GIVE_TAKE_TOOLWEAPON)
                //{
                    if (twT == ToolsWeaponsWarriorTypes.BowCrossbow || twT == ToolsWeaponsWarriorTypes.Staff)
                    {
                        if (_e.ExtraToolWeaponT(cellIdxForDoing).HaveToolWeapon())
                        {
                            _e.AddToolWeaponsInInventor(_e.UnitPlayerT(cellIdxForDoing), _e.ExtraTWLevelT(cellIdxForDoing), _e.ExtraToolWeaponT(cellIdxForDoing));
                            _e.SetExtraToolWeaponT(cellIdxForDoing, ToolsWeaponsWarriorTypes.None);
                        }
                        else
                        {


                            if (_e.MainToolWeaponT(cellIdxForDoing).Is(ToolsWeaponsWarriorTypes.Axe))
                            {


                                if (_e.MainTWLevelT(cellIdxForDoing).Is(LevelTypes.First))
                                {
                                    if (_e.ToolWeaponsInInventor(whoseMove, levTW, twT) > 0)
                                    {
                                        _e.SubtractToolWeaponsInInventor(whoseMove, levTW, twT);

                                        _e.MainToolWeaponC(cellIdxForDoing).Set(twT, levTW);

                                        RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);
                                    }
                                    else
                                    {
                                        var needRes = new Dictionary<ResourceTypes, float>();
                                        var canBuy = true;

                                        for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                        {
                                            var difAmountRes = _e.ResourcesInInventory(whoseMove, res) - CostsForBuyToolsWeaponsForWarrior.ForBuyToolWeapon(twT, levTW, res);
                                            needRes.Add(res, CostsForBuyToolsWeaponsForWarrior.ForBuyToolWeapon(twT, levTW, res));

                                            if (canBuy) canBuy = difAmountRes >= 0;
                                        }

                                        if (canBuy)
                                        {
                                            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                                _e.ResourcesInInventoryC(whoseMove).Subtract(resT, CostsForBuyToolsWeaponsForWarrior.ForBuyToolWeapon(twT, levTW, resT));

                                            _e.MainToolWeaponC(cellIdxForDoing).Set(twT, levTW);

                                            RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);

                                            if (_e.LessonT.Is(LessonTypes.GiveStaff, LessonTypes.GiveBowCrossbow))
                                            {
                                                 SetNextLesson();
                                            }
                                        }
                                        else
                                        {
                                           RpcSs.SimpleMistakeToGeneral(sender, needRes);
                                        }

                                    }
                                }
                                else
                                {
                                    _e.AddToolWeaponsInInventor(whoseMove, _e.MainTWLevelT(cellIdxForDoing), _e.MainToolWeaponT(cellIdxForDoing));
                                    _e.MainToolWeaponC(cellIdxForDoing).Set(ToolsWeaponsWarriorTypes.Axe, LevelTypes.First);

                                }
                            }

                            else
                            {
                                _e.AddToolWeaponsInInventor(whoseMove, _e.MainTWLevelT(cellIdxForDoing), _e.MainToolWeaponT(cellIdxForDoing));
                                _e.MainToolWeaponC(cellIdxForDoing).Set(ToolsWeaponsWarriorTypes.Axe, LevelTypes.First);
                            }
                        }
                    }

                    else if (twT == ToolsWeaponsWarriorTypes.Axe)
                    {
                        if (_e.MainToolWeaponT(cellIdxForDoing).Is(ToolsWeaponsWarriorTypes.Axe))
                        {
                            if (_e.MainTWLevelT(cellIdxForDoing).Is(LevelTypes.First))
                            {
                                if (_e.ToolWeaponsInInventor(whoseMove, levTW, twT) > 0)
                                {
                                    _e.SubtractToolWeaponsInInventor(whoseMove, levTW, twT);
                                    _e.MainToolWeaponC(cellIdxForDoing).Set(twT, levTW);

                                    RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);
                                }
                                else
                                {
                                    var needRes = new Dictionary<ResourceTypes, float>();
                                    var canBuy = true;

                                    for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                    {
                                        var difAmountRes = _e.ResourcesInInventory(whoseMove, res) - CostsForBuyToolsWeaponsForWarrior.ForBuyToolWeapon(twT, levTW, res);
                                        needRes.Add(res, CostsForBuyToolsWeaponsForWarrior.ForBuyToolWeapon(twT, levTW, res));

                                        if (canBuy) canBuy = difAmountRes >= 0;
                                    }

                                    if (canBuy)
                                    {
                                        for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                            _e.ResourcesInInventoryC(whoseMove).Subtract(resT, CostsForBuyToolsWeaponsForWarrior.ForBuyToolWeapon(twT, levTW, resT));

                                        _e.MainToolWeaponC(cellIdxForDoing).Set(twT, levTW);

                                        RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);

                                        if (_e.LessonT == LessonTypes.GiveIronAxe)
                                        {
                                             SetNextLesson();
                                        }

                                    }
                                    else
                                    {
                                       RpcSs.SimpleMistakeToGeneral(sender, needRes);
                                    }

                                }
                            }

                            else
                            {
                                _e.AddToolWeaponsInInventor(whoseMove, _e.MainTWLevelT(cellIdxForDoing), _e.MainToolWeaponT(cellIdxForDoing));
                                _e.MainToolWeaponC(cellIdxForDoing).Set(ToolsWeaponsWarriorTypes.Axe, LevelTypes.First);
                            }
                        }

                        else
                        {
                            _e.AddToolWeaponsInInventor(whoseMove, _e.MainTWLevelT(cellIdxForDoing), _e.MainToolWeaponT(cellIdxForDoing));
                            _e.MainToolWeaponC(cellIdxForDoing).Set(ToolsWeaponsWarriorTypes.Axe, LevelTypes.First);

                            RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);
                        }
                    }

                    else
                    {
                        var ownUnit_0 = _e.UnitPlayerT(cellIdxForDoing);

                        if (_e.MainToolWeaponT(cellIdxForDoing).Is(ToolsWeaponsWarriorTypes.BowCrossbow, ToolsWeaponsWarriorTypes.Staff))
                        {
                            _e.AddToolWeaponsInInventor(_e.UnitPlayerT(cellIdxForDoing), _e.MainTWLevelT(cellIdxForDoing), _e.MainToolWeaponT(cellIdxForDoing));
                            _e.MainToolWeaponC(cellIdxForDoing).Set(ToolsWeaponsWarriorTypes.Axe, LevelTypes.First);
                        }

                        else
                        {
                            if (_e.MainToolWeaponT(cellIdxForDoing).Is(ToolsWeaponsWarriorTypes.Axe))
                            {
                                if (_e.ExtraToolWeaponT(cellIdxForDoing).HaveToolWeapon())
                                {
                                    _e.AddToolWeaponsInInventor(ownUnit_0, _e.ExtraTWLevelT(cellIdxForDoing), _e.ExtraToolWeaponT(cellIdxForDoing));
                                    _e.SetExtraToolWeaponT(cellIdxForDoing, ToolsWeaponsWarriorTypes.None);

                                    RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);
                                }

                                else if (_e.ToolWeaponsInInventor(ownUnit_0, levTW, twT) > 0)
                                {
                                    _e.SubtractToolWeaponsInInventor(ownUnit_0, levTW, twT, 1);


                                    _e.UnitExtraTWC(cellIdxForDoing).Set(twT, levTW, ValuesChessy.MaxShieldProtection(levTW));

                                    RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);
                                }

                                else
                                {
                                    var needRes = new Dictionary<ResourceTypes, float>();
                                    var canCreatBuild = true;

                                    for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                    {
                                        var difAmountRes = _e.ResourcesInInventory(whoseMove, res) - CostsForBuyToolsWeaponsForWarrior.ForBuyToolWeapon(twT, levTW, res);
                                        needRes.Add(res, CostsForBuyToolsWeaponsForWarrior.ForBuyToolWeapon(twT, levTW, res));

                                        if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
                                    }

                                    if (canCreatBuild)
                                    {
                                        for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                            _e.ResourcesInInventoryC(whoseMove).Subtract(resT, CostsForBuyToolsWeaponsForWarrior.ForBuyToolWeapon(twT, levTW, resT));

                                        var protection = 0f;

                                        if (twT == ToolsWeaponsWarriorTypes.Shield)
                                        {
                                            protection = levTW == LevelTypes.First ? ValuesChessy.SHIELD_MAX_PROTECTION_LEVEL_FIRST
                                                : ValuesChessy.SHIELD_MAX_PROTECTION_LEVEL_SECOND;
                                        }

                                        _e.UnitExtraTWC(cellIdxForDoing).Set(twT, levTW, protection);

                                        //_e.EnergyUnitC(cellIdxForDoing).Energy -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                        RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);


                                        if (_e.LessonT.Is(LessonTypes.GiveTakePickPawn, LessonTypes.GiveShield, LessonTypes.GiveSword))
                                        {
                                            //if (_eMG.LessonT == LessonTypes.GiveSword)
                                            //{
                                            //    _eMG.YoungForestC(StartValues.CELL_IDX_FOR_SHIFT_PAWN_TO_FIRE_ADULT_FOREST).Resources = 0;

                                            //    _eMG.AdultForestC(StartValues.CELL_IDX_FOR_SHIFT_PAWN_TO_FIRE_ADULT_FOREST).Resources = ValuesChessy.MAX_RESOURCES;
                                            //    _eMG.BuildingTC(StartValues.CELL_IDX_FOR_SHIFT_PAWN_TO_FIRE_ADULT_FOREST).BuildingT = BuildingTypes.None;
                                            //}

                                             SetNextLesson();
                                        }
                                    }
                                    else
                                    {
                                       RpcSs.SimpleMistakeToGeneral(sender, needRes);
                                    }
                                }
                            }
                        }
                    }
                }

                else
                {
                    RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

        //}
    }
}