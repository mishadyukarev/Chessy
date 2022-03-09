using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit.Effect;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game
{
    sealed class UnitShiftS : SystemAbstract, IEcsRunSystem
    {
        internal UnitShiftS(in EntitiesModel ents) : base(ents)
        {

        }

        public void Run()
        {
            for (byte idx_from = 0; idx_from < StartValues.ALL_CELLS_AMOUNT; idx_from++)
            {
                var idx_to = E.UnitMainE(idx_from).ShiftTo.Idx;

                if (idx_to ==  0) continue;

                E.UnitEs(idx_to).Set(E.UnitEs(idx_from));
                E.UnitConditionTC(idx_to).Condition = ConditionUnitTypes.None;

                E.UnitTC(idx_from).Unit = UnitTypes.None;


                var direct = E.CellEs(idx_from).Direct(idx_to);

                if (!E.UnitTC(idx_to).Is(UnitTypes.Undead))
                {
                    if (E.AdultForestC(idx_from).HaveAnyResources)
                    {
                        E.CellEs(idx_from).TrailHealthC(direct).Health = TrailValues.HEALTH_TRAIL;
                    }
                    if (E.AdultForestC(idx_to).HaveAnyResources)
                    {
                        //if (E.PlayerE(E.UnitPlayerTC(idx_to).Player).AvailableHeroTC.Is(UnitTypes.Elfemale))
                        //{
                        //    E.UnitHpC(idx_to).Health = HpValues.MAX;
                        //}

                        if (E.UnitTC(idx_to).Is(UnitTypes.Pawn))
                        {
                            if (E.PlayerE(E.UnitPlayerTC(idx_to).Player).AvailableHeroTC.Is(UnitTypes.Elfemale))
                            {

                                E.BuildingMainE(idx_to).Set(BuildingTypes.Woodcutter, LevelTypes.First, BuildingValues.MAX_HP, E.UnitPlayerTC(idx_to).Player);
                            }
                        }


                        E.CellEs(idx_to).TrailHealthC(direct.Invert()).Health = TrailValues.HEALTH_TRAIL;
                    }

                    if (E.RiverEs(idx_to).RiverTC.HaveRiverNear)
                    {
                        E.UnitWaterC(idx_to).Water = WaterValues.MAX;


                        if (E.PlayerE(E.UnitPlayerTC(idx_to).Player).AvailableHeroTC.Is(UnitTypes.Snowy))
                        {
                            if (E.UnitTC(idx_to).Is(UnitTypes.Pawn))
                            {
                                if (E.UnitMainTWTC(idx_to).Is(ToolWeaponTypes.BowCrossbow))
                                {
                                    E.UnitEffectFrozenArrawC(idx_to).Shoots = 1;
                                }
                                else
                                {
                                    E.UnitEffectShield(idx_to).Protection = ShieldValues.AFTER_DIRECT_WAVE;
                                }
                            }
                            else if (E.UnitTC(idx_to).Is(UnitTypes.King))
                            {
                                E.UnitEffectShield(idx_to).Protection = ShieldValues.AFTER_DIRECT_WAVE;
                            }
                        }
                    }
                }

                switch (E.UnitTC(idx_to).Unit)
                {
                    case UnitTypes.Elfemale:
                        //if (E.AdultForestC(idx_to).HaveAnyResources)
                        //{
                        //    E.AdultForestC(idx_to).Resources = EnvironmentValues.MAX_RESOURCES;
                        //}
                        break;

                    case UnitTypes.Snowy:
                        if (E.UnitWaterC(idx_to).Water > 0)
                        {
                            E.FertilizeC(idx_to).Resources = EnvironmentValues.MAX_RESOURCES;
                            E.HaveFire(idx_to) = false;
                            E.UnitWaterC(idx_to).Water -= WaterValues.AFTER_SHIFT_SNOWY;
                        }
                        break;

                    case UnitTypes.Hell:
                        if (E.AdultForestC(idx_to).HaveAnyResources)
                        {
                            E.EffectEs(idx_to).HaveFire = true;
                        }
                        break;
                }

                if (E.BuildingTC(idx_to).HaveBuilding && !E.BuildingTC(idx_to).Is(BuildingTypes.City))
                {
                    if (!E.BuildingPlayerTC(idx_to).Is(E.UnitPlayerTC(idx_to).Player))
                    {
                        E.BuildingTC(idx_to).Building = BuildingTypes.None;
                    }
                }

                E.UnitMainE(idx_from).ShiftTo.Idx = 0;
                E.UnitMainE(idx_to).ShiftTo.Idx = 0;
            }
        }
    }
}