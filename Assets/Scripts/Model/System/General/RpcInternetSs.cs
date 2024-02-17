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
                    case nameof(s.TryExecuteDoneReadyM):
                        s.TryExecuteDoneReadyM(sender);
                        break;

                    case nameof(s.TryExecuteReadyForOnlineM):
                        s.TryExecuteReadyForOnlineM(sender);
                        break;

                    case nameof(s.TryShiftUnitOntoOtherCellM):
                        s.TryShiftUnitOntoOtherCellM((byte)objects[cellIdxCurrent++], (byte)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(s.TryAttackUnitOnCellM):
                        s.TryAttackUnitOnCellM((byte)objects[cellIdxCurrent++], (byte)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(s.TrySetConditionUnitOnCellM):
                        s.TrySetConditionUnitOnCellM((ConditionUnitTypes)objects[cellIdxCurrent++], (byte)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(s.TrySetUnitOnCellM):
                        var cellIdx = (byte)objects[cellIdxCurrent++];
                        s.TrySetUnitOnCellM((UnitTypes)objects[cellIdxCurrent++], sender, cellIdx);
                        break;

                    case nameof(s.GetHeroInCenterM):
                        s.GetHeroInCenterM((UnitTypes)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(s.TryMeltInMelterBuildingM):
                        s.TryMeltInMelterBuildingM(sender);
                        break;

                    case nameof(s.TryGiveTakeToolOrWeaponToUnitOnCellM):
                        var idxCell = (byte)objects[cellIdxCurrent++];
                        s.TryGiveTakeToolOrWeaponToUnitOnCellM((ToolsWeaponsWarriorTypes)objects[cellIdxCurrent++], (LevelTypes)objects[cellIdxCurrent++], idxCell, sender);
                        break;

                    case nameof(s.TryBuyFromMarketBuildingM):
                        s.TryBuyFromMarketBuildingM((MarketBuyTypes)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(s.unitSs.unitAbilitiesSs.CircularAttackKingM):
                        s.unitSs.unitAbilitiesSs.CircularAttackKingM((byte)objects[cellIdxCurrent++], AbilityTypes.CircularAttack, sender);
                        break;

                    case nameof(s.unitSs.unitAbilitiesSs.TryFireForestWithSimplePawnM):
                        s.unitSs.unitAbilitiesSs.TryFireForestWithSimplePawnM((byte)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(s.unitSs.unitAbilitiesSs.TryPutOutFireForestWithSimplePawnM):
                        s.unitSs.unitAbilitiesSs.TryPutOutFireForestWithSimplePawnM((byte)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(s.TrySeedYoungForestOnCellWithPawnM):
                        s.TrySeedYoungForestOnCellWithPawnM(AbilityTypes.Seed, sender, (byte)objects[cellIdxCurrent++]);
                        break;

                    case nameof(s.TryBuildFarmOnCellWithSimplePawnM):
                        s.TryBuildFarmOnCellWithSimplePawnM((byte)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(s.unitSs.TryDestroyBuildingWithSimplePawnM):
                        s.unitSs.TryDestroyBuildingWithSimplePawnM((byte)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(s.unitSs.unitAbilitiesSs.TryFireForestWithArcherM):
                        s.unitSs.unitAbilitiesSs.TryFireForestWithArcherM((byte)objects[cellIdxCurrent++], (byte)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(s.unitSs.unitAbilitiesSs.TryGrowAdultForestWithElfemaleM):
                        s.unitSs.unitAbilitiesSs.TryGrowAdultForestWithElfemaleM((byte)objects[cellIdxCurrent++], AbilityTypes.GrowAdultForest, sender);
                        break;

                    case nameof(s.unitSs.unitAbilitiesSs.stunElfemaleS_M.TryStunEnemyWithElfemaleM):
                        s.unitSs.unitAbilitiesSs.stunElfemaleS_M.TryStunEnemyWithElfemaleM((byte)objects[cellIdxCurrent++], (byte)objects[cellIdxCurrent++], AbilityTypes.StunElfemale, sender);
                        break;

                    case nameof(s.unitSs.unitAbilitiesSs.changeCornerArcherS.TryChangeCornerArcher):
                        s.unitSs.unitAbilitiesSs.changeCornerArcherS.TryChangeCornerArcher((byte)objects[cellIdxCurrent++], AbilityTypes.ChangeCornerArcher, sender);
                        break;

                    case nameof(s.unitSs.unitAbilitiesSs.TryChangeDirectWindWithSnowyM):
                        s.unitSs.unitAbilitiesSs.TryChangeDirectWindWithSnowyM((byte)objects[cellIdxCurrent++], (byte)objects[cellIdxCurrent++], AbilityTypes.ChangeDirectionWind, sender);
                        break;

                    case nameof(s.unitSs.unitAbilitiesSs.IncreaseWindWithRainyM):
                        s.unitSs.unitAbilitiesSs.IncreaseWindWithRainyM((byte)objects[cellIdxCurrent++], AbilityTypes.IncreaseWindSnowy, sender);
                        break;

                    case nameof(s.unitSs.unitAbilitiesSs.DecreaseWindWithRainyM):
                        s.unitSs.unitAbilitiesSs.DecreaseWindWithRainyM((byte)objects[cellIdxCurrent++], AbilityTypes.DecreaseWindSnowy, sender);
                        break;

                    case nameof(s.ExecuteSoundActionClip):
                        s.ExecuteSoundActionClip((ClipTypes)objects[cellIdxCurrent++]);
                        break;

                    case nameof(s.ExecuteSoundActionAbility):
                        s.ExecuteSoundActionAbility((AbilityTypes)objects[cellIdxCurrent++]);
                        break;

                    case nameof(s.ExecuteAnimationClip):
                        s.ExecuteAnimationClip((byte)objects[cellIdxCurrent++], (AnimationCellTypes)objects[cellIdxCurrent++]);
                        break;

                    case nameof(s.ExecuteAnimationCellDirectlyClip):
                        s.ExecuteAnimationCellDirectlyClip((byte)objects[cellIdxCurrent++], (CellAnimationDirectlyTypes)objects[cellIdxCurrent++]);
                        break;

                    case nameof(s.ExecuteMistake):
                        var mistakeT = (MistakeTypes)objects[cellIdxCurrent++];
                        s.ExecuteMistake(mistakeT, mistakeT == MistakeTypes.Economy ? (float[])objects[cellIdxCurrent++] : default);
                        break;

                    case nameof(s.TryBuyBuildingInTownM):
                        s.TryBuyBuildingInTownM((BuildingTypes)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(s.GetDataCellsS.GetDataCells):
                        s.GetDataCellsS.GetDataCells();
                        break;

                    default:
                        {
                            Debug.Log(nameMethod);
                        }
                        throw new Exception();
                }
            }

            s.GetDataCellsS.GetDataCells();

            //_s.SyncDataS.TrySyncDataM();
        }
        internal void ExecuteSoundActionToGeneral(in RpcTarget rpcTargetT, in ClipTypes clipT) => rpcC.Rpc(rpcTargetT, new object[] { nameof(s.ExecuteSoundActionClip), clipT });
        internal void ExecuteSoundActionToGeneral(in Player playerTo, ClipTypes clipT) => rpcC.Rpc(playerTo, new object[] { nameof(s.ExecuteSoundActionClip), clipT });
        internal void SoundToGeneral(RpcTarget rpcTarget, AbilityTypes abilityT) => rpcC.Rpc(rpcTarget, new object[] { nameof(s.ExecuteSoundActionAbility), abilityT });
        internal void SoundToGeneral(Player playerTo, AbilityTypes abilityT) => rpcC.Rpc(playerTo, new object[] { nameof(s.ExecuteSoundActionAbility), abilityT });

        internal void AnimationCellToGeneral(in byte cellIdx, in AnimationCellTypes animationCellT, in RpcTarget rpcTarget) => rpcC.Rpc(rpcTarget, new object[] { nameof(s.ExecuteAnimationClip), cellIdx, animationCellT });
        internal void AnimationCellToGeneral(in byte cellIdx, in AnimationCellTypes animationCellT, in Player sender) => rpcC.Rpc(sender, new object[] { nameof(s.ExecuteAnimationClip), cellIdx, animationCellT });
        internal void ExecuteAnimationCellDirectlyToGeneral(in byte cellIdx, in CellAnimationDirectlyTypes animationCellT, in RpcTarget rpcTarget) => rpcC.Rpc(rpcTarget, new object[] { nameof(s.ExecuteAnimationClip), cellIdx, animationCellT });

        internal void SimpleMistakeToGeneral(MistakeTypes mistakeType, Player playerTo) => rpcC.Rpc(playerTo, new object[] { nameof(s.ExecuteMistake), mistakeType });
        internal void SimpleMistakeToGeneral(Player playerTo, Dictionary<ResourceTypes, float> needRes)
        {
            var needRes2 = new float[(int)ResourceTypes.End];
            needRes2[0] = needRes[ResourceTypes.Food];
            needRes2[1] = needRes[ResourceTypes.Wood];
            needRes2[2] = needRes[ResourceTypes.Ore];
            needRes2[3] = needRes[ResourceTypes.Iron];
            needRes2[4] = needRes[ResourceTypes.Gold];

            rpcC.Rpc(playerTo, new object[] { nameof(s.ExecuteMistake), MistakeTypes.Economy, needRes2 });
        }
    }
}