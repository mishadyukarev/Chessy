using static Game.Game.EntityLeftCityUIPool;

namespace Game.Game
{
    struct BuildZoneUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var unit_sel = ref CellBuildEs.Build(Entities.SelectedIdxE.IdxC.Idx).BuildTC;
            ref var own_sel = ref CellBuildEs.Build(Entities.SelectedIdxE.IdxC.Idx).PlayerTC;


            if (Entities.SelectedIdxE.IsSelCell && unit_sel.Is(BuildingTypes.City))
            {
                if (own_sel.Is(Entities.WhoseMoveE.CurPlayerI))
                {
                    Melt<ButtonUIC>().SetActiveParent(true);
                }
                else Melt<ButtonUIC>().SetActiveParent(false);
            }
            else
            {
                Melt<ButtonUIC>().SetActiveParent(false);
            }
        }
    }
}