using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct EntityMPool
    {
        static Dictionary<RpcMasterTypes, Entity> _rpcEnts;
        static Dictionary<UniqueAbilityTypes, Entity> _uniqEnts;

        public static ref C Build<C>() where C : struct => ref _rpcEnts[RpcMasterTypes.Build].Get<C>();
        public static ref C ConditionUnit<C>() where C : struct => ref _rpcEnts[RpcMasterTypes.ConditionUnit].Get<C>();
        public static ref C BuyResources<C>() where C : struct => ref _rpcEnts[RpcMasterTypes.BuyRes].Get<C>();
        public static ref C Shift<C>() where C : struct => ref _rpcEnts[RpcMasterTypes.Shift].Get<C>();
        public static ref C SetUnit<C>() where C : struct => ref _rpcEnts[RpcMasterTypes.SetUnit].Get<C>();

        public static ref C Seed<C>() where C : struct => ref _uniqEnts[UniqueAbilityTypes.Seed].Get<C>();
        public static ref C GrowAdultForest<C>() where C : struct => ref _uniqEnts[UniqueAbilityTypes.GrowAdultForest].Get<C>();


        public EntityMPool(in EcsWorld gameW)
        {
            _rpcEnts = new Dictionary<RpcMasterTypes, Entity>();
            _uniqEnts = new Dictionary<UniqueAbilityTypes, Entity>();



            _rpcEnts.Add(RpcMasterTypes.Build, gameW.NewEntity()
                .Add(new BuildingC())
                .Add(new IdxC()));

            _rpcEnts.Add(RpcMasterTypes.ConditionUnit, gameW.NewEntity()
                .Add(new ConditionUnitC())
                .Add(new IdxC()));

            _rpcEnts.Add(RpcMasterTypes.BuyRes, gameW.NewEntity()
                .Add(new ResourceTypeC()));

            _rpcEnts.Add(RpcMasterTypes.Shift, gameW.NewEntity()
                .Add(new IdxFromToC()));

            _rpcEnts.Add(RpcMasterTypes.SetUnit, gameW.NewEntity()
                .Add(new IdxC())
                .Add(new UnitTC()));



            _uniqEnts.Add(UniqueAbilityTypes.Seed, gameW.NewEntity()
                .Add(new EnvironmetC())
                .Add(new IdxC()));

            _uniqEnts.Add(UniqueAbilityTypes.GrowAdultForest, gameW.NewEntity()
                .Add(new IdxC()));
        }
    }
}