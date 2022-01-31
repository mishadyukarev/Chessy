namespace Game.Game
{
    sealed class FirstButtonBuildUIS : SystemViewAbstract, IEcsRunSystem
    {
        public FirstButtonBuildUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            //var buildT = BuildingTypes.None;

            //if (Es.SelectedIdxE.IsSelCell)
            //{
            //    var selUnitDatCom = UnitEs.Main(Es.SelectedIdxE.IdxC.Idx).UnitC;

            //    if (selUnitDatCom.Is(UnitTypes.Pawn))
            //    {
            //        var selOnUnitCom = UnitEs.Main(Es.SelectedIdxE.IdxC.Idx).OwnerC;

            //        if (selOnUnitCom.Is(Es.WhoseMove.CurPlayerI))
            //        {
            //            buildT = BuildingTypes.Farm;
            //        }
            //    }
            //}

            //if (buildT == BuildingTypes.None)
            //{
            //    RightUIEntities.Building(ButtonTypes.First).Parent.SetActive(false);
            //}
            //else
            //{
            //    RightUIEntities.Building(ButtonTypes.First).Parent.SetActive(true);
            //    RightUIEntities.BuildingZone(ButtonTypes.First, buildT).Parent.SetActive(true);

            //    RightUIEntities.BuildingZone(ButtonTypes.First, BuildingTypes.Mine).Parent.SetActive(false);
            //    RightUIEntities.BuildingZone(ButtonTypes.First, BuildingTypes.City).Parent.SetActive(false);
            //}
        }
    }
}
