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
            var build_sel = BuildEs(Es.SelectedIdxE.IdxC.Idx).BuildingE.BuildTC;


            if (Es.SelectedIdxE.IsSelCell && !build_sel.Is(BuildingTypes.City))
            {
                Info<ButtonUIC>().SetActiveParent(true);

                Resources<TextUIC>(ResourceTypes.Food).Text = EnvironmentEs(Es.SelectedIdxE.IdxC.Idx).Fertilizer.Resources.Amount.ToString();
                Resources<TextUIC>(ResourceTypes.Wood).Text = EnvironmentEs(Es.SelectedIdxE.IdxC.Idx).AdultForest.Resources.Amount.ToString();
                Resources<TextUIC>(ResourceTypes.Ore).Text = EnvironmentEs(Es.SelectedIdxE.IdxC.Idx).Hill.Resources.Amount.ToString();
            }
            else
            {
                Info<ButtonUIC>().SetActiveParent(false);
            }
        }
    }
}