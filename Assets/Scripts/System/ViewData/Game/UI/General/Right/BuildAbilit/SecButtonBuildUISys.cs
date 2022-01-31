namespace Game.Game
{
    sealed class SecButtonBuildUISys : SystemViewAbstract, IEcsRunSystem
    {
        public SecButtonBuildUISys(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            //var buildT = BuildingTypes.None;

            //if (Es.SelectedIdxE.IsSelCell)
            //{
            //    ref var selUnitDatCom = UnitEs.Main(Es.SelectedIdxE.IdxC.Idx).UnitC;

            //    if (selUnitDatCom.Is(UnitTypes.Pawn))
            //    {
            //        ref var sellOnUnitCom = UnitEs.Main(Es.SelectedIdxE.IdxC.Idx).OwnerC;

            //        if (sellOnUnitCom.Is(Es.WhoseMove.CurPlayerI))
            //        {
            //            buildT = BuildingTypes.Mine;
            //        }
            //    }
            //}

            //if (buildT == BuildingTypes.None)
            //{
            //    RightUIEntities.Building(ButtonTypes.Second).Parent.SetActive(false);
            //}
            //else
            //{
            //    RightUIEntities.Building(ButtonTypes.Second).Parent.SetActive(true);
            //    RightUIEntities.BuildingZone(ButtonTypes.Second, buildT).Parent.SetActive(true);

            //    RightUIEntities.BuildingZone(ButtonTypes.Second, BuildingTypes.Farm).Parent.SetActive(false);
            //    RightUIEntities.BuildingZone(ButtonTypes.Second, BuildingTypes.City).Parent.SetActive(false);
            //}
        }
    }
}
