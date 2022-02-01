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

            foreach (byte idx_0 in CellEsWorker.Idxs)
            {
                ref var cell_0 = ref CellEs(idx_0).CellE.InstanceIDC;

                var unit_0 = UnitEs(idx_0).MainE.UnitTC;
                var ownUnit_0 = UnitEs(idx_0).MainE.OwnerC;

                var buil_0 = BuildEs(idx_0).BuildingE.BuildTC;

                foreach (var item in CellEs(idx_0).TrailEs.Keys) TrailEs(idx_0).Trail(item).TakeEveryUpdate();
                foreach (var item in UnitEs(idx_0).CooldownKeys) UnitEs(idx_0).CooldownAbility(item).TakeAfterUpdate();



                if (UnitEs(idx_0).MainE.HaveUnit(UnitStatEs(idx_0)) && !unit_0.IsAnimal)
                {
                    //CellUnitStepsInConditionEs.Steps(condUnit_0.Condition, idx_0)++;

                    Es.InventorResourcesEs.Resource(ResourceTypes.Food, ownUnit_0.Player).Resources.Amount -= EconomyValues.CostFood(unit_0.Unit);

                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.Second))
                        {
                            UnitStatEs(idx_0).Hp.SetMax();
                        }
                    }


                    if (EffectEs(idx_0).FireE.HaveFireC.Have)
                    {
                        UnitEs(idx_0).MainE.ResetCondition();
                    }

                    else
                    {
                        if (UnitEs(idx_0).MainE.ConditionTC.Is(ConditionUnitTypes.Protected))
                        {
                            if (UnitStatEs(idx_0).Hp.HaveMax)
                            {
                                if (unit_0.Is(UnitTypes.Scout))
                                {
                                    if (buil_0.Is(BuildingTypes.Woodcutter) || !buil_0.Have)
                                    {
                                        if (GameModeC.IsGameMode(GameModes.TrainingOff))
                                        {
                                            if (ownUnit_0.Is(PlayerTypes.First))
                                            {
                                                if (Es.WhereBuildingEs.TryGetBuilding(BuildingTypes.Camp, ownUnit_0.Player, out var idx_camp))
                                                {
                                                    BuildEs(idx_camp).BuildingE.Destroy(BuildEs(idx_camp), Es.WhereBuildingEs);
                                                }


                                                BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.Camp, ownUnit_0.Player, BuildEs(idx_0), Es.WhereBuildingEs);
                                                Es.WhereBuildingEs.HaveBuild(BuildingTypes.Camp, ownUnit_0.Player, idx_0).HaveBuilding.Have = true;
                                            }
                                        }
                                        else
                                        {
                                            if (Es.WhereBuildingEs.TryGetBuilding(BuildingTypes.Camp, ownUnit_0.Player, out var idx_camp))
                                            {
                                                BuildEs(idx_camp).BuildingE.Destroy(BuildEs(idx_camp), Es.WhereBuildingEs);
                                            }

                                            BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.Camp, ownUnit_0.Player, BuildEs(idx_0), Es.WhereBuildingEs);
                                            Es.WhereBuildingEs.HaveBuild(BuildingTypes.Camp, ownUnit_0.Player, idx_0).HaveBuilding.Have = true;
                                        }
                                    }
                                }
                            }
                        }

                        else if (!UnitEs(idx_0).MainE.ConditionTC.Is(ConditionUnitTypes.Relaxed))
                        {
                            if (UnitStatEs(idx_0).StepE.HaveSteps)
                            {
                                UnitEs(idx_0).MainE.SetCondition(ConditionUnitTypes.Protected);
                            }
                        }
                    }
                    UnitStatEs(idx_0).StepE.SetMax(UnitEs(idx_0).MainE);
                }
            }

            var amountAdultForest = 0;
            foreach (var idx in CellEsWorker.Idxs)
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
                foreach (byte idx_0 in CellEsWorker.Idxs)
                {
                    var build_0 = BuildEs(idx_0).BuildingE.BuildTC;

                    if (EnvironmentEs(idx_0).Hill.HaveEnvironment)
                    {
                        if (!build_0.Is(BuildingTypes.Mine))
                        {
                            if (!EnvironmentEs(idx_0).Hill.HaveMaxResources)
                            {
                                EnvironmentEs(idx_0).Hill.AddEveryMove();
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