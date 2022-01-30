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

                Es.InventorResourcesEs.Resource(ResourceTypes.Food, player).Resources += EconomyValues.ADDING_FOOD_AFTER_MOVE;
            }

            foreach (byte idx_0 in Es.CellEs.Idxs)
            {
                ref var cell_0 = ref Es.CellEs.CellE(idx_0).InstanceIDC;

                ref var unit_0 = ref Es.CellEs.UnitEs.Main(idx_0).UnitC;
                ref var levUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).LevelC;
                ref var ownUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).OwnerC;
                ref var hp_0 = ref Es.CellEs.UnitEs.StatEs.Hp(idx_0).Health;
                ref var condUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).ConditionC;

                ref var buil_0 = ref Es.CellEs.BuildEs.Build(idx_0).BuildTC;
                ref var ownBuil_0 = ref Es.CellEs.BuildEs.Build(idx_0).PlayerTC;
                ref var fire_0 = ref Es.CellEs.FireEs.Fire(idx_0).Fire;

                foreach (var item in Es.CellEs.TrailEs.Keys) Es.CellEs.TrailEs.Trail(item, idx_0).Health.Take();
                foreach (var item in Es.CellEs.UnitEs.CooldownKeys) Es.CellEs.UnitEs.Unique(item, idx_0).Cooldown.Take();



                if (unit_0.Have && !unit_0.IsAnimal)
                {
                    //CellUnitStepsInConditionEs.Steps(condUnit_0.Condition, idx_0)++;

                    Es.InventorResourcesEs.Resource(ResourceTypes.Food, ownUnit_0.Player).Resources.Take(EconomyValues.CostFood(unit_0.Unit));

                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.Second))
                        {
                            Es.CellEs.UnitEs.StatEs.Hp(idx_0).Health.Amount = CellUnitHpValues.MAX_HP;
                        }
                    }


                    if (fire_0.Have)
                    {
                        if (condUnit_0.HaveCondition) condUnit_0.Reset();
                    }

                    else
                    {
                        if (condUnit_0.Is(ConditionUnitTypes.Protected))
                        {
                            if (Es.CellEs.UnitEs.StatEs.Hp(idx_0).HaveMax)
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
                                                    Es.CellEs.BuildEs.Build(idx_camp).BuildTC.Reset();
                                                }


                                                Es.CellEs.BuildEs.Build(idx_0).SetNew(BuildingTypes.Camp, ownUnit_0.Player);
                                                Es.WhereBuildingEs.HaveBuild(BuildingTypes.Camp, ownUnit_0.Player, idx_0).HaveBuilding.Have = true;
                                            }
                                        }
                                        else
                                        {
                                            if (Es.WhereBuildingEs.IsSetted(BuildingTypes.Camp, ownUnit_0.Player, out var idx_camp))
                                            {
                                                Es.CellEs.BuildEs.Build(idx_camp).BuildTC.Reset();
                                            }

                                            Es.CellEs.BuildEs.Build(idx_0).SetNew(BuildingTypes.Camp, ownUnit_0.Player);
                                            Es.WhereBuildingEs.HaveBuild(BuildingTypes.Camp, ownUnit_0.Player, idx_0).HaveBuilding.Have = true;
                                        }
                                    }
                                }
                            }
                        }

                        else if (!condUnit_0.Is(ConditionUnitTypes.Relaxed))
                        {
                            if (Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Have)
                            {
                                condUnit_0.Condition = ConditionUnitTypes.Protected;
                            }
                        }
                    }
                    Es.CellEs.UnitEs.StatEs.Step(idx_0).SetMax(Es.CellEs.UnitEs.Main(idx_0));
                }
            }

            var amountAdultForest = 0;
            foreach (var idx in Es.CellEs.Idxs)
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
                foreach (byte idx_0 in Es.CellEs.Idxs)
                {
                    ref var build_0 = ref Es.CellEs.BuildEs.Build(idx_0).BuildTC;

                    if (Es.CellEs.EnvironmentEs.Hill(idx_0).HaveEnvironment)
                    {
                        if (!build_0.Is(BuildingTypes.Mine))
                        {
                            if (!EnvironmentEs.Hill(idx_0).HaveMaxResources)
                            {
                                Es.CellEs.EnvironmentEs.Hill(idx_0).AddEveryMove();
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