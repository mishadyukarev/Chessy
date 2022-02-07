//using ECS;
//using System.Collections.Generic;

//namespace Game.Game
//{
//    public readonly struct MasterEs
//    {
//        static Entity _else;
//        static Dictionary<RpcMasterTypes, Entity> _rpcEnts;
//        static Dictionary<AbilityTypes, Entity> _uniqEnts;


//        public MasterEs(in EcsWorld gameW)
//        {
//            _rpcEnts = new Dictionary<RpcMasterTypes, Entity>();
//            _uniqEnts = new Dictionary<AbilityTypes, Entity>();


//            _else = gameW.NewEntity()
//                .Add(new AbilityTC());

//            _rpcEnts.Add(RpcMasterTypes.ConditionUnit, gameW.NewEntity()
//                .Add(new ConditionUnitTC())
//                .Add(new IdxC()));

//            _rpcEnts.Add(RpcMasterTypes.BuyRes, gameW.NewEntity()
//                .Add(new ResourceTC()));

//            _rpcEnts.Add(RpcMasterTypes.Shift, gameW.NewEntity()
//                .Add(new IdxFromToC()));

//            _rpcEnts.Add(RpcMasterTypes.SetUnit, gameW.NewEntity()
//                .Add(new IdxC())
//                .Add(new UnitTC()));

//            _rpcEnts.Add(RpcMasterTypes.UpgCenterUnits, gameW.NewEntity()
//                .Add(new UnitTC()));

//            _rpcEnts.Add(RpcMasterTypes.UpgCenterBuild, gameW.NewEntity()
//                .Add(new BuildingTC()));

//            _rpcEnts.Add(RpcMasterTypes.GetHero, gameW.NewEntity()
//                .Add(new UnitTC()));

//            _rpcEnts.Add(RpcMasterTypes.CreateUnit, gameW.NewEntity()
//                .Add(new UnitTC()));

//            _rpcEnts.Add(RpcMasterTypes.Attack, gameW.NewEntity()
//                .Add(new IdxFromToC()));

//            _rpcEnts.Add(RpcMasterTypes.GiveTakeToolWeapon, gameW.NewEntity()
//                .Add(new ToolWeaponTC())
//                .Add(new LevelTC())
//                .Add(new IdxC()));

//            _rpcEnts.Add(RpcMasterTypes.UpgradeCellUnit, gameW.NewEntity()
//                .Add(new IdxC()));


//            _uniqEnts.Add(AbilityTypes.FireArcher, gameW.NewEntity()
//                .Add(new IdxFromToC()));

//            _uniqEnts.Add(AbilityTypes.ChangeDirectionWind, gameW.NewEntity()
//                .Add(new IdxFromToC()));

//            _uniqEnts.Add(AbilityTypes.StunElfemale, gameW.NewEntity()
//                .Add(new IdxFromToC()));
//        }
//    }
//}