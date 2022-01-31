using Game.Common;
using Photon.Pun;

namespace Game.Game
{
    sealed class UpdatorMS : SystemCellAbstract, IEcsRunSystem
    {
        readonly SystemsMaster _systemsMaster;

        internal UpdatorMS(in SystemsMaster systemsMaster, in Entities ents) : base(ents)
        {
            _systemsMaster = systemsMaster;
        }

        public void Run()
        {
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                Es.ScoutHeroCooldownE(UnitTypes.Scout, player).Cooldown.Amount -= 1;
                Es.ScoutHeroCooldownE(UnitTypes.Elfemale, player).Cooldown.Amount -= 1;

                Es.InventorResourcesEs.Resource(ResourceTypes.Food, player).Resources.Amount += EconomyValues.ADDING_FOOD_AFTER_MOVE;
            }

            foreach (byte idx_0 in CellEs.Idxs)
            {
                ref var cell_0 = ref CellEs.CellE(idx_0).InstanceIDC;

                var unit_0 = UnitEs.Main(idx_0).UnitTC;
                var ownUnit_0 = UnitEs.Main(idx_0).OwnerC;
                var hp_0 = UnitEs.StatEs.Hp(idx_0).Health;

                var buil_0 = BuildEs.BuildingE(idx_0).BuildTC;
                var ownBuil_0 = BuildEs.BuildingE(idx_0).Owner;
                ref var fire_0 = ref CellEs.FireEs.Fire(idx_0).Fire;

                foreach (var item in CellEs.TrailEs.Keys) CellEs.TrailEs.Trail(item, idx_0).Health.Amount--;
                foreach (var item in UnitEs.CooldownKeys) UnitEs.CooldownAbility(item, idx_0).TakeAfterUpdate();



                if (unit_0.Have && !unit_0.IsAnimal)
                {
                    //CellUnitStepsInConditionEs.Steps(condUnit_0.Condition, idx_0)++;

                    Es.InventorResourcesEs.Resource(ResourceTypes.Food, ownUnit_0.Player).Resources.Amount -= EconomyValues.CostFood(unit_0.Unit);

                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.Second))
                        {
                            UnitEs.StatEs.Hp(idx_0).Health.Amount = CellUnitHpValues.MAX_HP;
                        }
                    }


                    if (fire_0.Have)
                    {
                        UnitEs.Main(idx_0).ResetCondition();
                    }

                    else
                    {
                        if (UnitEs.Main(idx_0).ConditionTC.Is(ConditionUnitTypes.Protected))
                        {
                            if (UnitEs.StatEs.Hp(idx_0).HaveMax)
                            {
                                if (unit_0.Is(UnitTypes.Scout))
                                {
                                    if (buil_0.Is(BuildingTypes.Woodcutter) || !buil_0.Have)
                                    {
                                        if (GameModeC.IsGameMode(GameModes.TrainingOff))
                                        {
                                            if (ownUnit_0.Is(PlayerTypes.First))
                                            {
                                                if (Es.WhereBuildingEs.IsSetted(BuildingTypes.Camp, ownUnit_0.Player, out var idx_camp))
                                                {
                                                    BuildEs.BuildingE(idx_camp).Destroy(BuildEs, Es.WhereBuildingEs);
                                                }


                                                BuildEs.BuildingE(idx_0).SetNew(BuildingTypes.Camp, ownUnit_0.Player, BuildEs, Es.WhereBuildingEs);
                                                Es.WhereBuildingEs.HaveBuild(BuildingTypes.Camp, ownUnit_0.Player, idx_0).HaveBuilding.Have = true;
                                            }
                                        }
                                        else
                                        {
                                            if (Es.WhereBuildingEs.IsSetted(BuildingTypes.Camp, ownUnit_0.Player, out var idx_camp))
                                            {
                                                BuildEs.BuildingE(idx_camp).Destroy(BuildEs, Es.WhereBuildingEs);
                                            }

                                            BuildEs.BuildingE(idx_0).SetNew(BuildingTypes.Camp, ownUnit_0.Player, BuildEs, Es.WhereBuildingEs);
                                            Es.WhereBuildingEs.HaveBuild(BuildingTypes.Camp, ownUnit_0.Player, idx_0).HaveBuilding.Have = true;
                                        }
                                    }
                                }
                            }
                        }

                        else if (!UnitEs.Main(idx_0).ConditionTC.Is(ConditionUnitTypes.Relaxed))
                        {
                            if (UnitEs.StatEs.Step(idx_0).Steps.Have)
                            {
                                UnitEs.Main(idx_0).SetCondition(ConditionUnitTypes.Protected);
                            }
                        }
                    }
                    UnitEs.StatEs.Step(idx_0).SetMax(UnitEs.Main(idx_0));
                }
            }

            var amountAdultForest = 0;
            foreach (var idx in CellEs.Idxs)
            {
                if (Es.WhereEnviromentEs.Info(EnvironmentTypes.AdultForest, idx).HaveEnv.Have)
                    amountAdultForest += 1;
            }

            if (amountAdultForest <= 8)
            {
                Es.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.Truce);
                _systemsMaster.InvokeRun(SystemDataMasterTypes.Truce);
            }

            if (Es.Motion.AmountMotions.Amount % 3 == 0)
            {
                foreach (byte idx_0 in CellEs.Idxs)
                {
                    var build_0 = BuildEs.BuildingE(idx_0).BuildTC;

                    if (CellEs.EnvironmentEs.Hill(idx_0).HaveEnvironment)
                    {
                        if (!build_0.Is(BuildingTypes.Mine))
                        {
                            if (!EnvironmentEs.Hill(idx_0).HaveMaxResources)
                            {
                                CellEs.EnvironmentEs.Hill(idx_0).AddEveryMove();
                            }
                        }
                    }
                }
            }

            Es.Motion.AmountMotions.Amount += 1;



            Es.SunSidesE.SunSideTC.ToggleNext();
        }
    }
}