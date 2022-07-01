using Photon.Pun;
using System;
using UnityEngine;
namespace Chessy.Model.System
{
    public sealed class Rpc : MonoBehaviour
    {
        SystemsModel _s;

        public static string NameRpcMethod => nameof(PunRPC);

        public Rpc GiveSystems(in SystemsModel sMGame)
        {
            _s = sMGame;
            return this;
        }


        [PunRPC]
        void PunRPC(object[] objects, PhotonMessageInfo infoFrom)
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
                        _s.TryGiveTakeToolOrWeaponToUnitOnCellM((ToolWeaponTypes)objects[cellIdxCurrent++], (LevelTypes)objects[cellIdxCurrent++], idxCell, sender);
                        break;

                    case nameof(_s.TryBuyFromMarketBuildingM):
                        _s.TryBuyFromMarketBuildingM((MarketBuyTypes)objects[cellIdxCurrent++], sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.CurcularAttackKingM):
                        _s.UnitSs.UnitAbilitiesSs.CurcularAttackKingM((byte)objects[cellIdxCurrent++], AbilityTypes.CircularAttack, sender);
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

                    case nameof(_s.ExecuteSoundAction):
                        var obj1 = objects[cellIdxCurrent++];

                        if (obj1 is ClipTypes clipT) _s.ExecuteSoundAction(clipT);
                        else _s.ExecuteSoundAction((AbilityTypes)obj1);
                        break;

                    case nameof(_s.ExecuteAnimationClip):
                        _s.ExecuteAnimationClip((byte)objects[cellIdxCurrent++], (AnimationCellTypes)objects[cellIdxCurrent++]);
                        break;

                    case nameof(_s.ActiveMotion):
                        _s.ActiveMotion();
                        break;

                    case nameof(_s.ExecuteMistake):
                        var mistakeT = (MistakeTypes)objects[cellIdxCurrent++];
                        _s.ExecuteMistake(mistakeT, mistakeT == MistakeTypes.Economy ? (float[])objects[cellIdxCurrent++] : default);
                        break;

                    case nameof(_s.SyncData):
                        _s.SyncData(objects);
                        break;

                    case nameof(_s.TryBuyBuildingInTownM):
                        _s.TryBuyBuildingInTownM((BuildingTypes)objects[cellIdxCurrent++], sender);
                        break;

                    default: throw new Exception();
                }
            }

            _s.GetDataCellsS.GetDataCells();

            _s.SyncDataM();
        }
    }
}