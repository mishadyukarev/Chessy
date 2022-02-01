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
            var unit_sel = BuildEs(Es.SelectedIdxE.IdxC.Idx).BuildingE.BuildTC;
            var own_sel = BuildEs(Es.SelectedIdxE.IdxC.Idx).BuildingE.Owner;


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