using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct RpcPoolEs
    {
        int _idx_cur;

        internal readonly ActionMy<string, RpcTarget, object[]> Action0;
        internal readonly ActionMy<string, Player, object[]> Action1;

        internal readonly string MasterRPCName;
        internal readonly string GeneralRPCName;
        internal readonly string OtherRPCName;

        public RpcPoolEs(in List<object> actions, in List<string> names) : this()
        {
            _idx_cur = 0;

            Action0 = (ActionMy<string, RpcTarget, object[]>)actions[_idx_cur++];
            Action1 = (ActionMy<string, Player, object[]>)actions[_idx_cur++];

            _idx_cur = 0;
            MasterRPCName = names[_idx_cur++];
            GeneralRPCName = names[_idx_cur++];
            OtherRPCName = names[_idx_cur++];

            _idx_cur = 0;
        }

        public void OtherRpc(object[] objects, PhotonMessageInfo infoFrom)
        {
            _idx_cur = 0;
            var rpcT = (RpcOtherTypes)objects[_idx_cur++];

            switch (rpcT)
            {
                case RpcOtherTypes.None:
                    throw new Exception();

                case RpcOtherTypes.Sync:

                    break;

                default:
                    throw new Exception();
            }

        }


        public void RPC(in string name, in RpcTarget rpcTarget, params object[] objs)
        {
            Action0(name, rpcTarget, objs);
        }
        public void RPC(in string name, in Player player, params object[] objs)
        {
            Action1(name, player, objs);
        }


        #region Methods

        #region Unique

        public void FireArcherToMas(byte fromIdx, byte toIdx) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.FireArcher, fromIdx, toIdx });
        public void FirePawnToMas(byte idx) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.FirePawn, idx });
        public void PutOutFirePawnToMas(byte idx) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.PutOutFirePawn, idx });
        public void SeedEnvToMaster(byte idxCell, EnvironmentTypes env) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.Seed, idxCell, env });
        public void ChangeCornerArchToMas(byte idxCell) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.ChangeCornerArcher, idxCell });
        public void DestroyBuildingToMaster(byte idx) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.DestroyBuilding, idx });
        public void ExecuteAbilityOnCell_ToMaster(in byte idxCell, in AbilityTypes ability) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, ability, idxCell });


        //King
        public void CircularAttackKingToMaster(byte idxCell) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.CircularAttack, idxCell });


        //PawnAxe
        public void BuildFarmToMaster(in byte idx) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.SetFarm, idx });


        //Elfemale
        public void StunElfemaleToMas(byte fromIdx, byte toIdx) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.StunElfemale, fromIdx, toIdx });
        public void GrowAdultForest(byte idx) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.GrowAdultForest, idx });


        //Snowy
        public void IncreaseWindSnowy_ToMaster(in byte idxCell) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.IncreaseWindSnowy, idxCell });
        public void DecreaseWindSnowy_ToMaster(in byte idxCell) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.DecreaseWindSnowy, idxCell });



        public void PutOutFireElffToMas(byte fromIdx, byte toIdx) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.ChangeDirectionWind, fromIdx, toIdx });


        public void ResurrectToMaster(in byte idx_from, in byte idx_to) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.Resurrect, idx_from, idx_to });
        public void SetTeleportToMaster(in byte idx) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.SetTeleport, idx });
        public void TeleportToMaster(in byte idx) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.Teleport, idx });
        public void InvokeSkeletonsToMaster(in byte idx) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.InvokeSkeletons, idx });

        #endregion



        //public void Melt_ToMaster() => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.Melt });
        //public void BuyResource_ToMaster(in MarketBuyTypes marketBuy) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.MarketBuy, marketBuy });

        //public void DoneToMaster() => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.Done, });

        //public void GetHeroToMaster(UnitTypes unit) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.GetHero, unit });

        //public void TryShiftUnit_ToMaster(byte idx_from, byte idx_to) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.Shift, idx_from, idx_to });
        //public void TryAttackUnit_ToMaster(byte idx_from, byte idx_to) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.Attack, idx_from, idx_to });
        //public void ConditionUnitToMaster(in byte idx, ConditionUnitTypes cond) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.ConditionUnit, cond, idx });

        //public void GiveTakeToolWeaponToMaster(in byte idx, in ToolWeaponTypes tw, in LevelTypes level) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.GiveTakeToolWeapon, idx, tw, level });

        //public void TrySetUnit_ToMaster(byte idxCell, UnitTypes unitType) => Action0(MasterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.SetUnit, idxCell, unitType });

        #endregion


        #region General

        public void MistakeEconomyToGeneral(Player playerTo, Dictionary<ResourceTypes, float> needRes)
        {
            var needRes2 = new float[(int)ResourceTypes.End];
            needRes2[0] = needRes[ResourceTypes.Food];
            needRes2[1] = needRes[ResourceTypes.Wood];
            needRes2[2] = needRes[ResourceTypes.Ore];
            needRes2[3] = needRes[ResourceTypes.Iron];
            needRes2[4] = needRes[ResourceTypes.Gold];

            Action1(GeneralRPCName, playerTo, new object[] { RpcGeneralTypes.Mistake, MistakeTypes.Economy, needRes2 });
        }
        public void SimpleMistake_ToGeneral(MistakeTypes mistakeType, Player playerTo) => Action1(GeneralRPCName, playerTo, new object[] { RpcGeneralTypes.Mistake, mistakeType });



        public void ActiveMotionZone_ToGeneneral(Player player) => Action1(GeneralRPCName, player, new object[] { RpcGeneralTypes.ActiveMotion });
        public void ActiveMotionZone_ToGeneneral(in RpcTarget rpcTarget) => Action0(GeneralRPCName, rpcTarget, new object[] { RpcGeneralTypes.ActiveMotion });

        public void SoundToGeneral(RpcTarget rpcTarget, ClipTypes soundEffectType) => Action0(GeneralRPCName, rpcTarget, new object[] { RpcGeneralTypes.SoundEffect, soundEffectType });
        public void SoundToGeneral(RpcTarget rpcTarget, AbilityTypes uniq) => Action0(GeneralRPCName, rpcTarget, new object[] { RpcGeneralTypes.SoundUniqueAbility, uniq });
        public void SoundToGeneral(Player playerTo, ClipTypes eff) => Action1(GeneralRPCName, playerTo, new object[] { RpcGeneralTypes.SoundEffect, eff });
        public void SoundToGeneral(in RpcMasterTypes rpc, Player playerTo) => Action1(GeneralRPCName, playerTo, new object[] { RpcGeneralTypes.SoundEffect, rpc });
        public void SoundToGeneral(Player playerTo, AbilityTypes uniq) => Action1(GeneralRPCName, playerTo, new object[] { RpcGeneralTypes.SoundUniqueAbility, uniq });


        public void AnimationCell_ToGeneral(in byte cellIdx, in AnimationCellTypes animationCellT, in RpcTarget rpcTarget) => Action0(GeneralRPCName, rpcTarget, new object[] { RpcGeneralTypes.AnimationCell, cellIdx, animationCellT });

        #endregion
    }
}
