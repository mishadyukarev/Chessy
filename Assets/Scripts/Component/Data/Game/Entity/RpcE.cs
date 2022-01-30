using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public delegate void Action<in T1, in T2, in T3>(T1 arg1, T2 arg2, params T3[] arg3);

    public sealed class RpcE
    {
        int _idx_cur;

        readonly Action<string, RpcTarget, object[]> _action0;
        readonly Action<string, Player, object[]> _action1;

        readonly string _masterRPCName;
        readonly string _generalRPCName;
        readonly string _otherRPCName;

        public RpcE(in List<object> actions, in List<string> names)
        {
            _idx_cur = 0;

            _action0 = (Action<string, RpcTarget, object[]>)actions[_idx_cur++];
            _action1 = (Action<string, Player, object[]>)actions[_idx_cur++];

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
            InfoC.AddInfo(MGOTypes.Other, infoFrom);

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

        public void BonusNearUnits(byte idxCell) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.BonusNear, idxCell });

        public void StunElfemaleToMas(byte fromIdx, byte toIdx) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.StunElfemale, fromIdx, toIdx });

        public void GrowAdultForest(byte idx) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.GrowAdultForest, idx });
        public void PutOutFireElffToMas(byte fromIdx, byte toIdx) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.ChangeDirectionWind, fromIdx, toIdx });

        public void CircularAttackKingToMaster(byte idxCell) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.CircularAttack, idxCell });
        public void IceWallToMaster(in byte idx) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UniqueAbility, AbilityTypes.IceWall, idx });

        #endregion


        #region Upgrades

        public void PickUpgUnitToMas(UnitTypes unit) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UpgCenterUnits, unit });
        public void PickUpgBuildToMas(BuildingTypes build) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UpgCenterBuild, build });
        public void UpgWater() => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UpgWater });

        #endregion


        public void ReadyToMaster() => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.Ready });

        public void DoneToMaster() => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.Done, });

        public void BuyResToMaster(ResourceTypes res) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.BuyRes, res });

        public void GetHeroToMaster(UnitTypes unit) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.GetHero, unit });

        public void ShiftUnitToMaster(byte idx_from, byte idx_to) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.Shift, idx_from, idx_to });
        public void AttackUnitToMaster(byte idx_from, byte idx_to) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.Attack, idx_from, idx_to });

        public void BuildToMaster(in byte idx, in BuildingTypes build) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.Build, idx, build });
        public void DestroyBuildingToMaster(byte xyCellForDestroy) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.DestroyBuild, xyCellForDestroy });

        public void ConditionUnitToMaster(ConditionUnitTypes neededCondtionType, byte idxCell) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.ConditionUnit, neededCondtionType, idxCell });

        public void FromNewUnitToMas(UnitTypes unitType, byte idxCell) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.ToNewUnit, unitType, idxCell });
        public void FromToNewUnitToMas(UnitTypes unitType, byte idxFrom, byte idxTo) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.CreateHeroFromTo, unitType, idxFrom, idxTo });
        public void UpgradeUnitToMaster(byte idxCell) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.UpgradeCellUnit, idxCell });
        public void GiveTakeToolWeaponToMaster(ToolWeaponTypes tw, LevelTypes level, byte idx) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.GiveTakeToolWeapon, tw, level, idx });

        public void CreateUnitToMaster(UnitTypes unitType) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.CreateUnit, unitType });

        public void MeltOreToMaster() => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.MeltOre, });

        public void SetUniToMaster(byte idxCell, UnitTypes unitType) => _action0(_masterRPCName, RpcTarget.MasterClient, new object[] { RpcMasterTypes.SetUnit, idxCell, unitType });

        #endregion


        #region General

        public void MistakeEconomyToGeneral(Player playerTo, Dictionary<ResourceTypes, int> needRes)
        {
            int[] needRes2 = new int[(int)ResourceTypes.End];
            needRes2[0] = needRes[ResourceTypes.Food];
            needRes2[1] = needRes[ResourceTypes.Wood];
            needRes2[2] = needRes[ResourceTypes.Ore];
            needRes2[3] = needRes[ResourceTypes.Iron];
            needRes2[4] = needRes[ResourceTypes.Gold];

            _action1(_generalRPCName, playerTo, new object[] { RpcGeneralTypes.Mistake, MistakeTypes.Economy, needRes2 });
        }
        public void SimpleMistakeToGeneral(MistakeTypes mistakeType, Player playerTo) => _action1(_generalRPCName, playerTo, new object[] { RpcGeneralTypes.Mistake, mistakeType });



        public void ActiveMotionZoneToGen(Player player) => _action1(_generalRPCName, player, new object[] { RpcGeneralTypes.ActiveMotion, });

        public void SoundToGeneral(RpcTarget rpcTarget, ClipTypes soundEffectType) => _action0(_generalRPCName, rpcTarget, new object[] { RpcGeneralTypes.SoundEff, soundEffectType });
        public void SoundToGeneral(RpcTarget rpcTarget, AbilityTypes uniq) => _action0(_generalRPCName, rpcTarget, new object[] { RpcGeneralTypes.SoundUniqueAbility, uniq });
        public void SoundToGeneral(Player playerTo, ClipTypes eff) => _action1(_generalRPCName, playerTo, new object[] { RpcGeneralTypes.SoundEff, eff });
        public void SoundToGeneral(in RpcMasterTypes rpc, Player playerTo) => _action1(_generalRPCName, playerTo, new object[] { RpcGeneralTypes.SoundEff, rpc });
        public void SoundToGeneral(Player playerTo, AbilityTypes uniq) => _action1(_generalRPCName, playerTo, new object[] { RpcGeneralTypes.SoundUniqueAbility, uniq });

        #endregion
    }
}
