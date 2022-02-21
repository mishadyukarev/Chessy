namespace Game.Game
{
    sealed class EnvUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal EnvUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var idx_sel = E.SelectedIdxC.Idx;

            UIEs.LeftEnvEs.Resources<TextUIC>(ResourceTypes.Food).Text = ((int)(E.FertilizeC(idx_sel).Resources * 100)).ToString();
            UIEs.LeftEnvEs.Resources<TextUIC>(ResourceTypes.Wood).Text = ((int)(E.AdultForestC(idx_sel).Resources * 100)).ToString();
            UIEs.LeftEnvEs.Resources<TextUIC>(ResourceTypes.Ore).Text = ((int)(E.HillC(idx_sel).Resources * 100)).ToString();
        }
    }
}