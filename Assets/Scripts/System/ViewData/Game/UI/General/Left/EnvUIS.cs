namespace Game.Game
{
    sealed class EnvUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal EnvUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var idx_sel = Es.SelectedIdxC.Idx;

            UIEs.LeftEnvEs.Resources<TextUIC>(ResourceTypes.Food).Text = ((int)(Es.FertilizeC(idx_sel).Resources * 100)).ToString();
            UIEs.LeftEnvEs.Resources<TextUIC>(ResourceTypes.Wood).Text = ((int)(Es.AdultForestC(idx_sel).Resources * 100)).ToString();
            UIEs.LeftEnvEs.Resources<TextUIC>(ResourceTypes.Ore).Text = ((int)(Es.HillC(idx_sel).Resources * 100)).ToString();
        }
    }
}