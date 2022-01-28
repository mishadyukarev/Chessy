namespace Game.Game
{
    struct FirstButtonBuildUIS : IEcsRunSystem
    {
        public void Run()
        {
            var buildT = BuildingTypes.None;

            if (Entities.SelectedIdxE.IsSelCell)
            {
                ref var selUnitDatCom = ref CellUnitEs.Else(Entities.SelectedIdxE.IdxC.Idx).UnitC;

                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    ref var selOnUnitCom = ref CellUnitEs.Else(Entities.SelectedIdxE.IdxC.Idx).OwnerC;

                    if (selOnUnitCom.Is(Entities.WhoseMove.CurPlayerI))
                    {
                        buildT = BuildingTypes.Farm;
                    }
                }
            }

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
