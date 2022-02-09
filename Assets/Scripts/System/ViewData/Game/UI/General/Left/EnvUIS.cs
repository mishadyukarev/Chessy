using static Game.Game.LeftEnvironmentUIEs;

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

                Resources<TextUIC>(ResourceTypes.Food).Text = EnvironmentEs(Es.SelectedIdxE.IdxC.Idx).Fertilizer.ResourcesC.Amount.ToString();
                Resources<TextUIC>(ResourceTypes.Wood).Text = EnvironmentEs(Es.SelectedIdxE.IdxC.Idx).AdultForest.ResourcesC.Amount.ToString();
                Resources<TextUIC>(ResourceTypes.Ore).Text = EnvironmentEs(Es.SelectedIdxE.IdxC.Idx).Hill.ResourcesC.Amount.ToString();
            }
            else
            {
                Info<ButtonUIC>().SetActiveParent(false);
            }
        }
    }
}