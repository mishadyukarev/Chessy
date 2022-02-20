namespace Game.Game
{
    sealed class BuildCellVS : SystemViewAbstract, IEcsRunSystem
    {
        internal BuildCellVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                var curPlayerI = E.CurPlayerI.Player;

                var isVisForMe = E.BuildE(idx_0).IsVisible(curPlayerI);
                var isVisForNext = E.BuildE(idx_0).IsVisible(E.WhoseMove.NextPlayerFrom(curPlayerI));

                for (var build = BuildingTypes.None + 1; build < BuildingTypes.End; build++)
                {
                    VEs.BuildingE(idx_0, build).SR.Disable();
                }

                if (E.BuildTC(idx_0).HaveBuilding)
                {
                    if (isVisForMe)
                    {
                        VEs.BuildingE(idx_0, E.BuildTC(idx_0).Build).SR.Enable();
                    }
                }
            }
        }
    }

}
