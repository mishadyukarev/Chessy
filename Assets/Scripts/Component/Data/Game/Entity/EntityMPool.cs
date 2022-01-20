using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct EntityMPool
    {
        static Entity _else;
        static Dictionary<RpcMasterTypes, Entity> _rpcEnts;
        static Dictionary<UniqueAbilityTypes, Entity> _uniqEnts;

        public static ref UniqueAbilityC UniqueAbilityC => ref _else.Get<UniqueAbilityC>();

        public static ref C Build<C>() where C : struct => ref _rpcEnts[RpcMasterTypes.Build].Get<C>();
        public static ref C ConditionUnit<C>() where C : struct => ref _rpcEnts[RpcMasterTypes.ConditionUnit].Get<C>();
        public static ref C BuyResources<C>() where C : struct => ref _rpcEnts[RpcMasterTypes.BuyRes].Get<C>();
        public static ref C Shift<C>() where C : struct => ref _rpcEnts[RpcMasterTypes.Shift].Get<C>();
        public static ref C SetUnit<C>() where C : struct => ref _rpcEnts[RpcMasterTypes.SetUnit].Get<C>();
        public static ref C UpgradeCenterUnit<C>() where C : struct => ref _rpcEnts[RpcMasterTypes.UpgCenterUnits].Get<C>();
        public static ref C UpgradeCenterBuild<C>() where C : struct => ref _rpcEnts[RpcMasterTypes.UpgCenterBuild].Get<C>();
        public static ref UnitTC ForGetHero => ref _rpcEnts[RpcMasterTypes.GetHero].Get<UnitTC>();
        public static ref C CreateUnit<C>() where C : struct => ref _rpcEnts[RpcMasterTypes.CreateUnit].Get<C>();
        public static ref IdxFromToC Attack => ref _rpcEnts[RpcMasterTypes.Attack].Get<IdxFromToC>();
        public static ref C CreateHeroFromTo<C>() where C : struct => ref _rpcEnts[RpcMasterTypes.CreateHeroFromTo].Get<C>();
        public static ref C GiveTakeToolWeapon<C>() where C : struct => ref _rpcEnts[RpcMasterTypes.GiveTakeToolWeapon].Get<C>();
        public static ref C UpgradeUnit<C>() where C : struct => ref _rpcEnts[RpcMasterTypes.UpgradeCellUnit].Get<C>();
        public static ref C ScoutOldNew<C>() where C : struct => ref _rpcEnts[RpcMasterTypes.ToNewUnit].Get<C>();
        public static ref IdxC DestroyIdxC => ref _rpcEnts[RpcMasterTypes.DestroyBuild].Get<IdxC>();

        public static ref C Seed<C>() where C : struct => ref _uniqEnts[UniqueAbilityTypes.Seed].Get<C>();
        public static ref C GrowAdultForest<C>() where C : struct => ref _uniqEnts[UniqueAbilityTypes.GrowAdultForest].Get<C>();
        public static ref C FireArcher<C>() where C : struct => ref _uniqEnts[UniqueAbilityTypes.FireArcher].Get<C>();
        public static ref C ChangeDirectionWind<C>() where C : struct => ref _uniqEnts[UniqueAbilityTypes.ChangeDirectionWind].Get<C>();
        public static ref C StunElfemale<C>() where C : struct => ref _uniqEnts[UniqueAbilityTypes.StunElfemale].Get<C>();


        public EntityMPool(in EcsWorld gameW)
        {
            _rpcEnts = new Dictionary<RpcMasterTypes, Entity>();
            _uniqEnts = new Dictionary<UniqueAbilityTypes, Entity>();


            _else = gameW.NewEntity()
                .Add(new UniqueAbilityC());


            _rpcEnts.Add(RpcMasterTypes.Build, gameW.NewEntity()
                .Add(new BuildingTC())
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

            _rpcEnts.Add(RpcMasterTypes.UpgCenterUnits, gameW.NewEntity()
                .Add(new UnitTC()));

            _rpcEnts.Add(RpcMasterTypes.UpgCenterBuild, gameW.NewEntity()
                .Add(new BuildingTC()));

            _rpcEnts.Add(RpcMasterTypes.GetHero, gameW.NewEntity()
                .Add(new UnitTC()));

            _rpcEnts.Add(RpcMasterTypes.CreateUnit, gameW.NewEntity()
                .Add(new UnitTC()));

            _rpcEnts.Add(RpcMasterTypes.Attack, gameW.NewEntity()
                .Add(new IdxFromToC()));

            _rpcEnts.Add(RpcMasterTypes.CreateHeroFromTo, gameW.NewEntity()
                .Add(new UnitTC())
                .Add(new IdxFromToC()));

            _rpcEnts.Add(RpcMasterTypes.GiveTakeToolWeapon, gameW.NewEntity()
                .Add(new ToolWeaponC())
                .Add(new LevelTC())
                .Add(new IdxC()));

            _rpcEnts.Add(RpcMasterTypes.UpgradeCellUnit, gameW.NewEntity()
                .Add(new IdxC()));

            _rpcEnts.Add(RpcMasterTypes.ToNewUnit, gameW.NewEntity()
                .Add(new UnitTC())
                .Add(new IdxC()));

            _rpcEnts.Add(RpcMasterTypes.DestroyBuild, gameW.NewEntity()
                .Add(new IdxC()));




            _uniqEnts.Add(UniqueAbilityTypes.Seed, gameW.NewEntity()
                .Add(new EnvironmetC())
                .Add(new IdxC()));

            _uniqEnts.Add(UniqueAbilityTypes.GrowAdultForest, gameW.NewEntity()
                .Add(new IdxC()));

            _uniqEnts.Add(UniqueAbilityTypes.FireArcher, gameW.NewEntity()
                .Add(new IdxFromToC()));

            _uniqEnts.Add(UniqueAbilityTypes.ChangeDirectionWind, gameW.NewEntity()
                .Add(new IdxFromToC()));

            _uniqEnts.Add(UniqueAbilityTypes.StunElfemale, gameW.NewEntity()
                .Add(new IdxFromToC()));
        }
    }
}