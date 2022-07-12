using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Model.System
{
    sealed class RpcInternetSs : SystemModelAbstract
    {
        internal RpcInternetSs(in SystemsModel sM, in EntitiesModel eM) : base(sM, eM)
        {
        }


        public void PunRPC(object[] objects, in PhotonMessageInfo infoFrom)
        {
            var cellIdxCurrent = 0;
            var sender = infoFrom.Sender;
            var obj = objects[cellIdxCurrent++];

            if (obj is string nameMethod)
            {
                switch (nameMethod)
                {
                    case nameof(_s.TryExecuteDoneReadyM):
                        _s.TryExecuteDoneReadyM(sender);
                        break;

                    case nameof(_s.TryExecuteReadyForOnlineM):
                        _s.TryExecuteReadyForOnlineM(sender);
                        break;

                    case nameof(_s.TryShiftUnitOntoOtherCellM):
                        _s.TryShiftUnitOntoOtherCellM((byte)objects[cellIdxCurrent++], (byte)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(_s.TryAttackUnitOnCellM):
                        _s.TryAttackUnitOnCellM((byte)objects[cellIdxCurrent++], (byte)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(_s.TrySetConditionUnitOnCellM):
                        _s.TrySetConditionUnitOnCellM((ConditionUnitTypes)objects[cellIdxCurrent++], (byte)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(_s.TrySetUnitOnCellM):
                        var cellIdx = (byte)objects[cellIdxCurrent++];
                        _s.TrySetUnitOnCellM((UnitTypes)objects[cellIdxCurrent++], sender, cellIdx);
                        break;

                    case nameof(_s.GetHeroInCenterM):
                        _s.GetHeroInCenterM((UnitTypes)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(_s.TryMeltInMelterBuildingM):
                        _s.TryMeltInMelterBuildingM(sender);
                        break;

                    case nameof(_s.TryGiveTakeToolOrWeaponToUnitOnCellM):
                        var idxCell = (byte)objects[cellIdxCurrent++];
                        _s.TryGiveTakeToolOrWeaponToUnitOnCellM((ToolsWeaponsWarriorTypes)objects[cellIdxCurrent++], (LevelTypes)objects[cellIdxCurrent++], idxCell, sender);
                        break;

                    case nameof(_s.TryBuyFromMarketBuildingM):
                        _s.TryBuyFromMarketBuildingM((MarketBuyTypes)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.CircularAttackKingM):
                        _s.UnitSs.UnitAbilitiesSs.CircularAttackKingM((byte)objects[cellIdxCurrent++], AbilityTypes.CircularAttack, sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.TryFireForestWithSimplePawnM):
                        _s.UnitSs.UnitAbilitiesSs.TryFireForestWithSimplePawnM((byte)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.TryPutOutFireForestWithSimplePawnM):
                        _s.UnitSs.UnitAbilitiesSs.TryPutOutFireForestWithSimplePawnM((byte)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(_s.TrySeedYoungForestOnCellWithPawnM):
                        _s.TrySeedYoungForestOnCellWithPawnM(AbilityTypes.Seed, sender, (byte)objects[cellIdxCurrent++]);
                        break;

                    case nameof(_s.TryBuildFarmOnCellWithSimplePawnM):
                        _s.TryBuildFarmOnCellWithSimplePawnM((byte)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(_s.UnitSs.TryDestroyBuildingWithSimplePawnM):
                        _s.UnitSs.TryDestroyBuildingWithSimplePawnM((byte)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.TryFireForestWithArcherM):
                        _s.UnitSs.UnitAbilitiesSs.TryFireForestWithArcherM((byte)objects[cellIdxCurrent++], (byte)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.TryGrowAdultForestWithElfemaleM):
                        _s.UnitSs.UnitAbilitiesSs.TryGrowAdultForestWithElfemaleM((byte)objects[cellIdxCurrent++], AbilityTypes.GrowAdultForest, sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.TryStunEnemyWithElfemaleM):
                        _s.UnitSs.UnitAbilitiesSs.TryStunEnemyWithElfemaleM((byte)objects[cellIdxCurrent++], (byte)objects[cellIdxCurrent++], AbilityTypes.StunElfemale, sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.TryChangeCornerArcher):
                        _s.UnitSs.UnitAbilitiesSs.TryChangeCornerArcher((byte)objects[cellIdxCurrent++], AbilityTypes.ChangeCornerArcher, sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.TryChangeDirectWindWithSnowyM):
                        _s.UnitSs.UnitAbilitiesSs.TryChangeDirectWindWithSnowyM((byte)objects[cellIdxCurrent++], (byte)objects[cellIdxCurrent++], AbilityTypes.ChangeDirectionWind, sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.IncreaseWindWithRainyM):
                        _s.UnitSs.UnitAbilitiesSs.IncreaseWindWithRainyM((byte)objects[cellIdxCurrent++], AbilityTypes.IncreaseWindSnowy, sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.DecreaseWindWithRainyM):
                        _s.UnitSs.UnitAbilitiesSs.DecreaseWindWithRainyM((byte)objects[cellIdxCurrent++], AbilityTypes.DecreaseWindSnowy, sender);
                        break;

                    case nameof(_s.ExecuteSoundActionClip):
                        _s.ExecuteSoundActionClip((ClipTypes)objects[cellIdxCurrent++]);
                        break;

                    case nameof(_s.ExecuteSoundActionAbility):
                        _s.ExecuteSoundActionAbility((AbilityTypes)objects[cellIdxCurrent++]);
                        break;

                    case nameof(_s.ExecuteAnimationClip):
                        _s.ExecuteAnimationClip((byte)objects[cellIdxCurrent++], (AnimationCellTypes)objects[cellIdxCurrent++]);
                        break;

                    case nameof(_s.ExecuteAnimationCellDirectlyClip):
                        _s.ExecuteAnimationCellDirectlyClip((byte)objects[cellIdxCurrent++], (CellAnimationDirectlyTypes)objects[cellIdxCurrent++]);
                        break;

                    case nameof(_s.ActiveMotion):
                        _s.ActiveMotion();
                        break;

                    case nameof(_s.ExecuteMistake):
                        var mistakeT = (MistakeTypes)objects[cellIdxCurrent++];
                        _s.ExecuteMistake(mistakeT, mistakeT == MistakeTypes.Economy ? (float[])objects[cellIdxCurrent++] : default);
                        break;

                    case nameof(_s.SyncDataS.SyncData):
                        _s.SyncDataS.SyncData(objects);
                        break;

                    case nameof(_s.TryBuyBuildingInTownM):
                        _s.TryBuyBuildingInTownM((BuildingTypes)objects[cellIdxCurrent++], sender);
                        break;

                    default:
                        {
                            Debug.Log(nameMethod);
                        }
                        throw new Exception();
                }
            }

            _s.GetDataCellsS.GetDataCellsM();
        }
        internal void ExecuteSoundActionToGeneral(in RpcTarget rpcTargetT, in ClipTypes clipT) => _e.RpcC.Action0(_e.RpcC.PunRPCName, rpcTargetT, new object[] { nameof(_s.ExecuteSoundActionClip), clipT });
        internal void ExecuteSoundActionToGeneral(in Player playerTo, ClipTypes clipT) => _e.RpcC.Action1(_e.RpcC.PunRPCName, playerTo, new object[] { nameof(_s.ExecuteSoundActionClip), clipT });
        internal void SoundToGeneral(RpcTarget rpcTarget, AbilityTypes abilityT) => _e.RpcC.Action0(_e.RpcC.PunRPCName, rpcTarget, new object[] { nameof(_s.ExecuteSoundActionAbility), abilityT });
        internal void SoundToGeneral(Player playerTo, AbilityTypes abilityT) => _e.RpcC.Action1(_e.RpcC.PunRPCName, playerTo, new object[] { nameof(_s.ExecuteSoundActionAbility), abilityT });

        internal void ActiveMotionZoneToGeneneral(in Player player) => _e.RpcC.Action1(_e.RpcC.PunRPCName, player, new object[] { nameof(_s.ActiveMotion) });
        internal void ActiveMotionZoneToGeneneral(in RpcTarget rpcTarget) => _e.RpcC.Action0(_e.RpcC.PunRPCName, rpcTarget, new object[] { nameof(_s.ActiveMotion) });

        internal void AnimationCellToGeneral(in byte cellIdx, in AnimationCellTypes animationCellT, in RpcTarget rpcTarget) => _e.RpcC.Action0(_e.RpcC.PunRPCName, rpcTarget, new object[] { nameof(_s.ExecuteAnimationClip), cellIdx, animationCellT });
        internal void ExecuteAnimationCellDirectlyToGeneral(in byte cellIdx, in CellAnimationDirectlyTypes animationCellT, in RpcTarget rpcTarget) => _e.RpcC.Action0(_e.RpcC.PunRPCName, rpcTarget, new object[] { nameof(_s.ExecuteAnimationClip), cellIdx, animationCellT });

        internal void SimpleMistakeToGeneral(MistakeTypes mistakeType, Player playerTo) => _e.RpcC.Action1(_e.RpcC.PunRPCName, playerTo, new object[] { nameof(_s.ExecuteMistake), mistakeType });
        internal void SimpleMistakeToGeneral(Player playerTo, Dictionary<ResourceTypes, float> needRes)
        {
            var needRes2 = new float[(int)ResourceTypes.End];
            needRes2[0] = needRes[ResourceTypes.Food];
            needRes2[1] = needRes[ResourceTypes.Wood];
            needRes2[2] = needRes[ResourceTypes.Ore];
            needRes2[3] = needRes[ResourceTypes.Iron];
            needRes2[4] = needRes[ResourceTypes.Gold];

            _e.RpcC.Action1(_e.RpcC.PunRPCName, playerTo, new object[] { nameof(_s.ExecuteMistake), MistakeTypes.Economy, needRes2 });
        }
    }
}