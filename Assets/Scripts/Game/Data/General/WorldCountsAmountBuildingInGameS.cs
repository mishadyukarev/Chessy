namespace Chessy.Game.System.Model
{
    sealed class WorldCountsAmountBuildingInGameS : SystemAbstract, IEcsRunSystem
    {
        internal WorldCountsAmountBuildingInGameS(in EntitiesModel eM) : base(eM) { }

        public void Run()
        {
            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                for (var buildingT = BuildingTypes.None + 1; buildingT < BuildingTypes.End; buildingT++)
                {
                    E.BuildingsInfo(playerT, LevelTypes.First, buildingT).IdxC.Clear();
                }
            }

            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                if (E.BuildingTC(idx_0).HaveBuilding)
                {
                    E.BuildingsInfo(E.BuildingPlayerTC(idx_0).Player, E.BuildingLevelTC(idx_0).Level, E.BuildingTC(idx_0).Building).IdxC.Add(idx_0);
                }
            }
        }
    }
}