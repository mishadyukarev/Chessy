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
            var build_sel = BuildEs.BuildingE(Es.SelectedIdxE.IdxC.Idx).BuildTC;


            if (Es.SelectedIdxE.IsSelCell && !build_sel.Is(BuildingTypes.City))
            {
                Info<ButtonUIC>().SetActiveParent(true);

                Resources<TextUIC>(ResourceTypes.Food).Text = CellEs.EnvironmentEs.Fertilizer(Es.SelectedIdxE.IdxC.Idx).Resources.Amount.ToString();
                Resources<TextUIC>(ResourceTypes.Wood).Text = CellEs.EnvironmentEs.AdultForest(Es.SelectedIdxE.IdxC.Idx).Resources.Amount.ToString();
                Resources<TextUIC>(ResourceTypes.Ore).Text = CellEs.EnvironmentEs.Hill(Es.SelectedIdxE.IdxC.Idx).Resources.Amount.ToString();
            }
            else
            {
                Info<ButtonUIC>().SetActiveParent(false);
            }
        }
    }
}