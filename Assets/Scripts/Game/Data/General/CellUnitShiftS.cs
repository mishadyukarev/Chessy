namespace Game.Game
{
    sealed class CellUnitShiftS : SystemAbstract
    {
        internal CellUnitShiftS(in EntitiesModel ents) : base(ents)
        {
            ents.UnitShiftE = new CellUnitShiftE(Shift);
        }

        void Shift(byte idx_from, byte idx_to)
        {
            E.UnitEs(idx_to).Set(E.UnitEs(idx_from));
            E.UnitConditionTC(idx_to).Condition = ConditionUnitTypes.None;

            E.UnitTC(idx_from).Unit = UnitTypes.None;


            var direct = E.CellEs(idx_from).Direct(idx_to);

            if (!E.UnitTC(idx_to).Is(UnitTypes.Undead))
            {
                if (E.AdultForestC(idx_from).HaveAnyResources)
                {
                    E.CellEs(idx_from).TrailHealthC(direct).Health = CellTrail_Values.HEALTH_TRAIL;
                }
                if (E.AdultForestC(idx_to).HaveAnyResources)
                {
                    E.CellEs(idx_to).TrailHealthC(direct.Invert()).Health = CellTrail_Values.HEALTH_TRAIL;
                }

                if (E.RiverEs(idx_to).RiverTC.HaveRiverNear)
                {
                    E.UnitWaterC(idx_to).Water = E.UnitInfo(E.UnitPlayerTC(idx_to), E.UnitLevelTC(idx_to), E.UnitTC(idx_to)).MaxWater;
                }
            }

            if (E.UnitTC(idx_to).Is(UnitTypes.Hell))
            {
                if (E.AdultForestC(idx_to).HaveAnyResources)
                {
                    E.EffectEs(idx_to).HaveFire = true;
                }
            }

            if (E.BuildTC(idx_to).HaveBuilding && !E.BuildTC(idx_to).Is(BuildingTypes.City))
            {
                if (!E.BuildPlayerTC(idx_to).Is(E.UnitPlayerTC(idx_to).Player))
                {
                    E.BuildTC(idx_to).Building = BuildingTypes.None;
                }
            }

        }
    }
}