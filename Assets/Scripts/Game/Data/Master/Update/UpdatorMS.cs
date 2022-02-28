using Chessy.Common;
using Photon.Pun;

namespace Chessy.Game
{
    sealed class UpdatorMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdatorMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                for (var unit = UnitTypes.Scout; unit < UnitTypes.Camel; unit++)
                {
                    E.UnitInfo(player, LevelTypes.First, unit).ScoutHeroCooldownC.Cooldown--;
                }

                E.ResourcesC(player, ResourceTypes.Food).Resources += ResourcesEconomy_Values.ADDING_FOOD_AFTER_MOVE;
            }

            for (byte idx_0 = 0; idx_0 < Start_Values.ALL_CELLS_AMOUNT; idx_0++)
            {
                for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
                {
                    E.UnitEs(idx_0).CoolDownC(abilityT).Cooldown--;
                }

                if (E.UnitTC(idx_0).HaveUnit && !E.UnitMainE(idx_0).IsAnimal)
                {
                    E.ResourcesC(E.UnitPlayerTC(idx_0).Player, ResourceTypes.Food).Resources -= ResourcesEconomy_Values.CostFoodForFeedingThem(E.UnitTC(idx_0).Unit);

                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (E.UnitPlayerTC(idx_0).Is(PlayerTypes.Second))
                        {
                            E.UnitHpC(idx_0).Health = CellUnitStatHp_Values.MAX_HP;
                        }
                    }


                    if (E.HaveFire(idx_0))
                    {
                        E.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                    }

                    else
                    {
                        if (E.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Protected))
                        {
                            if (E.UnitHpC(idx_0).Health >= CellUnitStatHp_Values.MAX_HP)
                            {
                                if (E.UnitTC(idx_0).Is(UnitTypes.Scout))
                                {
                                    if (E.BuildingTC(idx_0).Is(BuildingTypes.Woodcutter) || !E.BuildingTC(idx_0).HaveBuilding)
                                    {
                                        if (GameModeC.IsGameMode(GameModes.TrainingOff))
                                        {
                                            if (E.UnitPlayerTC(idx_0).Is(PlayerTypes.First))
                                            {
                                                if (E.BuildingsInfo(E.UnitPlayerTC(idx_0).Player, LevelTypes.First, BuildingTypes.City).IdxC.HaveAny)
                                                {
                                                    //Es.BuildE(idx_camp).BuildingE.Destroy(Es);
                                                }


                                                //Es.BuildE(idx_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitPlayerTC(idx_0).Player);
                                            }
                                        }
                                        else
                                        {
                                            if (E.BuildingsInfo(E.UnitPlayerTC(idx_0).Player, E.BuildingLevelTC(idx_0).Level, BuildingTypes.Camp).IdxC.HaveAny)
                                            {
                                                //Es.BuildingE(idx_camp).Destroy(Es);
                                            }

                                            //Es.BuildE(idx_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitPlayerTC(idx_0).Player);
                                        }
                                    }
                                }
                            }
                        }

                        else if (!E.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed))
                        {
                            if (E.UnitStepC(idx_0).HaveAnySteps)
                            {
                                E.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.Protected;
                            }
                        }
                    }
                    E.UnitStepC(idx_0).Steps = E.UnitInfo(E.UnitPlayerTC(idx_0), E.UnitLevelTC(idx_0), E.UnitTC(idx_0)).MaxSteps;
                }
            }

            E.Motions++;
            E.SunSideTC.ToggleNext();
        }
    }
}