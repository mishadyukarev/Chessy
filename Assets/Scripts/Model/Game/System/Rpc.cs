using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Interface;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class Rpc : MonoBehaviour
    {
        EntitiesModelGame _eMG;
        SystemsModelGame _s;
        EntitiesModelCommon _eMCommon;

        int _idx_cur;


        public static List<string> NamesMethods_S
        {
            get
            {
                var list = new List<string>();
                list.Add(nameof(MasterRPC));
                list.Add(nameof(GeneralRpc));
                list.Add(nameof(OtherRpc));
                return list;
            }
        }

        public Rpc GiveData(in SystemsModelGame sMGame, in EntitiesModelGame eMGame, in EntitiesModelCommon eMCommon)
        {
            _s = sMGame;
            _eMG = eMGame;
            _eMCommon = eMCommon;

            return this;
        }


        [PunRPC]
        void MasterRPC(object[] objects, PhotonMessageInfo infoFrom)
        {
            _idx_cur = 0;

            var sender = infoFrom.Sender;

            var obj = objects[_idx_cur++];

            if (obj is string nameMethod)
            {
                if (nameMethod == nameof(_s.TryExecuteReadyForOnlineM))
                {
                    _s.TryExecuteReadyForOnlineM(sender);
                }

                else if (nameMethod == nameof(_s.TryDoneM))
                {
                    _s.TryDoneM(sender);
                }

                else if (nameMethod == nameof(_s.TryShiftUnitM))
                {
                    _s.TryShiftUnitM((byte)objects[_idx_cur++], (byte)objects[_idx_cur++], sender);
                }

                else if (nameMethod == nameof(_s.TryAttackUnitOnCellM))
                {
                    _s.TryAttackUnitOnCellM((byte)objects[_idx_cur++], (byte)objects[_idx_cur++], sender);
                }

                else if (nameMethod == nameof(_s.TrySetConditionUnitOnCellM))
                {
                    _s.TrySetConditionUnitOnCellM((ConditionUnitTypes)objects[_idx_cur++], (byte)objects[_idx_cur++], sender);
                }

                else if (nameMethod == nameof(_s.TrySetUnitOnCellM))
                {
                    var cell = (byte)objects[_idx_cur++];
                    _s.TrySetUnitOnCellM((UnitTypes)objects[_idx_cur++], sender, cell);
                }

                else if (nameMethod == nameof(_s.GetHeroInCenterM))
                {
                    _s.GetHeroInCenterM((UnitTypes)objects[_idx_cur++], sender);
                }

                else if (nameMethod == nameof(_s.TryMeltInMelterBuildingM))
                {
                    _s.TryMeltInMelterBuildingM(sender);
                }

                else if (nameMethod == nameof(_s.TryGiveTakeToolOrWeaponToUnitOnCellM))
                {
                    var idx = (byte)objects[_idx_cur++];
                    _s.TryGiveTakeToolOrWeaponToUnitOnCellM((ToolWeaponTypes)objects[_idx_cur++], (LevelTypes)objects[_idx_cur++], idx, sender);
                }

                else if (nameMethod == nameof(_s.ForUISystems.TryBuyBuildingM))
                {
                    _s.ForUISystems.TryBuyBuildingM((BuildingTypes)objects[_idx_cur++], sender);
                }

                else if (nameMethod == nameof(_s.TryBuyFromMarketBuildingM))
                {
                    _s.TryBuyFromMarketBuildingM((MarketBuyTypes)objects[_idx_cur++], sender);
                }

                else if (nameMethod == nameof(_s.UnitSs.UnitAbilitiesSs.CurcularAttackKingM))
                {
                    _s.UnitSs.UnitAbilitiesSs.CurcularAttackKingM((byte)objects[_idx_cur++], AbilityTypes.CircularAttack, sender);
                }

                else if (nameMethod == nameof(_s.UnitSs.UnitAbilitiesSs.FirePawn))
                {
                    _s.UnitSs.UnitAbilitiesSs.FirePawn((byte)objects[_idx_cur++], sender);
                }

                else if (nameMethod == nameof(_s.UnitSs.UnitAbilitiesSs.PutOut))
                {
                    _s.UnitSs.UnitAbilitiesSs.PutOut((byte)objects[_idx_cur++], sender);
                }

                else if (nameMethod == nameof(_s.TrySeedYoungForestOnCellWithPawnM))
                {
                    _s.TrySeedYoungForestOnCellWithPawnM(AbilityTypes.Seed, sender, (byte)objects[_idx_cur++]);
                }

                else if (nameMethod == nameof(_s.TryBuildFarmOnCellWithUnitM))
                {
                    _s.TryBuildFarmOnCellWithUnitM((byte)objects[_idx_cur++], sender);
                }

                else if (nameMethod == nameof(_s.BuildingSs.TryDestroyM))
                {
                    _s.BuildingSs.TryDestroyM((byte)objects[_idx_cur++], sender);
                }

                else if (nameMethod == nameof(_s.UnitSs.UnitAbilitiesSs.FirePawn))
                {
                    _s.UnitSs.UnitAbilitiesSs.TryFireForestWithPawnM((byte)objects[_idx_cur++], (byte)objects[_idx_cur++], sender);
                }

                else if (nameMethod == nameof(_s.UnitSs.UnitAbilitiesSs.TryGrowAdultForestM))
                {
                    _s.UnitSs.UnitAbilitiesSs.TryGrowAdultForestM((byte)objects[_idx_cur++], AbilityTypes.GrowAdultForest, sender);
                }

                else if (nameMethod == nameof(_s.UnitSs.UnitAbilitiesSs.TryStunWithElfemaleM))
                {
                    _s.UnitSs.UnitAbilitiesSs.TryStunWithElfemaleM((byte)objects[_idx_cur++], (byte)objects[_idx_cur++], AbilityTypes.StunElfemale, sender);
                }

                else if (nameMethod == nameof(_s.UnitSs.UnitAbilitiesSs.TryChangeCornerArcher))
                {
                    _s.UnitSs.UnitAbilitiesSs.TryChangeCornerArcher((byte)objects[_idx_cur++], AbilityTypes.ChangeCornerArcher, sender);
                }

                else if (nameMethod == nameof(_s.UnitSs.UnitAbilitiesSs.TryChangeCornerArcher))
                {
                    _s.UnitSs.UnitAbilitiesSs.TryChange((byte)objects[_idx_cur++], (byte)objects[_idx_cur++], AbilityTypes.ChangeDirectionWind, sender);
                }
            }

            else if (obj is RpcMasterTypes rpcT)
            {
                switch (rpcT)
                {
                    case RpcMasterTypes.UniqueAbility:
                        var abilityT = (AbilityTypes)objects[_idx_cur++];
                        switch (abilityT)
                        {
                            case AbilityTypes.IncreaseWindSnowy:
                                _s.UnitSs.UnitAbilitiesSs.IncreaseWindSnowyM(true, (byte)objects[_idx_cur++], abilityT, sender);
                                break;

                            case AbilityTypes.DecreaseWindSnowy:
                                _s.UnitSs.UnitAbilitiesSs.IncreaseWindSnowyM(false, (byte)objects[_idx_cur++], abilityT, sender);
                                break;

                            case AbilityTypes.Resurrect:
                                {
                                }
                                break;

                            case AbilityTypes.SetTeleport:
                                {
                                }
                                break;

                            case AbilityTypes.Teleport:
                                {
                                }
                                break;

                            case AbilityTypes.InvokeSkeletons:
                                {
                                }
                                break;

                            default: throw new Exception();
                        }
                        break;

                    default: throw new Exception();
                }
            }

            

            _s.GetDataCellsS.GetDataCells();
            _eMG.NeedUpdateView = true;

            SyncAllMaster();
        }

        [PunRPC]
        void GeneralRpc(object[] objects, PhotonMessageInfo infoFrom)
        {
            _idx_cur = 0;

            switch ((RpcGeneralTypes)objects[_idx_cur++])
            {
                case RpcGeneralTypes.None:
                    throw new Exception();

                case RpcGeneralTypes.SoundEffect:
                    _eMG.SoundAction((ClipTypes)objects[_idx_cur++]).Invoke();
                    break;

                case RpcGeneralTypes.SoundUniqueAbility:
                    _eMG.SoundAction((AbilityTypes)objects[_idx_cur++]).Invoke();
                    break;

                case RpcGeneralTypes.AnimationCell:
                    _eMG.DataFromViewC.AnimationCell((byte)objects[_idx_cur++], (AnimationCellTypes)objects[_idx_cur++]).Invoke();
                    break;

                case RpcGeneralTypes.ActiveMotion:
                    _eMG.MotionTimer = 4;
                    break;

                case RpcGeneralTypes.Mistake:

                    var mistakeT = (MistakeTypes)objects[_idx_cur++];

                    _s.Mistake(mistakeT);

                    _eMG.SoundAction(ClipTypes.WritePensil).Invoke();

                    //if (mistakeT == MistakeTypes.NeedMoreSteps || mistakeT == MistakeTypes.MinSpeedWind 
                    //    || mistakeT == MistakeTypes.MaxSpeedWind || mistakeT == MistakeTypes.NeedBuildingHouses
                    //    || mistakeT == MistakeTypes.NeedMoreHp || mistakeT == MistakeTypes.NeedMorePeopleInCity)
                    //{

                    //}
                    //else
                    //{
                    //    _e.Sound(ClipTypes.Mistake).Action.Invoke();
                    //}

                    if (mistakeT == MistakeTypes.Economy)
                    {
                        _eMG.MistakeEconomy(ResourceTypes.Food).Resources = 0;
                        _eMG.MistakeEconomy(ResourceTypes.Wood).Resources = 0;
                        _eMG.MistakeEconomy(ResourceTypes.Ore).Resources = 0;
                        _eMG.MistakeEconomy(ResourceTypes.Iron).Resources = 0;
                        _eMG.MistakeEconomy(ResourceTypes.Gold).Resources = 0;

                        var needRes = (float[])objects[_idx_cur++];

                        _eMG.MistakeEconomy(ResourceTypes.Food).Resources = needRes[0];
                        _eMG.MistakeEconomy(ResourceTypes.Wood).Resources = needRes[1];
                        _eMG.MistakeEconomy(ResourceTypes.Ore).Resources = needRes[2];
                        _eMG.MistakeEconomy(ResourceTypes.Iron).Resources = needRes[3];
                        _eMG.MistakeEconomy(ResourceTypes.Gold).Resources = needRes[4];
                    }
                    break;

                default:
                    throw new Exception();
            }
        }

        [PunRPC]
        void OtherRpc(object[] objects, PhotonMessageInfo infoFrom) => _eMG.RpcPoolEs.OtherRpc(objects, infoFrom);


        #region SyncData

        public void SyncAllMaster()
        {
            var objs = new List<object>();

            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                objs.Add(_eMG.UnitT(cell_0));
                objs.Add(_eMG.UnitLevelT(cell_0));
                objs.Add(_eMG.UnitPlayerT(cell_0));
                objs.Add(_eMG.UnitConditionT(cell_0));
                objs.Add(_eMG.IsRightArcherUnit(cell_0));
                for (var playerT = (PlayerTypes)0; playerT < PlayerTypes.End; playerT++)
                    objs.Add(_eMG.UnitVisibleC(cell_0).IsVisible(playerT));

                objs.Add(_eMG.HpUnit(cell_0));
                objs.Add(_eMG.StepUnit(cell_0));
                objs.Add(_eMG.WaterUnit(cell_0));

                objs.Add(_eMG.DamageAttack(cell_0));
                objs.Add(_eMG.DamageOnCell(cell_0));

                objs.Add(_eMG.MainToolWeaponT(cell_0));
                objs.Add(_eMG.MainTWLevelT(cell_0));

                objs.Add(_eMG.ExtraToolWeaponT(cell_0));
                objs.Add(_eMG.ExtraTWLevelT(cell_0));
                objs.Add(_eMG.ExtraTWProtection(cell_0));

                objs.Add(_eMG.PawnExtractAdultForest(cell_0));
                objs.Add(_eMG.PawnExtractHill(cell_0));

                objs.Add(_eMG.LastDiedUnitTC(cell_0).UnitT);
                objs.Add(_eMG.LastDiedLevelTC(cell_0).LevelT);
                objs.Add(_eMG.LastDiedPlayerTC(cell_0).PlayerT);

                objs.Add(_eMG.AttackSimpleCellsC(cell_0).IdxsByteClone);
                objs.Add(_eMG.AttackUniqueCellsC(cell_0).IdxsByteClone);

                objs.Add(_eMG.CellsForShift(cell_0).IdxsByteClone);
                objs.Add(_eMG.UnitNeedStepsForShiftC(cell_0).NeedStepsCopy);

                objs.Add(_eMG.UnitButtonAbilitiesC(cell_0).AbilityTypesClone);
                objs.Add(_eMG.UnitCooldownAbilitiesC(cell_0).CooldonwsFloat);

                objs.Add(_eMG.StunUnit(cell_0));
                objs.Add(_eMG.ShieldEffect(cell_0));
                objs.Add(_eMG.FrozenArrawEffect(cell_0));
                objs.Add(_eMG.HaveKingEffect(cell_0));

                objs.Add(_eMG.UnitForArsonC(cell_0).IdxsByteClone);


                #region Building

                objs.Add(_eMG.BuildingT(cell_0));
                objs.Add(_eMG.BuildingLevelT(cell_0));
                objs.Add(_eMG.BuildingPlayerT(cell_0));
                objs.Add(_eMG.BuildingHp(cell_0));
                objs.Add(_eMG.BuildingVisibleC(cell_0).IsVisibleClone);
                objs.Add(_eMG.WoodcutterExtractC(cell_0).Resources);
                objs.Add(_eMG.FarmExtractFertilizeC(cell_0).Resources);

                #endregion


                objs.Add(_eMG.YoungForestC(cell_0).Resources);
                objs.Add(_eMG.AdultForestC(cell_0).Resources);
                objs.Add(_eMG.MountainC(cell_0).Resources);
                objs.Add(_eMG.HillC(cell_0).Resources);
                objs.Add(_eMG.FertilizeC(cell_0).Resources);

                objs.Add(_eMG.RiverT(cell_0));
                objs.Add(_eMG.HaveRiverC(cell_0).HaveRives);

                objs.Add(_eMG.HaveFire(cell_0));

                objs.Add(_eMG.TrailVisibleC(cell_0).IsVisibleClone);
                objs.Add(_eMG.HealthTrail(cell_0).Healths);
            }

            objs.Add(_eMG.IsStartedGame);
            objs.Add(_eMG.Motions);
            objs.Add(_eMG.WhereTeleportC.Start);
            objs.Add(_eMG.WhereTeleportC.End);
            objs.Add(_eMG.WinnerPlayerT);
            objs.Add(_eMG.WhoseMovePlayerT);

            objs.Add(_eMG.WeatherE.WindC.DirectT);
            objs.Add(_eMG.WeatherE.WindC.Speed);
            objs.Add(_eMG.WeatherE.WindC.MaxSpeed);
            objs.Add(_eMG.WeatherE.WindC.MinSpeed);
            objs.Add(_eMG.WeatherE.CloudC.Center);
            objs.Add(_eMG.WeatherE.SunSideTC.SunSideT);


            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                objs.Add(_eMG.PlayerInfoE(playerT).IsReadyForStartOnlineGame);
                objs.Add(_eMG.PlayerInfoE(playerT).WoodForBuyHouse);
                objs.Add(_eMG.PlayerInfoE(playerT).BuildingsInfoC.HavBuildingsClone);
                objs.Add(_eMG.PlayerInfoE(playerT).WhereKingEffects.IdxsByteClone);

                objs.Add(_eMG.PlayerInfoE(playerT).KingInfoE.HaveInInventor);
                objs.Add(_eMG.PlayerInfoE(playerT).KingInfoE.CellKing);

                objs.Add(_eMG.PlayerInfoE(playerT).PawnInfoC.PeopleInCity);
                objs.Add(_eMG.PlayerInfoE(playerT).PawnInfoC.MaxAvailable);
                objs.Add(_eMG.PlayerInfoE(playerT).PawnInfoC.AmountInGame);

                objs.Add(_eMG.PlayerInfoE(playerT).GodInfoE.HaveHeroInInventor);
                objs.Add(_eMG.PlayerInfoE(playerT).GodInfoE.UnitT);
                objs.Add(_eMG.PlayerInfoE(playerT).GodInfoE.Cooldown);

                for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
                {
                    for (var twT = (ToolWeaponTypes)1; twT < ToolWeaponTypes.End; twT++)
                    {
                        objs.Add(_eMG.PlayerInfoE(playerT).LevelE(levelT).ToolWeapons(twT));
                    }

                    for (var buildingT = (BuildingTypes)1; buildingT < BuildingTypes.End; buildingT++)
                    {
                        objs.Add(_eMG.PlayerInfoE(playerT).LevelE(levelT).BuildingInfoE(buildingT).IdxC.IdxsByteClone);
                    }
                }

                for (var resT = (ResourceTypes)1; resT < ResourceTypes.End; resT++)
                {
                    objs.Add(_eMG.PlayerInfoE(playerT).ResourcesC(resT).Resources);
                }
            }



            var objects = new object[objs.Count];
            for (int i = 0; i < objects.Length; i++) objects[i] = objs[i];


            _eMG.RpcPoolEs.RPC(nameof(SyncAllOther), RpcTarget.Others, objects);
        }

        [PunRPC]
        void SyncAllOther(object[] objects)
        {
            if (PhotonNetwork.IsMasterClient) return;

            _idx_cur = 0;


            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                _eMG.UnitTC(cell_0).UnitT = (UnitTypes)objects[_idx_cur++];
                _eMG.UnitLevelTC(cell_0).LevelT = (LevelTypes)objects[_idx_cur++];
                _eMG.UnitPlayerTC(cell_0).PlayerT = (PlayerTypes)objects[_idx_cur++];
                _eMG.UnitConditionTC(cell_0).Condition = (ConditionUnitTypes)objects[_idx_cur++];
                _eMG.UnitIsRightArcherC(cell_0).IsRight = (bool)objects[_idx_cur++];
                for (var playerT = (PlayerTypes)0; playerT < PlayerTypes.End; playerT++)
                    _eMG.UnitVisibleC(cell_0).Set(playerT, (bool)objects[_idx_cur++]);

                _eMG.HpUnitC(cell_0).Health = (double)objects[_idx_cur++];
                _eMG.StepUnitC(cell_0).Steps = (double)objects[_idx_cur++];
                _eMG.WaterUnitC(cell_0).Water = (double)objects[_idx_cur++];

                _eMG.DamageAttackC(cell_0).Damage = (double)objects[_idx_cur++];
                _eMG.DamageOnCellC(cell_0).Damage = (double)objects[_idx_cur++];

                _eMG.MainToolWeaponTC(cell_0).ToolWeaponT = (ToolWeaponTypes)objects[_idx_cur++];
                _eMG.MainTWLevelTC(cell_0).LevelT = (LevelTypes)objects[_idx_cur++];

                _eMG.ExtraToolWeaponTC(cell_0).ToolWeaponT = (ToolWeaponTypes)objects[_idx_cur++];
                _eMG.ExtraTWLevelTC(cell_0).LevelT = (LevelTypes)objects[_idx_cur++];
                _eMG.ExtraTWProtectionC(cell_0).Protection = (float)objects[_idx_cur++];

                _eMG.PawnExtractAdultForestC(cell_0).Resources = (float)objects[_idx_cur++];
                _eMG.PawnExtractHillC(cell_0).Resources = (float)objects[_idx_cur++];

                _eMG.LastDiedUnitTC(cell_0).UnitT = (UnitTypes)objects[_idx_cur++];
                _eMG.LastDiedLevelTC(cell_0).LevelT = (LevelTypes)objects[_idx_cur++];
                _eMG.LastDiedPlayerTC(cell_0).PlayerT = (PlayerTypes)objects[_idx_cur++];

                _eMG.AttackSimpleCellsC(cell_0).Sync((byte[])objects[_idx_cur++]);
                _eMG.AttackUniqueCellsC(cell_0).Sync((byte[])objects[_idx_cur++]);

                _eMG.CellsForShift(cell_0).Sync((byte[])objects[_idx_cur++]);
                _eMG.UnitNeedStepsForShiftC(cell_0).Sync((float[])objects[_idx_cur++]);

                _eMG.UnitButtonAbilitiesC(cell_0).Sync((byte[])objects[_idx_cur++]);
                _eMG.UnitCooldownAbilitiesC(cell_0).Sync((float[])objects[_idx_cur++]);

                _eMG.StunUnitC(cell_0).Stun = (float)objects[_idx_cur++];
                _eMG.ShieldUnitEffectC(cell_0).Protection = (float)objects[_idx_cur++];
                _eMG.FrozenArrawEffectC(cell_0).Shoots = (int)objects[_idx_cur++];
                _eMG.HaveKingEffect(cell_0) = (bool)objects[_idx_cur++];

                _eMG.UnitForArsonC(cell_0).Sync((byte[])objects[_idx_cur++]);


                #region Building

                _eMG.BuildingTC(cell_0).BuildingT = (BuildingTypes)objects[_idx_cur++];
                _eMG.BuildingLevelTC(cell_0).LevelT = (LevelTypes)objects[_idx_cur++];
                _eMG.BuildingPlayerTC(cell_0).PlayerT = (PlayerTypes)objects[_idx_cur++];
                _eMG.BuildingHpC(cell_0).Health = (double)objects[_idx_cur++];
                _eMG.BuildingVisibleC(cell_0).Sync((bool[])objects[_idx_cur++]);
                _eMG.WoodcutterExtractC(cell_0).Resources = (float)objects[_idx_cur++];
                _eMG.FarmExtractFertilizeC(cell_0).Resources = (float)objects[_idx_cur++];

                #endregion


                _eMG.YoungForestC(cell_0).Resources = (float)objects[_idx_cur++];
                _eMG.AdultForestC(cell_0).Resources = (float)objects[_idx_cur++];
                _eMG.MountainC(cell_0).Resources = (float)objects[_idx_cur++];
                _eMG.HillC(cell_0).Resources = (float)objects[_idx_cur++];
                _eMG.FertilizeC(cell_0).Resources = (float)objects[_idx_cur++];

                _eMG.RiverTC(cell_0).RiverT = (RiverTypes)objects[_idx_cur++];
                _eMG.HaveRiverC(cell_0).Sync((bool[])objects[_idx_cur++]);

                _eMG.HaveFire(cell_0) = (bool)objects[_idx_cur++];

                _eMG.TrailVisibleC(cell_0).Sync((bool[])objects[_idx_cur++]);
                _eMG.HealthTrail(cell_0).Sync((float[])objects[_idx_cur++]);
            }

            _eMG.IsStartedGame = (bool)objects[_idx_cur++];
            _eMG.MotionsC.Motions = (int)objects[_idx_cur++];
            _eMG.WhereTeleportC.Start = (byte)objects[_idx_cur++];
            _eMG.WhereTeleportC.End = (byte)objects[_idx_cur++];
            _eMG.WinnerPlayerTC.PlayerT = (PlayerTypes)objects[_idx_cur++];
            _eMG.WhoseMovePlayerTC.PlayerT = (PlayerTypes)objects[_idx_cur++];

            _eMG.WeatherE.WindC.DirectT = (DirectTypes)objects[_idx_cur++];
            _eMG.WeatherE.WindC.Speed = (float)objects[_idx_cur++];
            _eMG.WeatherE.WindC.MaxSpeed = (float)objects[_idx_cur++];
            _eMG.WeatherE.WindC.MinSpeed = (float)objects[_idx_cur++];
            _eMG.WeatherE.CloudC.Center = (byte)objects[_idx_cur++];
            _eMG.WeatherE.SunSideTC.SunSideT = (SunSideTypes)objects[_idx_cur++];

            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                _eMG.PlayerInfoE(playerT).IsReadyForStartOnlineGame = (bool)objects[_idx_cur++];
                _eMG.PlayerInfoE(playerT).WoodForBuyHouse = (float)objects[_idx_cur++];
                _eMG.PlayerInfoE(playerT).BuildingsInfoC.Sync((bool[])objects[_idx_cur++]);
                _eMG.PlayerInfoE(playerT).WhereKingEffects.Sync((byte[])objects[_idx_cur++]);

                _eMG.PlayerInfoE(playerT).KingInfoE.HaveInInventor = (bool)objects[_idx_cur++];
                _eMG.PlayerInfoE(playerT).KingInfoE.CellKing = (byte)objects[_idx_cur++];

                _eMG.PlayerInfoE(playerT).PawnInfoC.PeopleInCity = (int)objects[_idx_cur++];
                _eMG.PlayerInfoE(playerT).PawnInfoC.MaxAvailable = (int)objects[_idx_cur++];
                _eMG.PlayerInfoE(playerT).PawnInfoC.AmountInGame = (int)objects[_idx_cur++];

                _eMG.PlayerInfoE(playerT).GodInfoE.HaveHeroInInventor = (bool)objects[_idx_cur++];
                _eMG.PlayerInfoE(playerT).GodInfoE.UnitT = (UnitTypes)objects[_idx_cur++];
                _eMG.PlayerInfoE(playerT).GodInfoE.Cooldown = (float)objects[_idx_cur++];

                for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
                {
                    for (var twT = (ToolWeaponTypes)1; twT < ToolWeaponTypes.End; twT++)
                    {
                        _eMG.PlayerInfoE(playerT).LevelE(levelT).ToolWeapons(twT) = (int)objects[_idx_cur++];
                    }

                    for (var buildingT = (BuildingTypes)1; buildingT < BuildingTypes.End; buildingT++)
                    {
                        _eMG.PlayerInfoE(playerT).LevelE(levelT).BuildingInfoE(buildingT).IdxC.Sync((byte[])objects[_idx_cur++]);
                    }
                }

                for (var resT = (ResourceTypes)1; resT < ResourceTypes.End; resT++)
                {
                    _eMG.PlayerInfoE(playerT).ResourcesC(resT).Resources = (float)objects[_idx_cur++];
                }
            }



            _eMG.NeedUpdateView = true;
        }

        #endregion


        #region Serialize

        //public void Init()
        //{
        //    PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);
        //}

        //public static object DeserializeVector2Int(byte[] data)
        //{
        //    Vector2Int result = new Vector2Int();

        //    result.x = BitConverter.ToInt32(data, 0);
        //    result.y = BitConverter.ToInt32(data, 4);

        //    return result;

        //}
        //public static byte[] SerializeVector2Int(object obj)
        //{
        //    Vector2Int vector = (Vector2Int)obj;
        //    byte[] result = new byte[8];

        //    BitConverter.GetBytes(vector.x).CopyTo(result, 0);
        //    BitConverter.GetBytes(vector.y).CopyTo(result, 4);

        //    return result;
        //}

        #endregion
    }
}