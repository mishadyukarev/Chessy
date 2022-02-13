namespace Game.Game
{
    sealed class EnvUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal EnvUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            UIEs.LeftEs.EnvironmentEs.Resources<TextUIC>(ResourceTypes.Food).Text = Es.FertilizeE(Es.SelectedIdxE.Idx).Resources.ToString();
            UIEs.LeftEs.EnvironmentEs.Resources<TextUIC>(ResourceTypes.Wood).Text = Es.AdultForestE(Es.SelectedIdxE.Idx).Resources.ToString();
            UIEs.LeftEs.EnvironmentEs.Resources<TextUIC>(ResourceTypes.Ore).Text = Es.HillE(Es.SelectedIdxE.Idx).Resources.ToString();
        }
    }
}