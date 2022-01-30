using static Game.Game.EntityLeftEnvUIPool;

namespace Game.Game
{
    sealed class EnvUIS : SystemViewAbstract, IEcsRunSystem
    {
        public EnvUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            ref var build_sel = ref Es.CellEs.BuildEs.Build(Es.SelectedIdxE.IdxC.Idx).BuildTC;


            if (Es.SelectedIdxE.IsSelCell && !build_sel.Is(BuildingTypes.City))
            {
                Info<ButtonUIC>().SetActiveParent(true);

                Resources<TextUIC>(ResourceTypes.Food).Text = Es.CellEs.EnvironmentEs.Fertilizer(Es.SelectedIdxE.IdxC.Idx).AmountResources.ToString();
                Resources<TextUIC>(ResourceTypes.Wood).Text = Es.CellEs.EnvironmentEs.AdultForest(Es.SelectedIdxE.IdxC.Idx).AmountResources.ToString();
                Resources<TextUIC>(ResourceTypes.Ore).Text = Es.CellEs.EnvironmentEs.Hill(Es.SelectedIdxE.IdxC.Idx).AmountResources.ToString();
            }
            else
            {
                Info<ButtonUIC>().SetActiveParent(false);
            }
        }
    }
}