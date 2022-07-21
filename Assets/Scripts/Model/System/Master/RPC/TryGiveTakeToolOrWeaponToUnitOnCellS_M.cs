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
                    if (_extraTWC[cellIdxForDoing].HaveToolWeapon)
                    {
                        _e.AddToolWeaponsInInventor(_unitCs[cellIdxForDoing].PlayerT, _extraTWC[cellIdxForDoing].LevelT, _extraTWC[cellIdxForDoing].ToolWeaponT);
                        _extraTWC[cellIdxForDoing].ToolWeaponT = ToolsWeaponsWarriorTypes.None;
                    }
                    else
                    {


                        if (_mainTWC[cellIdxForDoing].ToolWeaponT == ToolsWeaponsWarriorTypes.Axe)
                        {


                            if (_mainTWC[cellIdxForDoing].LevelT == LevelTypes.First)
                            {
                                if (_e.ToolWeaponsInInventor(whoseMove, levTW, twT) > 0)
                                {
                                    _e.SubtractToolWeaponsInInventor(whoseMove, levTW, twT);

                                    _mainTWC[cellIdxForDoing].Set(twT, levTW);

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

                                        _mainTWC[cellIdxForDoing].Set(twT, levTW);

                                        RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);

                                        //if (_aboutGameC.LessonT.Is(LessonTypes.GiveStaff, LessonTypes.GiveBowCrossbow))
                                        //{
                                        //     SetNextLesson();
                                        //}
                                    }
                                    else
                                    {
                                        RpcSs.SimpleMistakeToGeneral(sender, needRes);
                                    }

                                }
                            }
                            else
                            {
                                _e.AddToolWeaponsInInventor(whoseMove, _mainTWC[cellIdxForDoing].LevelT, _mainTWC[cellIdxForDoing].ToolWeaponT);
                                _mainTWC[cellIdxForDoing].Set(ToolsWeaponsWarriorTypes.Axe, LevelTypes.First);

                            }
                        }

                        else
                        {
                            _e.AddToolWeaponsInInventor(whoseMove, _mainTWC[cellIdxForDoing].LevelT, _mainTWC[cellIdxForDoing].ToolWeaponT);
                            _mainTWC[cellIdxForDoing].Set(ToolsWeaponsWarriorTypes.Axe, LevelTypes.First);
                        }
                    }
                }

                else if (twT == ToolsWeaponsWarriorTypes.Axe)
                {
                    if (_mainTWC[cellIdxForDoing].ToolWeaponT == ToolsWeaponsWarriorTypes.Axe)
                    {
                        if (_mainTWC[cellIdxForDoing].LevelT == LevelTypes.First)
                        {
                            if (_e.ToolWeaponsInInventor(whoseMove, levTW, twT) > 0)
                            {
                                _e.SubtractToolWeaponsInInventor(whoseMove, levTW, twT);
                                _mainTWC[cellIdxForDoing].Set(twT, levTW);

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

                                    _mainTWC[cellIdxForDoing].Set(twT, levTW);

                                    RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);

                                    if (_aboutGameC.LessonT == LessonTypes.GiveIronAxe)
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
                            _e.AddToolWeaponsInInventor(whoseMove, _mainTWC[cellIdxForDoing].LevelT, _mainTWC[cellIdxForDoing].ToolWeaponT);
                            _mainTWC[cellIdxForDoing].Set(ToolsWeaponsWarriorTypes.Axe, LevelTypes.First);
                        }
                    }

                    else
                    {
                        _e.AddToolWeaponsInInventor(whoseMove, _mainTWC[cellIdxForDoing].LevelT, _mainTWC[cellIdxForDoing].ToolWeaponT);
                        _mainTWC[cellIdxForDoing].Set(ToolsWeaponsWarriorTypes.Axe, LevelTypes.First);

                        RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);
                    }
                }

                else
                {
                    var ownUnit_0 = _unitCs[cellIdxForDoing].PlayerT;

                    if (_mainTWC[cellIdxForDoing].ToolWeaponT.Is(ToolsWeaponsWarriorTypes.BowCrossbow, ToolsWeaponsWarriorTypes.Staff))
                    {
                        _e.AddToolWeaponsInInventor(_unitCs[cellIdxForDoing].PlayerT, _mainTWC[cellIdxForDoing].LevelT, _mainTWC[cellIdxForDoing].ToolWeaponT);
                        _mainTWC[cellIdxForDoing].Set(ToolsWeaponsWarriorTypes.Axe, LevelTypes.First);
                    }

                    else
                    {
                        if (_mainTWC[cellIdxForDoing].ToolWeaponT == ToolsWeaponsWarriorTypes.Axe)
                        {
                            if (_extraTWC[cellIdxForDoing].HaveToolWeapon)
                            {
                                _e.AddToolWeaponsInInventor(ownUnit_0, _extraTWC[cellIdxForDoing].LevelT, _extraTWC[cellIdxForDoing].ToolWeaponT);
                                _extraTWC[cellIdxForDoing].ToolWeaponT = ToolsWeaponsWarriorTypes.None;

                                RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);
                            }

                            else if (_e.ToolWeaponsInInventor(ownUnit_0, levTW, twT) > 0)
                            {
                                _e.SubtractToolWeaponsInInventor(ownUnit_0, levTW, twT, 1);


                                _extraTWC[cellIdxForDoing].Set(twT, levTW, ValuesChessy.MaxShieldProtection(levTW));

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

                                    _extraTWC[cellIdxForDoing].Set(twT, levTW, protection);


                                    RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.PickMelee);


                                    if (_aboutGameC.LessonT == LessonTypes.GiveTakePickPawn)
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