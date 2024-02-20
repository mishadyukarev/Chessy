using Chessy.Model.Entity;
using Chessy.Model.Values;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Model.System
{
    public sealed class OnPhotonSerializeViewS : SystemModelAbstract
    {
        readonly EntitiesModel _eCopy;

        readonly List<bool> _canSync = new();
        readonly List<object> _objsForSync = new();

        internal OnPhotonSerializeViewS(in SystemsModel sM, in EntitiesModel eM) : base(sM, eM)
        {
            _eCopy = new EntitiesModel(dataFromViewC, eM.CommonGameE.RpcC.PunRPCName, new List<object>() { eM.CommonGameE.RpcC.Action0, eM.CommonGameE.RpcC.Action1 }, aboutGameC.TestModeT);
        }

        internal void OnPhotonSerializeView0(in SyncTypes syncType, PhotonStream stream, PhotonMessageInfo info)
        {
            switch (syncType)
            {
                case SyncTypes.Main:
                    {
                        if (stream.IsWriting)
                        {
                            stream.SendNext(aboutGameC.IsStartedGame);
                            stream.SendNext(aboutGameC.WinnerPlayerT);
                            stream.SendNext(windC.DirectT);
                            stream.SendNext(windC.Speed);
                            stream.SendNext(sunC.SunSideT);


                            for (byte playerT_byte = 1; playerT_byte < (byte)PlayerTypes.End; playerT_byte++)
                            {
                                //var playerInfoE = PlayerInfoE(playerT);

                                var playerInfoC = playerInfoCs[playerT_byte];
                                stream.SendNext(playerInfoC.IsReadyForStartOnlineGame);
                                stream.SendNext(playerInfoC.WoodForBuyHouse);
                                stream.SendNext(playerInfoC.HaveKingInInventor);
                                stream.SendNext(playerInfoC.AmountBuiltHouses);

                                var pawnPeopleInfoC = pawnPeopleInfoCs[playerT_byte];
                                stream.SendNext(pawnPeopleInfoC.PeopleInCity);
                                stream.SendNext(pawnPeopleInfoC.AmountInGame);

                                var godInfoC = godInfoCs[playerT_byte];
                                stream.SendNext(godInfoC.HaveGodInInventor);
                                stream.SendNext(godInfoC.UnitType);
                                stream.SendNext(godInfoC.CooldownInSecondsForNextAppearance);


                                for (var buildingT = (BuildingTypes)0; buildingT < BuildingTypes.End; buildingT++)
                                {
                                    var buildingInfoC = buildingsInTownInfoCs[playerT_byte];

                                    stream.SendNext(buildingInfoC.HaveBuilding(buildingT));
                                }



                                for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
                                {
                                    for (var twT = (ToolsWeaponsWarriorTypes)1; twT < ToolsWeaponsWarriorTypes.End; twT++)
                                    {
                                        stream.SendNext(howManyToolWeaponsInInventoryCs[playerT_byte].ToolWeapons(twT, levelT));
                                    }
                                }

                                var resInInventorC = resourcesInInventoryCs[playerT_byte];

                                for (var resT = (ResourceTypes)1; resT < ResourceTypes.End; resT++)
                                {
                                    stream.SendNext(resInInventorC.ResourcesRef(resT));
                                }

                            }
                        }

                        else
                        {

                            aboutGameC.IsStartedGame = (bool)stream.ReceiveNext();
                            aboutGameC.WinnerPlayerT = (PlayerTypes)stream.ReceiveNext();
                            windC.DirectT = (DirectTypes)stream.ReceiveNext();
                            windC.Speed = (byte)stream.ReceiveNext();
                            sunC.SunSideT = (SunSideTypes)stream.ReceiveNext();


                            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                            {
                                var player_byte = (byte)playerT;

                                playerInfoCs[player_byte].IsReadyForStartOnlineGame = (bool)stream.ReceiveNext();
                                playerInfoCs[player_byte].WoodForBuyHouse = (float)stream.ReceiveNext();
                                playerInfoCs[player_byte].HaveKingInInventor = (bool)stream.ReceiveNext();
                                playerInfoCs[player_byte].AmountBuiltHouses = (int)stream.ReceiveNext();

                                PawnPeopleInfoC(playerT).PeopleInCity = (int)stream.ReceiveNext();
                                PawnPeopleInfoC(playerT).AmountInGame = (int)stream.ReceiveNext();

                                godInfoCs[player_byte].HaveGodInInventor = (bool)stream.ReceiveNext();
                                godInfoCs[player_byte].UnitType = (UnitTypes)stream.ReceiveNext();
                                godInfoCs[player_byte].CooldownInSecondsForNextAppearance = (int)stream.ReceiveNext();

                                for (var buildingT = (BuildingTypes)0; buildingT < BuildingTypes.End; buildingT++)
                                {
                                    buildingsInTownInfoCs[(byte)playerT].HaveBuilding(buildingT) = (bool)stream.ReceiveNext();
                                }

                                for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
                                {
                                    for (var twT = (ToolsWeaponsWarriorTypes)1; twT < ToolsWeaponsWarriorTypes.End; twT++)
                                    {
                                        ToolWeaponsInInventoryC(playerT).Set(twT, levelT, (int)stream.ReceiveNext());
                                    }
                                }

                                for (var resT = (ResourceTypes)1; resT < ResourceTypes.End; resT++)
                                {
                                    ResourcesInInventoryC(playerT).Set(resT, (double)stream.ReceiveNext());
                                }

                            }

                            Update();

                            //Debug.Log("Synchronization");
                        }
                    }
                    break;

                case SyncTypes.UnitShift:
                    {
                        if (stream.IsWriting)
                        {
                            for (byte curCellIdx_0 = 0; curCellIdx_0 < IndexCellsValues.CELLS; curCellIdx_0++)
                            {
                                if (cellCs[curCellIdx_0].IsBorder) continue;

                                stream.SendNext(UnitShiftC(curCellIdx_0).Distance);
                            }
                        }
                        else
                        {
                            for (byte curCellIdx_0 = 0; curCellIdx_0 < IndexCellsValues.CELLS; curCellIdx_0++)
                            {
                                if (cellCs[curCellIdx_0].IsBorder) continue;

                                UnitShiftC(curCellIdx_0).Distance = (float)stream.ReceiveNext();
                            }
                        }
                    }
                    break;

                case SyncTypes.CloudShift:
                    {
                        if (stream.IsWriting)
                        {
                            for (byte curCellIdx_0 = 0; curCellIdx_0 < IndexCellsValues.CELLS; curCellIdx_0++)
                            {
                                if (cellCs[curCellIdx_0].IsBorder) continue;

                                stream.SendNext(shiftCloudCs[curCellIdx_0].Distance);

                            }
                        }
                        else
                        {
                            for (byte curCellIdx_0 = 0; curCellIdx_0 < IndexCellsValues.CELLS; curCellIdx_0++)
                            {
                                if (cellCs[curCellIdx_0].IsBorder) continue;

                                shiftCloudCs[curCellIdx_0].Distance = (float)stream.ReceiveNext();
                            }
                        }
                    }
                    break;

                case SyncTypes.Cells:
                    {
                        for (byte currentCellIdx_0 = 0; currentCellIdx_0 < IndexCellsValues.CELLS; currentCellIdx_0++)
                        {
                            if (cellCs[currentCellIdx_0].IsBorder) continue;

                            var unitC = unitCs[currentCellIdx_0];
                            var mainTWC = base.mainTWC[currentCellIdx_0];
                            var extraTWC = UnitExtraTWC(currentCellIdx_0);
                            var unitViewDataC = unitWhereViewDataCs[currentCellIdx_0];
                            var unitEffectsC = effectsUnitCs[currentCellIdx_0];
                            var unitCooldownC = _cooldownAbilityCs[currentCellIdx_0];
                            var unitAttackC = UnitAttackC(currentCellIdx_0);

                            var cloudViewDataC = cloudWhereViewDataCs[currentCellIdx_0];

                            if (stream.IsWriting)
                            {
                                stream.SendNext(unitC.UnitT);
                                stream.SendNext(unitC.LevelT);
                                stream.SendNext(unitC.PlayerT);
                                stream.SendNext(unitC.ConditionT);
                                stream.SendNext(unitC.IsArcherDirectedToRight);
                                stream.SendNext(unitC.HowManySecondUnitWasHereInThisCondition);

                                stream.SendNext(mainTWC.ToolWeaponT);
                                stream.SendNext(mainTWC.LevelT);
                     
                                stream.SendNext(extraTWC.ToolWeaponT);
                                stream.SendNext(extraTWC.LevelT);
                                stream.SendNext(extraTWC.ProtectionShield);
              
                                stream.SendNext(unitViewDataC.DataIdxCell);
                                stream.SendNext(unitViewDataC.ViewIdxCell);
  
                                stream.SendNext(unitEffectsC.ProtectionRainyMagicShield);
                                stream.SendNext(unitEffectsC.HaveFrozenArrawArcher);
                                stream.SendNext(unitEffectsC.StunHowManyUpdatesNeedStay);

                                stream.SendNext(unitAttackC.DamageSimpleAttack);
                                stream.SendNext(unitAttackC.DamageOnCell);
                                stream.SendNext(unitAttackC.CooldownForAttackAnyUnitInSeconds);

                                for (var abilityT = (AbilityTypes)1; abilityT < AbilityTypes.End; abilityT++)
                                {
                                    stream.SendNext(unitCooldownC.Cooldown(abilityT));
                                }

                                stream.SendNext(unitHpCs[currentCellIdx_0].Health);
                                stream.SendNext(unitWaterCs[currentCellIdx_0].Water);
                                stream.SendNext(UnitShiftC(currentCellIdx_0).WhereNeedShiftIdxCell);


                                stream.SendNext(cloudCs[currentCellIdx_0].IsCenter);
                                stream.SendNext(shiftCloudCs[currentCellIdx_0].WhereNeedShiftIdxCell);
                                stream.SendNext(cloudViewDataC.DataIdxCell);
                                stream.SendNext(cloudViewDataC.ViewIdxCell);
                            }

                            else
                            {
                                unitC.UnitT = (UnitTypes)stream.ReceiveNext();
                                unitC.LevelT = (LevelTypes)stream.ReceiveNext();
                                unitC.PlayerT = (PlayerTypes)stream.ReceiveNext();
                                unitC.ConditionT = (ConditionUnitTypes)stream.ReceiveNext();
                                unitC.IsArcherDirectedToRight = (bool)stream.ReceiveNext();
                                unitC.HowManySecondUnitWasHereInThisCondition = (int)stream.ReceiveNext();

                                mainTWC.ToolWeaponT = (ToolsWeaponsWarriorTypes)stream.ReceiveNext();
                                mainTWC.LevelT = (LevelTypes)stream.ReceiveNext();

                                extraTWC.ToolWeaponT = (ToolsWeaponsWarriorTypes)stream.ReceiveNext();
                                extraTWC.LevelT = (LevelTypes)stream.ReceiveNext();
                                extraTWC.ProtectionShield = (float)stream.ReceiveNext();

                                unitViewDataC.DataIdxCell = (byte)stream.ReceiveNext();
                                unitViewDataC.ViewIdxCell = (byte)stream.ReceiveNext();

                                unitEffectsC.ProtectionRainyMagicShield = (float)stream.ReceiveNext();
                                unitEffectsC.HaveFrozenArrawArcher = (bool)stream.ReceiveNext();
                                unitEffectsC.StunHowManyUpdatesNeedStay = (float)stream.ReceiveNext();

                                unitAttackC.DamageSimpleAttack = (double)stream.ReceiveNext();
                                unitAttackC.DamageOnCell = (double)stream.ReceiveNext();
                                unitAttackC.CooldownForAttackAnyUnitInSeconds = (int)stream.ReceiveNext();

                                for (var abilityT = (AbilityTypes)1; abilityT < AbilityTypes.End; abilityT++)
                                {
                                    unitCooldownC.Set(abilityT, (int)stream.ReceiveNext());
                                }

                                unitHpCs[currentCellIdx_0].Health = (double)stream.ReceiveNext();
                                unitWaterCs[currentCellIdx_0].Water = (double)stream.ReceiveNext();
                                UnitShiftC(currentCellIdx_0).WhereNeedShiftIdxCell = (byte)stream.ReceiveNext();


                                cloudCs[currentCellIdx_0].IsCenter = (bool)stream.ReceiveNext();
                                shiftCloudCs[currentCellIdx_0].WhereNeedShiftIdxCell = (byte)stream.ReceiveNext();
                                cloudViewDataC.DataIdxCell = (byte)stream.ReceiveNext();
                                cloudViewDataC.ViewIdxCell = (byte)stream.ReceiveNext();
                            }
                        }

                        Update();
                    }
                    break;

                case SyncTypes.Else:
                    {
                        if (stream.IsWriting)
                        {
                            for (byte curCellIdx_0 = 0; curCellIdx_0 < IndexCellsValues.CELLS; curCellIdx_0++)
                            {
                                if (cellCs[curCellIdx_0].IsBorder) continue;

                                for (var environmentT = (EnvironmentTypes)1; environmentT < EnvironmentTypes.End; environmentT++)
                                {
                                    stream.SendNext(environmentCs[curCellIdx_0].Resources(environmentT));
                                }

                                stream.SendNext(fireCs[curCellIdx_0].HaveFire);

                                var buildingC = buildingCs[curCellIdx_0];
                                stream.SendNext(buildingC.BuildingT);
                                stream.SendNext(buildingC.LevelT);
                                stream.SendNext(buildingC.PlayerT);

                                var trailHealthC = hpTrailCs[curCellIdx_0];
                                for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
                                {
                                    stream.SendNext(trailHealthC.Health(directT));
                                }
                            }
                        }

                        else
                        {
                            for (byte curCellIdx_0 = 0; curCellIdx_0 < IndexCellsValues.CELLS; curCellIdx_0++)
                            {
                                if (cellCs[curCellIdx_0].IsBorder) continue;

                                for (var environmentT = (EnvironmentTypes)1; environmentT < EnvironmentTypes.End; environmentT++)
                                {
                                    environmentCs[curCellIdx_0].Set(environmentT, (double)stream.ReceiveNext());
                                }

                                fireCs[curCellIdx_0].HaveFire = (bool)stream.ReceiveNext();

                                var buildingC_0 = buildingCs[curCellIdx_0];

                                buildingC_0.BuildingT = (BuildingTypes)stream.ReceiveNext();
                                buildingC_0.LevelT = (LevelTypes)stream.ReceiveNext();
                                buildingC_0.PlayerT = (PlayerTypes)stream.ReceiveNext();

                                for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
                                {
                                    hpTrailCs[curCellIdx_0].Set(directT, (float)stream.ReceiveNext());
                                }
                            }
                        }

                        Update();
                    }
                    break;

                default: break;
            }
        }


        //void TrySync(in PhotonStream stream)
        //{
        //    var needSync = false;

        //    for (var i = 0; i < _canSync.Count; i++)
        //    {
        //        if (_canSync[i])
        //        {
        //            needSync = true;
        //            break;
        //        }
        //    }

        //    if (needSync)
        //    {
        //        stream.SendNext(needSync);

        //        for (var i = 0; i < _canSync.Count; i++)
        //        {
        //            if (_canSync[i])
        //            {
        //                stream.SendNext(true);
        //                stream.SendNext(_objsForSync[i]);


        //                try
        //                {
                           
        //                }
        //                catch (Exception)
        //                {

        //                    throw;
        //                }


        //            }
        //            else
        //            {
        //                stream.SendNext(false);
        //            }
        //        }
        //    }

        //    _canSync.Clear();
        //    _objsForSync.Clear();
        //}

        //bool TryAddDataForSync<T>(T one, ref T twoCopy) where T : struct
        //{
        //    var obj0 = /*(object)*/one.ToString();
        //    var obj1 = /*(object)*/twoCopy.ToString();

        //    var canSync = obj0 != obj1;
        //    _canSync.Add(canSync);
        //    if (canSync)
        //    {
        //        twoCopy = one; 
        //    }
        //    _objsForSync.Add(one);
        //    return canSync;
        //}
        //bool TryAddDataForSync(float one, ref float twoCopy) => Can(one != twoCopy, one, ref twoCopy);
        //bool TryAddDataForSync(int one, ref int twoCopy) => Can(one != twoCopy, one, ref twoCopy);
        //bool TryAddDataForSync(double one, ref double twoCopy) => Can(one != twoCopy, one, ref twoCopy);
        //bool TryAddDataForSync(bool one, ref bool twoCopy) => Can(one != twoCopy, one, ref twoCopy);
        //bool TryAddDataForSync(byte one, ref byte twoCopy) => Can(one != twoCopy, one, ref twoCopy);
        //bool Can<T>(in bool b, T one, ref T twoCopy)
        //{
        //    _canSync.Add(b);
        //    if (b)
        //    {
        //        twoCopy = one;
        //        _objsForSync.Add(one);
        //    }
        //    return b;
        //}

        //void TryAddForSync<T>(in bool needSync, T one, ref T twoCopy) where T : struct
        //{
        //    _canSync.Add(needSync);
        //    if (needSync)
        //    {
        //        twoCopy = one;
        //        _objsForSync.Add(one);
        //    }
        //}

        void Update()
        {
            s.GetDataCellsS.GetDataCells();
            updateAllViewC.NeedUpdateView = true;
        }
    }
}