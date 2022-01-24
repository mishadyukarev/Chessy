using static Game.Game.CellUnitEs;
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
                ref var selUnitDatCom = ref Unit(EntitiesPool.SelectedIdxE.IdxC.Idx);

                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    ref var selOnUnitCom = ref EntitiesPool.UnitElse.Owner(EntitiesPool.SelectedIdxE.IdxC.Idx);

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
