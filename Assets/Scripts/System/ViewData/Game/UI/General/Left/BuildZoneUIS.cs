using static Game.Game.EntityLeftCityUIPool;

namespace Game.Game
{
    sealed class BuildZoneUIS : SystemViewAbstract, IEcsRunSystem
    {
        public BuildZoneUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            ref var unit_sel = ref Es.CellEs.BuildEs.Build(Es.SelectedIdxE.IdxC.Idx).BuildTC;
            ref var own_sel = ref Es.CellEs.BuildEs.Build(Es.SelectedIdxE.IdxC.Idx).PlayerTC;


            if (Es.SelectedIdxE.IsSelCell && unit_sel.Is(BuildingTypes.City))
            {
                if (own_sel.Is(Es.WhoseMove.CurPlayerI))
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