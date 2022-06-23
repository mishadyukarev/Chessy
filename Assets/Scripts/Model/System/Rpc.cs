using Chessy.Model.Model.System;
using Photon.Pun;
using System;
using UnityEngine;

namespace Chessy.Model
{
    public sealed class Rpc : MonoBehaviour
    {
        SystemsModel _s;

        int _idxCurrent;

        public static string NameRpcMethod => nameof(PunRPC);

        public Rpc GiveData(in SystemsModel sMGame)
        {
            _s = sMGame;
            return this;
        }


        [PunRPC]
        void PunRPC(object[] objects, PhotonMessageInfo infoFrom)
        {
            _idxCurrent = 0;
            var sender = infoFrom.Sender;
            var obj = objects[_idxCurrent++];

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
                        _s.TryShiftUnitOntoOtherCellM((byte)objects[_idxCurrent++], (byte)objects[_idxCurrent++], sender);
                        break;

                    case nameof(_s.TryAttackUnitOnCellM):
                        _s.TryAttackUnitOnCellM((byte)objects[_idxCurrent++], (byte)objects[_idxCurrent++], sender);
                        break;

                    case nameof(_s.TrySetConditionUnitOnCellM):
                        _s.TrySetConditionUnitOnCellM((ConditionUnitTypes)objects[_idxCurrent++], (byte)objects[_idxCurrent++], sender);
                        break;

                    case nameof(_s.TrySetUnitOnCellM):
                        var cellIdx = (byte)objects[_idxCurrent++];
                        _s.TrySetUnitOnCellM((UnitTypes)objects[_idxCurrent++], sender, cellIdx);
                        break;

                    case nameof(_s.GetHeroInCenterM):
                        _s.GetHeroInCenterM((UnitTypes)objects[_idxCurrent++], sender);
                        break;

                    case nameof(_s.TryMeltInMelterBuildingM):
                        _s.TryMeltInMelterBuildingM(sender);
                        break;

                    case nameof(_s.TryGiveTakeToolOrWeaponToUnitOnCellM):
                        var idxCell = (byte)objects[_idxCurrent++];
                        _s.TryGiveTakeToolOrWeaponToUnitOnCellM((ToolWeaponTypes)objects[_idxCurrent++], (LevelTypes)objects[_idxCurrent++], idxCell, sender);
                        break;

                    case nameof(_s.TryBuyFromMarketBuildingM):
                        _s.TryBuyFromMarketBuildingM((MarketBuyTypes)objects[_idxCurrent++], sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.CurcularAttackKingM):
                        _s.UnitSs.UnitAbilitiesSs.CurcularAttackKingM((byte)objects[_idxCurrent++], AbilityTypes.CircularAttack, sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.TryFireForestWithSimplePawnM):
                        _s.UnitSs.UnitAbilitiesSs.TryFireForestWithSimplePawnM((byte)objects[_idxCurrent++], sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.TryPutOutFireForestWithSimplePawnM):
                        _s.UnitSs.UnitAbilitiesSs.TryPutOutFireForestWithSimplePawnM((byte)objects[_idxCurrent++], sender);
                        break;

                    case nameof(_s.TrySeedYoungForestOnCellWithPawnM):
                        _s.TrySeedYoungForestOnCellWithPawnM(AbilityTypes.Seed, sender, (byte)objects[_idxCurrent++]);
                        break;

                    case nameof(_s.TryBuildFarmOnCellWithSimplePawnM):
                        _s.TryBuildFarmOnCellWithSimplePawnM((byte)objects[_idxCurrent++], sender);
                        break;

                    case nameof(_s.UnitSs.TryDestroyBuildingWithSimplePawnM):
                        _s.UnitSs.TryDestroyBuildingWithSimplePawnM((byte)objects[_idxCurrent++], sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.TryFireForestWithArcherM):
                        _s.UnitSs.UnitAbilitiesSs.TryFireForestWithArcherM((byte)objects[_idxCurrent++], (byte)objects[_idxCurrent++], sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.TryGrowAdultForestWithElfemaleM):
                        _s.UnitSs.UnitAbilitiesSs.TryGrowAdultForestWithElfemaleM((byte)objects[_idxCurrent++], AbilityTypes.GrowAdultForest, sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.TryStunEnemyWithElfemaleM):
                        _s.UnitSs.UnitAbilitiesSs.TryStunEnemyWithElfemaleM((byte)objects[_idxCurrent++], (byte)objects[_idxCurrent++], AbilityTypes.StunElfemale, sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.TryChangeCornerArcher):
                        _s.UnitSs.UnitAbilitiesSs.TryChangeCornerArcher((byte)objects[_idxCurrent++], AbilityTypes.ChangeCornerArcher, sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.TryChangeDirectWindWithSnowyM):
                        _s.UnitSs.UnitAbilitiesSs.TryChangeDirectWindWithSnowyM((byte)objects[_idxCurrent++], (byte)objects[_idxCurrent++], AbilityTypes.ChangeDirectionWind, sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.IncreaseWindWithRainyM):
                        _s.UnitSs.UnitAbilitiesSs.IncreaseWindWithRainyM((byte)objects[_idxCurrent++], AbilityTypes.IncreaseWindSnowy, sender);
                        break;

                    case nameof(_s.UnitSs.UnitAbilitiesSs.DecreaseWindWithRainyM):
                        _s.UnitSs.UnitAbilitiesSs.DecreaseWindWithRainyM((byte)objects[_idxCurrent++], AbilityTypes.DecreaseWindSnowy, sender);
                        break;

                    case nameof(_s.ExecuteSoundAction):
                        var obj1 = objects[_idxCurrent++];

                        if (obj1 is ClipTypes clipT) _s.ExecuteSoundAction(clipT);
                        else _s.ExecuteSoundAction((AbilityTypes)obj1);
                        break;

                    case nameof(_s.ExecuteAnimationClip):
                        _s.ExecuteAnimationClip((byte)objects[_idxCurrent++], (AnimationCellTypes)objects[_idxCurrent++]);
                        break;

                    case nameof(_s.ActiveMotion):
                        _s.ActiveMotion();
                        break;

                    case nameof(_s.ExecuteMistake):
                        var mistakeT = (MistakeTypes)objects[_idxCurrent++];
                        _s.ExecuteMistake(mistakeT, mistakeT == MistakeTypes.Economy ? (float[])objects[_idxCurrent++] : default);
                        break;

                    case nameof(_s.SyncData):
                        _s.SyncData(objects);
                        break;

                    case nameof(_s.ForUISystems.TryBuyBuildingInTownM):
                        _s.ForUISystems.TryBuyBuildingInTownM((BuildingTypes)objects[_idxCurrent++], sender);
                        break;

                    default: throw new Exception();
                }
            }

            _s.GetDataCellsS.GetDataCells();

            _s.SyncDataM();
        }
    }
}