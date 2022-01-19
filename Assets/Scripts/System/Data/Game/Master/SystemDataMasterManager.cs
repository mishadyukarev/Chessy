using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct SystemDataMasterManager
    {
        static Dictionary<SystemDataMasterTypes, Action> _systems;
        static Dictionary<RpcMasterTypes, Action> _rpcSysts;
        static Dictionary<UniqueAbilityTypes, Action> _uniqAbil;


        public SystemDataMasterManager(in bool def)
        {
            _systems = new Dictionary<SystemDataMasterTypes, Action>();
            _rpcSysts = new Dictionary<RpcMasterTypes, Action>();
            _uniqAbil = new Dictionary<UniqueAbilityTypes, Action>();


            var action =
                (Action)new UpdatorMS().Run
                + new ExtractBuildUpdMS().Run
                + new UpdateFireMS().Run
                + new CloudUpdMS().Run

                + new UpdateExtractUnitMS().Run
                + new ResumeUnitUpdMS().Run
                + new UpdateHealingUnitMS().Run
                + new UpdateHungryMS().Run
                + new ThirstyUpdMS().Run

                + new UpdateAnimalsMS().Run;
            _systems.Add(SystemDataMasterTypes.UpdateMove, action);

            action = new TruceMS().Run;
            _systems.Add(SystemDataMasterTypes.Truce, action);



            _rpcSysts.Add(RpcMasterTypes.Build, 
                (Action)new BuildMineMS().Run
                + new BuildFarmMS().Run
                + new CityBuildMS().Run);
            _rpcSysts.Add(RpcMasterTypes.DestroyBuild, new DestroyMS().Run);
            _rpcSysts.Add(RpcMasterTypes.Shift, new ShiftUnitMS().Run);
            _rpcSysts.Add(RpcMasterTypes.Attack, new AttackMS().Run);
            _rpcSysts.Add(RpcMasterTypes.ConditionUnit, new ConditionMS().Run);
            _rpcSysts.Add(RpcMasterTypes.Ready, new ReadyMS().Run);
            _rpcSysts.Add(RpcMasterTypes.Done, new DonerMS().Run);
            _rpcSysts.Add(RpcMasterTypes.CreateUnit, new CreateUnitMS().Run);
            _rpcSysts.Add(RpcMasterTypes.MeltOre, new MeltOreMS().Run);
            _rpcSysts.Add(RpcMasterTypes.SetUnit, new SetterUnitMS().Run);
            _rpcSysts.Add(RpcMasterTypes.BuyRes, new BuyResourcesMS().Run);
            _rpcSysts.Add(RpcMasterTypes.UpgradeCellUnit, new UpgradeUnitMS().Run);
            _rpcSysts.Add(RpcMasterTypes.ToNewUnit, new ScoutOldNewSys().Run);
            _rpcSysts.Add(RpcMasterTypes.GiveTakeToolWeapon, new GiveTakeToolWeaponMS().Run);
            _rpcSysts.Add(RpcMasterTypes.GetHero, new GetHeroMS().Run);
            _rpcSysts.Add(RpcMasterTypes.CreateHeroFromTo, new FromToNewUnitMS().Run);
            _rpcSysts.Add(RpcMasterTypes.UpgCenterUnits, new PickCenterUpgradeUnitsMS().Run);
            _rpcSysts.Add(RpcMasterTypes.UpgCenterBuild, new PickCenterUpgradeBuildsMS().Run);
            _rpcSysts.Add(RpcMasterTypes.UpgWater, new CenterUpgradeUnitWaterMS().Run);


            _uniqAbil.Add(UniqueAbilityTypes.Seed, new SeedingMS().Run);
            _uniqAbil.Add(UniqueAbilityTypes.StunElfemale, new StunElfemaleMS().Run);
            _uniqAbil.Add(UniqueAbilityTypes.FirePawn, new FirePawnMS().Run);
            _uniqAbil.Add(UniqueAbilityTypes.PutOutFirePawn, new PutOutFireMS().Run);
            _uniqAbil.Add(UniqueAbilityTypes.FireArcher, new FireArcherMS().Run);
            _uniqAbil.Add(UniqueAbilityTypes.GrowAdultForest, new GrowAdultForestMS().Run);
            _uniqAbil.Add(UniqueAbilityTypes.CircularAttack, new CircularAttackKingMS().Run);
            _uniqAbil.Add(UniqueAbilityTypes.BonusNear, new BonusNearUnitKingMS().Run);
            _uniqAbil.Add(UniqueAbilityTypes.ChangeDirectionWind, new ChangeDirectionWindElfemaleMS().Run);
            _uniqAbil.Add(UniqueAbilityTypes.ChangeCornerArcher, new ChangeCornerArcherMS().Run);
        }

        public static void InvokeRun(SystemDataMasterTypes mastDataSys) => _systems[mastDataSys].Invoke();
        public static void InvokeRun(RpcMasterTypes rpc)
        {
            if (_rpcSysts.ContainsKey(rpc)) _rpcSysts[rpc].Invoke();
            else throw new System.Exception();
        }
        public static void InvokeRun(UniqueAbilityTypes uniqAbil) => _uniqAbil[uniqAbil].Invoke();
    }
}