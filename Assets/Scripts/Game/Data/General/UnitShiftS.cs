using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit.Effect;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.System.Model
{
    public struct UnitShiftS
    {
        public UnitShiftS(in byte idx_from, in byte idx_to, in EntitiesModel e)
        {
            e.UnitEs(idx_to).Set(e.UnitEs(idx_from));
            e.UnitConditionTC(idx_to).Condition = ConditionUnitTypes.None;

            e.UnitTC(idx_from).Unit = UnitTypes.None;


            var direct = e.CellEs(idx_from).Direct(idx_to);

            if (!e.UnitTC(idx_to).Is(UnitTypes.Undead))
            {
                if (e.AdultForestC(idx_from).HaveAnyResources)
                {
                    e.CellEs(idx_from).TrailHealthC(direct).Health = TrailValues.HEALTH_TRAIL;
                    new TrailsVisibleS(idx_from, direct, e);
                }
                if (e.AdultForestC(idx_to).HaveAnyResources)
                {
                    //if (E.PlayerE(E.UnitPlayerTC(idx_to).Player).AvailableHeroTC.Is(UnitTypes.Elfemale))
                    //{
                    //    E.UnitHpC(idx_to).Health = HpValues.MAX;
                    //}

                    if (e.UnitTC(idx_to).Is(UnitTypes.Pawn))
                    {
                        if (e.PlayerInfoE(e.UnitPlayerTC(idx_to).Player).AvailableHeroTC.Is(UnitTypes.Elfemale))
                        {
                            new BuildS(BuildingTypes.Woodcutter, LevelTypes.First, e.UnitPlayerTC(idx_to).Player, BuildingValues.MAX_HP, idx_to, e);
                        }
                    }

                    var dirTrail = direct.Invert();

                    e.CellEs(idx_to).TrailHealthC(dirTrail).Health = TrailValues.HEALTH_TRAIL;
                    new TrailsVisibleS(idx_to, dirTrail, e);
                }

                if (e.RiverEs(idx_to).RiverTC.HaveRiverNear)
                {
                    e.UnitWaterC(idx_to).Water = WaterValues.MAX;


                    if (e.PlayerInfoE(e.UnitPlayerTC(idx_to).Player).AvailableHeroTC.Is(UnitTypes.Snowy))
                    {
                        if (e.UnitTC(idx_to).Is(UnitTypes.Pawn))
                        {
                            if (e.UnitMainTWTC(idx_to).Is(ToolWeaponTypes.BowCrossbow))
                            {
                                e.UnitEffectFrozenArrawC(idx_to).Shoots = 1;
                            }
                            else
                            {
                                e.UnitEffectShield(idx_to).Protection = ShieldValues.AFTER_DIRECT_WAVE;
                            }
                        }
                        else if (e.UnitTC(idx_to).Is(UnitTypes.King))
                        {
                            e.UnitEffectShield(idx_to).Protection = ShieldValues.AFTER_DIRECT_WAVE;
                        }
                    }
                }
            }

            switch (e.UnitTC(idx_to).Unit)
            {
                case UnitTypes.Elfemale:
                    //if (E.AdultForestC(idx_to).HaveAnyResources)
                    //{
                    //    E.AdultForestC(idx_to).Resources = EnvironmentValues.MAX_RESOURCES;
                    //}
                    break;

                case UnitTypes.Snowy:
                    if (e.UnitWaterC(idx_to).Water > 0)
                    {
                        e.FertilizeC(idx_to).Resources = EnvironmentValues.MAX_RESOURCES;
                        e.HaveFire(idx_to) = false;
                        e.UnitWaterC(idx_to).Water -= WaterValues.AFTER_SHIFT_SNOWY;
                    }
                    break;

                case UnitTypes.Hell:
                    if (e.AdultForestC(idx_to).HaveAnyResources)
                    {
                        e.EffectEs(idx_to).HaveFire = true;
                    }
                    break;
            }

            if (e.BuildingTC(idx_to).HaveBuilding && !e.BuildingTC(idx_to).Is(BuildingTypes.City))
            {
                if (!e.BuildingPlayerTC(idx_to).Is(e.UnitPlayerTC(idx_to).Player))
                {
                    e.BuildingTC(idx_to).Building = BuildingTypes.None;
                }
            }

            new GetDataUnitOnCellS(idx_to, e);

            foreach (var idx_1 in e.CellEs(idx_to).IdxsAround)
            {
                if (e.UnitTC(idx_1).HaveUnit)
                {
                    new GetDataUnitOnCellS(idx_1, e);
                }

                if (e.CellEs(idx_1).IsActiveParentSelf)
                {
                    foreach (var idx_2 in e.CellEs(idx_1).IdxsAround)
                    {
                        if (e.UnitTC(idx_2).HaveUnit)
                        {
                            new GetDataUnitOnCellS(idx_2, e);
                        }
                    }
                }
            }
        }
    }
}