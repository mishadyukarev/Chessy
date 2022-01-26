namespace Game.Game
{
    struct SecButtonBuildUISys : IEcsRunSystem
    {
        public void Run()
        {
            var buildT = BuildingTypes.None;

            if (Entities.SelectedIdxE.IsSelCell)
            {
                ref var selUnitDatCom = ref CellUnitEs.Else(Entities.SelectedIdxE.IdxC.Idx).UnitC;

                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    ref var sellOnUnitCom = ref CellUnitEs.Else(Entities.SelectedIdxE.IdxC.Idx).OwnerC;

                    if (sellOnUnitCom.Is(Entities.WhoseMoveE.CurPlayerI))
                    {
                        buildT = BuildingTypes.Mine;
                    }
                }
            }

            if (buildT == BuildingTypes.None)
            {
                RightUIEntities.Building(ButtonTypes.Second).Parent.SetActive(false);
            }
            else
            {
                RightUIEntities.Building(ButtonTypes.Second).Parent.SetActive(true);
                RightUIEntities.BuildingZone(ButtonTypes.Second, buildT).Parent.SetActive(true);

                RightUIEntities.BuildingZone(ButtonTypes.Second, BuildingTypes.Farm).Parent.SetActive(false);
                RightUIEntities.BuildingZone(ButtonTypes.Second, BuildingTypes.City).Parent.SetActive(false);
            }
        }
    }
}
