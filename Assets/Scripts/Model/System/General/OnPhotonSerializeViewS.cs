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
            _eCopy = new EntitiesModel(_dataFromViewC, eM.CommonGameE.RpcC.PunRPCName, new List<object>() { eM.CommonGameE.RpcC.Action0, eM.CommonGameE.RpcC.Action1 }, AboutGameC.TestModeT);
        }

        internal void OnPhotonSerializeView0(in SyncTypes syncType, PhotonStream stream, PhotonMessageInfo info)
        {
            switch (syncType)
            {
                case SyncTypes.Main:
                    {
                        if (stream.IsWriting)
                        {
                            stream.SendNext(AboutGameC.IsStartedGame);
                            stream.SendNext(AboutGameC.WinnerPlayerT);
                            stream.SendNext(WindC.DirectT);
                            stream.SendNext(WindC.Speed);
                            stream.SendNext(SunC.SunSideT);


                            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                            {
                                var playerInfoE = PlayerInfoE(playerT);

                                var playerInfoC = playerInfoE.PlayerInfoC;
                                stream.SendNext(playerInfoC.IsReadyForStartOnlineGame);
                                stream.SendNext(playerInfoC.WoodForBuyHouse);
                                stream.SendNext(playerInfoC.HaveKingInInventor);
                                stream.SendNext(playerInfoC.AmountBuiltHouses);

                                var pawnPeopleInfoC = playerInfoE.PawnInfoC;
                                stream.SendNext(pawnPeopleInfoC.PeopleInCity);
                                stream.SendNext(pawnPeopleInfoC.AmountInGame);

                                var godInfoC = playerInfoE.GodInfoC;
                                stream.SendNext(godInfoC.HaveGodInInventor);
                                stream.SendNext(godInfoC.UnitType);
                                stream.SendNext(godInfoC.CooldownInSecondsForNextAppearance);


                                for (var buildingT = (BuildingTypes)0; buildingT < BuildingTypes.End; buildingT++)
                                {
                                    var buildingInfoC = playerInfoE.BuildingsInTownInfoC;

                                    stream.SendNext(buildingInfoC.HaveBuilding(buildingT));
                                }



                                for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
                                {
                                    for (var twT = (ToolsWeaponsWarriorTypes)1; twT < ToolsWeaponsWarriorTypes.End; twT++)
                                    {
                                        stream.SendNext(ToolWeaponsInInventoryC(playerT).ToolWeapons(twT, levelT));
                                    }
                                }

                                var resInInventorC = playerInfoE.ResourcesInInventoryC;

                                for (var resT = (ResourceTypes)1; resT < ResourceTypes.End; resT++)
                                {
                                    stream.SendNext(resInInventorC.ResourcesRef(resT));
                                }

                            }
                        }

                        else
                        {

                            AboutGameC.IsStartedGame = (bool)stream.ReceiveNext();
                            AboutGameC.WinnerPlayerT = (PlayerTypes)stream.ReceiveNext();
                            WindC.DirectT = (DirectTypes)stream.ReceiveNext();
                            WindC.Speed = (byte)stream.ReceiveNext();
                            SunC.SunSideT = (SunSideTypes)stream.ReceiveNext();


                            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                            {

                                PlayerInfoC(playerT).IsReadyForStartOnlineGame = (bool)stream.ReceiveNext();
                                PlayerInfoC(playerT).WoodForBuyHouse = (float)stream.ReceiveNext();
                                PlayerInfoC(playerT).HaveKingInInventor = (bool)stream.ReceiveNext();
                                PlayerInfoC(playerT).AmountBuiltHouses = (int)stream.ReceiveNext();

                                PawnPeopleInfoC(playerT).PeopleInCity = (int)stream.ReceiveNext();
                                PawnPeopleInfoC(playerT).AmountInGame = (int)stream.ReceiveNext();

                                GodInfoC(playerT).HaveGodInInventor = (bool)stream.ReceiveNext();
                                GodInfoC(playerT).UnitType = (UnitTypes)stream.ReceiveNext();
                                GodInfoC(playerT).CooldownInSecondsForNextAppearance = (int)stream.ReceiveNext();

                                for (var buildingT = (BuildingTypes)0; buildingT < BuildingTypes.End; buildingT++)
                                {
                                    BuildingsInTownInfoC(playerT).HaveBuilding(buildingT) = (bool)stream.ReceiveNext();
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
                                if (CellC(curCellIdx_0).IsBorder) continue;

                                stream.SendNext(UnitShiftC(curCellIdx_0).Distance);
                            }
                        }
                        else
                        {
                            for (byte curCellIdx_0 = 0; curCellIdx_0 < IndexCellsValues.CELLS; curCellIdx_0++)
                            {
                                if (CellC(curCellIdx_0).IsBorder) continue;

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
                                if (CellC(curCellIdx_0).IsBorder) continue;

                                stream.SendNext(CloudShiftC(curCellIdx_0).Distance);

                            }
                        }
                        else
                        {
                            for (byte curCellIdx_0 = 0; curCellIdx_0 < IndexCellsValues.CELLS; curCellIdx_0++)
                            {
                                if (CellC(curCellIdx_0).IsBorder) continue;
                                
                                CloudShiftC(curCellIdx_0).Distance = (float)stream.ReceiveNext();
                            }
                        }
                    }
                    break;

                case SyncTypes.Cells:
                    {
                        for (byte currentCellIdx_0 = 0; currentCellIdx_0 < IndexCellsValues.CELLS; currentCellIdx_0++)
                        {
                            if (CellC(currentCellIdx_0).IsBorder) continue;

                            var unitC = UnitC(currentCellIdx_0);
                            var mainTWC = UnitMainTWC(currentCellIdx_0);
                            var extraTWC = UnitExtraTWC(currentCellIdx_0);
                            var unitViewDataC = UnitViewDataC(currentCellIdx_0);
                            var unitEffectsC = UnitEffectC(currentCellIdx_0);
                            var unitCooldownC = UnitCooldownC(currentCellIdx_0);
                            var unitAttackC = UnitAttackC(currentCellIdx_0);

                            var cloudViewDataC = CloudViewDataC(currentCellIdx_0);

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

                                stream.SendNext(UnitHpC(currentCellIdx_0).Health);
                                stream.SendNext(UnitWaterC(currentCellIdx_0).Water);
                                stream.SendNext(UnitShiftC(currentCellIdx_0).WhereNeedShiftIdxCell);


                                stream.SendNext(CloudC(currentCellIdx_0).IsCenter);
                                stream.SendNext(CloudShiftC(currentCellIdx_0).WhereNeedShiftIdxCell);
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

                                UnitHpC(currentCellIdx_0).Health = (double)stream.ReceiveNext();
                                UnitWaterC(currentCellIdx_0).Water = (double)stream.ReceiveNext();
                                UnitShiftC(currentCellIdx_0).WhereNeedShiftIdxCell = (byte)stream.ReceiveNext();


                                CloudC(currentCellIdx_0).IsCenter = (bool)stream.ReceiveNext();
                                CloudShiftC(currentCellIdx_0).WhereNeedShiftIdxCell = (byte)stream.ReceiveNext();
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
                                if (CellC(curCellIdx_0).IsBorder) continue;

                                for (var environmentT = (EnvironmentTypes)1; environmentT < EnvironmentTypes.End; environmentT++)
                                {
                                    stream.SendNext(EnvironmentC(curCellIdx_0).Resources(environmentT));
                                }

                                stream.SendNext(FireC(curCellIdx_0).HaveFire);

                                var buildingC = BuildingC(curCellIdx_0);
                                stream.SendNext(buildingC.BuildingT);
                                stream.SendNext(buildingC.LevelT);
                                stream.SendNext(buildingC.PlayerT);

                                var trailHealthC = TrailHealthC(curCellIdx_0);
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
                                if (CellC(curCellIdx_0).IsBorder) continue;

                                for (var environmentT = (EnvironmentTypes)1; environmentT < EnvironmentTypes.End; environmentT++)
                                {
                                    EnvironmentC(curCellIdx_0).Set(environmentT, (double)stream.ReceiveNext());
                                }

                                FireC(curCellIdx_0).HaveFire = (bool)stream.ReceiveNext();

                                BuildingC(curCellIdx_0).BuildingT = (BuildingTypes)stream.ReceiveNext();
                                BuildingC(curCellIdx_0).LevelT = (LevelTypes)stream.ReceiveNext();
                                BuildingC(curCellIdx_0).PlayerT = (PlayerTypes)stream.ReceiveNext();

                                for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
                                {
                                    TrailHealthC(curCellIdx_0).Set(directT, (float)stream.ReceiveNext());
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
            _s.GetDataCellsS.GetDataCells();
            _updateAllViewC.NeedUpdateView = true;
        }
    }
}