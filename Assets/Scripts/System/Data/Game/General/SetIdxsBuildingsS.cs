namespace Game.Game
{
    sealed class SetIdxsBuildingsS : SystemAbstract, IEcsRunSystem
    {
        internal SetIdxsBuildingsS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (var buildT = BuildingTypes.None + 1; buildT < BuildingTypes.End; buildT++)
            {
                for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                {
                    E.PlayerE(playerT).LevelE(LevelTypes.First).BuildsInGame(buildT).Clear();
                }
            }

            for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
            {
                if (E.BuildTC(idx_0).HaveBuilding)
                {
                    E.PlayerE(E.BuildPlayerTC(idx_0).Player).LevelE(E.BuildLevelTC(idx_0).Level)
                        .BuildsInGame(E.BuildTC(idx_0).Build).Add(idx_0);
                }
            }
        }
    }
}