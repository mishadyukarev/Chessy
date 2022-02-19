using Game.Common;
using Photon.Pun;

namespace Game.Game
{
    sealed class UpdatorMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdatorMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                for (var unit = UnitTypes.Scout; unit < UnitTypes.Camel; unit++)
                {
                    Es.PlayerE(player).UnitsInfoE(unit).ScoutHeroCooldownC.Cooldown--;
                }

                Es.PlayerE(player).ResourcesC(ResourceTypes.Food).Resources += ResourcesEconomy_Values.ADDING_FOOD_AFTER_MOVE;
            }

            for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
            {
                //foreach (var item in Es.CellEs(idx_0).TrailEs.Keys) Es.TrailEs(idx_0).Trail(item).HealthC.Take(0.1f);
                //foreach (var item in Es.UnitE(idx_0).CooldownKeys) Es.UnitE(idx_0).Ability(item).CooldownC.Cooldown--;



                if (Es.UnitTC(idx_0).HaveUnit && !Es.UnitEs(idx_0).IsAnimal)
                {
                    //CellUnitStepsInConditionEs.Steps(condUnit_0.Condition, idx_0)++;

                    //Es.InventorResourcesEs.Resource(ResourceTypes.Food, Es.UnitPlayerTC(idx_0).Player).ResourceC.Resources -= ResourcesInInventorValues.CostFoodForFeedingThem(Es.UnitTC(idx_0).Unit);

                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (Es.UnitPlayerTC(idx_0).Is(PlayerTypes.Second))
                        {
                            Es.UnitHpC(idx_0).Health = CellUnitStatHp_Values.MAX_HP;
                        }
                    }


                    if (Es.HaveFire(idx_0))
                    {
                        Es.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                    }

                    else
                    {
                        if (Es.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Protected))
                        {
                            if (Es.UnitHpC(idx_0).Health >= CellUnitStatHp_Values.MAX_HP)
                            {
                                if (Es.UnitTC(idx_0).Is(UnitTypes.Scout))
                                {
                                    if (Es.BuildTC(idx_0).Is(BuildingTypes.Woodcutter) || !Es.BuildTC(idx_0).HaveBuilding)
                                    {
                                        if (GameModeC.IsGameMode(GameModes.TrainingOff))
                                        {
                                            if (Es.UnitPlayerTC(idx_0).Is(PlayerTypes.First))
                                            {
                                                if (Es.PlayerE(Es.UnitPlayerTC(idx_0).Player).LevelE(Es.BuildLevelTC(idx_0).Level).BuildsInGame(BuildingTypes.City).HaveAny)
                                                {
                                                    //Es.BuildE(idx_camp).BuildingE.Destroy(Es);
                                                }


                                                //Es.BuildE(idx_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitPlayerTC(idx_0).Player);
                                            }
                                        }
                                        else
                                        {
                                            if (Es.PlayerE(Es.UnitPlayerTC(idx_0).Player).LevelE(Es.BuildLevelTC(idx_0).Level).BuildsInGame(BuildingTypes.Camp).HaveAny)
                                            {
                                                //Es.BuildingE(idx_camp).Destroy(Es);
                                            }

                                            //Es.BuildE(idx_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitPlayerTC(idx_0).Player);
                                        }
                                    }
                                }
                            }
                        }

                        else if (!Es.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed))
                        {
                            if (Es.UnitStepC(idx_0).HaveAnySteps)
                            {
                                Es.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.Protected;
                            }
                        }
                    }
                    Es.UnitStepC(idx_0).Steps = CellUnitStatStep_Values.StandartForUnit(Es.UnitTC(idx_0).Unit);
                }
            }

            Es.Motions += 1;
            Es.SunSideTC.ToggleNext();
        }
    }
}