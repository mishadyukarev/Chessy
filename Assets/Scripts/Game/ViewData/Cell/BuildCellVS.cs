namespace Game.Game
{
    sealed class BuildCellVS : SystemViewAbstract, IEcsRunSystem
    {
        internal BuildCellVS(in EntitiesModel ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                var curPlayerI = E.CurPlayerITC.Player;

                var isVisForMe = E.BuildEs(idx_0).IsVisible(curPlayerI);
                var isVisForNext = E.BuildEs(idx_0).IsVisible(E.NextPlayer(curPlayerI).Player);

                for (var build = BuildingTypes.None + 1; build < BuildingTypes.End; build++)
                {
                    VEs.BuildingE(idx_0, build).Disable();
                }

                if (E.BuildingTC(idx_0).HaveBuilding)
                {
                    if (isVisForMe)
                    {
                        VEs.BuildingE(idx_0, E.BuildingTC(idx_0).Building).Enable();
                    }
                }
            }
        }
    }

}
