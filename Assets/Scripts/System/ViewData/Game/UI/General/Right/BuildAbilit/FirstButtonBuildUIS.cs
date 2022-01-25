using static Game.Game.CellUnitEntities;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct FirstButtonBuildUIS : IEcsRunSystem
    {
        public void Run()
        {
            var buildT = BuildingTypes.None;

            if (EntitiesPool.SelectedIdxE.IsSelCell)
            {
                ref var selUnitDatCom = ref CellUnitEntities.Else(EntitiesPool.SelectedIdxE.IdxC.Idx).UnitC;

                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    ref var selOnUnitCom = ref CellUnitEntities.Else(EntitiesPool.SelectedIdxE.IdxC.Idx).OwnerC;

                    if (selOnUnitCom.Is(WhoseMoveE.CurPlayerI))
                    {
                        buildT = BuildingTypes.Farm;
                    }
                }
            }

            if (buildT == BuildingTypes.None)
            {
                RightUIEntities.Building(ButtonTypes.First).Parent.SetActive(false);
            }
            else
            {
                RightUIEntities.Building(ButtonTypes.First).Parent.SetActive(true);
                RightUIEntities.BuildingZone(ButtonTypes.First, buildT).Parent.SetActive(true);

                RightUIEntities.BuildingZone(ButtonTypes.First, BuildingTypes.Mine).Parent.SetActive(false);
                RightUIEntities.BuildingZone(ButtonTypes.First, BuildingTypes.City).Parent.SetActive(false);
            }
        }
    }
}
