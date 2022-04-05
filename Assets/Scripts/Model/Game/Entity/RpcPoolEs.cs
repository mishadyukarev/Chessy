﻿using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct RpcPoolEs
    {
        int _idx_cur;

        readonly ActionMy<string, RpcTarget, object[]> _action0;
        readonly ActionMy<string, Player, object[]> _action1;

        readonly string _masterRPCName;
        readonly string _generalRPCName;
        readonly string _otherRPCName;

        public RpcPoolEs(in List<object> actions, in List<string> names) : this()
        {
            _idx_cur = 0;

            _action0 = (ActionMy<string, RpcTarget, object[]>)actions[_idx_cur++];
            _action1 = (ActionMy<string, Player, object[]>)actions[_idx_cur++];

            _idx_cur = 0;
            _masterRPCName = names[_idx_cur++];
            _generalRPCName = names[_idx_cur++];
            _otherRPCName = names[_idx_cur++];

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
            _action0(name, rpcTarget, objs);
        }
        public void RPC(in string name, in Player player, params object[] objs)
        {
            _action1(name, player, objs);
        }


        #region Methods

        #region Unique

        public void FireArcherToMas(byte fromIdx, byte toIdx) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.FireArcher, fromIdx, toIdx });
        public void FirePawnToMas(byte idx) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.FirePawn, idx });
        public void PutOutFirePawnToMas(byte idx) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.PutOutFirePawn, idx });
        public void SeedEnvToMaster(byte idxCell, EnvironmentTypes env) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.Seed, idxCell, env });
        public void ChangeCornerArchToMas(byte idxCell) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.ChangeCornerArcher, idxCell });
        public void DestroyBuildingToMaster(byte idx) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.DestroyBuilding, idx });
        public void ExecuteAbilityOnCell_ToMaster(in byte idxCell, in AbilityTypes ability) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, ability, idxCell });


        //King
        public void CircularAttackKingToMaster(byte idxCell) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.CircularAttack, idxCell });


        //PawnAxe
        public void BuildFarmToMaster(in byte idx) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.SetFarm, idx });


        //Elfemale
        public void StunElfemaleToMas(byte fromIdx, byte toIdx) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.StunElfemale, fromIdx, toIdx });
        public void GrowAdultForest(byte idx) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.GrowAdultForest, idx });


        //Snowy
        public void IncreaseWindSnowy_ToMaster(in byte idxCell) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.IncreaseWindSnowy, idxCell });
        public void DecreaseWindSnowy_ToMaster(in byte idxCell) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.DecreaseWindSnowy, idxCell });



        public void PutOutFireElffToMas(byte fromIdx, byte toIdx) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.ChangeDirectionWind, fromIdx, toIdx });


        public void ResurrectToMaster(in byte idx_from, in byte idx_to) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.Resurrect, idx_from, idx_to });
        public void SetTeleportToMaster(in byte idx) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.SetTeleport, idx });
        public void TeleportToMaster(in byte idx) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.Teleport, idx });
        public void InvokeSkeletonsToMaster(in byte idx) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.InvokeSkeletons, idx });

        #endregion



        #region CityBuild

        public void CityBuyBuildingToMaster(in BuildingTypes buildT) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.BuyBuilding, buildT });

        #endregion


        public void Melt_ToMaster() => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.Melt });
        public void BuyResource_ToMaster(in MarketBuyTypes marketBuy) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.MarketBuy, marketBuy });

        public void ReadyToMaster() => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.Ready });

        public void DoneToMaster() => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.Done, });

        public void GetHeroToMaster(UnitTypes unit) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.GetHero, unit });

        public void TryShiftUnit_ToMaster(byte idx_from, byte idx_to) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.Shift, idx_from, idx_to });
        public void TryAttackUnit_ToMaster(byte idx_from, byte idx_to) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.Attack, idx_from, idx_to });
        public void ConditionUnitToMaster(in byte idx, ConditionUnitTypes cond) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.ConditionUnit, cond, idx });

        public void GiveTakeToolWeaponToMaster(in byte idx, in ToolWeaponTypes tw, in LevelTypes level) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.GiveTakeToolWeapon, idx, tw, level });

        public void TrySetUnit_ToMaster(byte idxCell, UnitTypes unitType) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.SetUnit, idxCell, unitType });

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

            _action1(_generalRPCName, playerTo, new object[] { RpcGeneralTypes.Mistake, MistakeTypes.Economy, needRes2 });
        }
        public void SimpleMistake_ToGeneral(MistakeTypes mistakeType, Player playerTo) => _action1(_generalRPCName, playerTo, new object[] { RpcGeneralTypes.Mistake, mistakeType });



        public void ActiveMotionZone_ToGeneneral(Player player) => _action1(_generalRPCName, player, new object[] { RpcGeneralTypes.ActiveMotion });
        public void ActiveMotionZone_ToGeneneral(in RpcTarget rpcTarget) => _action0(_generalRPCName, rpcTarget, new object[] { RpcGeneralTypes.ActiveMotion });

        public void SoundToGeneral(RpcTarget rpcTarget, ClipTypes soundEffectType) => _action0(_generalRPCName, rpcTarget, new object[] { RpcGeneralTypes.SoundEffect, soundEffectType });
        public void SoundToGeneral(RpcTarget rpcTarget, AbilityTypes uniq) => _action0(_generalRPCName, rpcTarget, new object[] { RpcGeneralTypes.SoundUniqueAbility, uniq });
        public void SoundToGeneral(Player playerTo, ClipTypes eff) => _action1(_generalRPCName, playerTo, new object[] { RpcGeneralTypes.SoundEffect, eff });
        public void SoundToGeneral(in RpcMasterTypes rpc, Player playerTo) => _action1(_generalRPCName, playerTo, new object[] { RpcGeneralTypes.SoundEffect, rpc });
        public void SoundToGeneral(Player playerTo, AbilityTypes uniq) => _action1(_generalRPCName, playerTo, new object[] { RpcGeneralTypes.SoundUniqueAbility, uniq });

        #endregion
    }
}
