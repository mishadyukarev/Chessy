namespace Game.Game
{
    sealed class BuildCellVS : SystemViewAbstract, IEcsRunSystem
    {
        internal BuildCellVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                var curPlayerI = Es.WhoseMoveE.CurPlayerI;

                var isVisForMe = Es.BuildingEs(idx_0).BuildingVisE(curPlayerI).IsVisibleC.IsVisible;
                var isVisForNext = Es.BuildingEs(idx_0).BuildingVisE(Es.WhoseMoveE.NextPlayerFrom(curPlayerI)).IsVisibleC.IsVisible;

                for (var build = BuildingTypes.None + 1; build < BuildingTypes.End; build++)
                {
                    VEs.BuildingE(idx_0, build).SR.Disable();
                }

                if (Es.BuildingE(idx_0).HaveBuilding)
                {
                    if (isVisForMe)
                    {
                        VEs.BuildingE(idx_0, Es.BuildingE(idx_0).Building).SR.Enable();
                    }
                }
            }
        }
    }

}
