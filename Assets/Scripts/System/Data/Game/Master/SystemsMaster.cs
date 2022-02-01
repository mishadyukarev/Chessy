using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct SystemsMaster
    {
        readonly Dictionary<SystemDataMasterTypes, Action> _systems;
        readonly Dictionary<RpcMasterTypes, Action> _rpcSysts;
        readonly Dictionary<AbilityTypes, Action> _uniqAbil;


        public SystemsMaster(in Entities ents)
        {
            _systems = new Dictionary<SystemDataMasterTypes, Action>();
            _rpcSysts = new Dictionary<RpcMasterTypes, Action>();
            _uniqAbil = new Dictionary<AbilityTypes, Action>();


            var action =
                (Action)new UpdatorMS(this, ents).Run

                + new UpdExtractWoodcutterMS(ents).Run
                + new UpdExtractFarmMS(ents).Run
                + new UpdExtractMineMS(ents).Run

                + new UpdateFireMS(ents).Run
                + new CloudUpdMS(ents).Run
                + new UpdateIceWallMS(ents).Run

                + new UpdateExtractUnitMS(ents).Run
                + new ResumeUnitUpdMS(ents).Run
                + new UpdateHealingUnitMS(ents).Run
                + new UpdateHungryMS(ents).Run
                + new UpdateThirstyMS(ents).Run
                + new UpdGiveWaterSnowyMS(ents).Run

                + new UpdateCamelShiftMS(ents).Run
                + new UpdateSpawnCamelMS(ents).Run;
            _systems.Add(SystemDataMasterTypes.UpdateMove, action);

            action = new TruceMS(ents).Run;
            _systems.Add(SystemDataMasterTypes.Truce, action);


            _rpcSysts.Add(RpcMasterTypes.DestroyBuild, new DestroyMS(ents).Run);
            _rpcSysts.Add(RpcMasterTypes.Shift, new ShiftUnitMS(ents).Run);
            _rpcSysts.Add(RpcMasterTypes.Attack, new AttackMS(ents).Run);
            _rpcSysts.Add(RpcMasterTypes.ConditionUnit, new ConditionMS(ents).Run);
            _rpcSysts.Add(RpcMasterTypes.Ready, new ReadyMS(ents).Run);
            _rpcSysts.Add(RpcMasterTypes.Done, new DonerMS(this, ents).Run);
            _rpcSysts.Add(RpcMasterTypes.CreateUnit, new CreateUnitMS(ents).Run);
            _rpcSysts.Add(RpcMasterTypes.MeltOre, new MeltOreMS(ents).Run);
            _rpcSysts.Add(RpcMasterTypes.SetUnit, new SetterUnitMS(ents).Run);
            _rpcSysts.Add(RpcMasterTypes.BuyRes, new BuyResourcesMS(ents).Run);
            _rpcSysts.Add(RpcMasterTypes.UpgradeCellUnit, new UpgradeUnitMS(ents).Run);
            _rpcSysts.Add(RpcMasterTypes.ToNewUnit, new ScoutOldNewMS(ents).Run);
            _rpcSysts.Add(RpcMasterTypes.GiveTakeToolWeapon, new GiveTakeToolWeaponMS(ents).Run);
            _rpcSysts.Add(RpcMasterTypes.GetHero, new GetHeroMS(ents).Run);
            _rpcSysts.Add(RpcMasterTypes.CreateHeroFromTo, new FromToNewUnitMS(ents).Run);
            _rpcSysts.Add(RpcMasterTypes.UpgCenterUnits, new PickCenterUpgradeUnitsMS(ents).Run);
            _rpcSysts.Add(RpcMasterTypes.UpgCenterBuild, new PickCenterUpgradeBuildsMS(ents).Run);
            _rpcSysts.Add(RpcMasterTypes.UpgWater, new CenterUpgradeUnitWaterMS(ents).Run);


            _uniqAbil.Add(AbilityTypes.Seed, new SeedingMS(ents).Run);
            _uniqAbil.Add(AbilityTypes.StunElfemale, new StunElfemaleMS(ents).Run);

            _uniqAbil.Add(AbilityTypes.FirePawn, new FirePawnMS(ents).Run);
            _uniqAbil.Add(AbilityTypes.PutOutFirePawn, new PutOutFireMS(ents).Run);
            _uniqAbil.Add(AbilityTypes.Farm, new BuildFarmMS(ents).Run);
            _uniqAbil.Add(AbilityTypes.Mine, new BuildMineMS(ents).Run);
            _uniqAbil.Add(AbilityTypes.City, new CityBuildMS(ents).Run);

            _uniqAbil.Add(AbilityTypes.FireArcher, new FireArcherMS(ents).Run);
            _uniqAbil.Add(AbilityTypes.GrowAdultForest, new GrowAdultForestMS(ents).Run);
            _uniqAbil.Add(AbilityTypes.CircularAttack, new CircularAttackKingMS(ents).Run);
            _uniqAbil.Add(AbilityTypes.BonusNear, new BonusNearUnitKingMS(ents).Run);
            _uniqAbil.Add(AbilityTypes.ChangeDirectionWind, new ChangeDirectionWindElfemaleMS(ents).Run);
            _uniqAbil.Add(AbilityTypes.ChangeCornerArcher, new ChangeCornerArcherMS(ents).Run);

            _uniqAbil.Add(AbilityTypes.IceWall, new IceWallMS(ents).Run);
            _uniqAbil.Add(AbilityTypes.ActiveIceWall, new ActiveIceWallMS(ents).Run);
        }

        public void InvokeRun(SystemDataMasterTypes mastDataSys) => _systems[mastDataSys].Invoke();
        public void InvokeRun(RpcMasterTypes rpc)
        {
            if (_rpcSysts.ContainsKey(rpc)) _rpcSysts[rpc].Invoke();
            else throw new System.Exception();
        }
        public void InvokeRun(AbilityTypes uniqAbil) => _uniqAbil[uniqAbil].Invoke();
    }
}