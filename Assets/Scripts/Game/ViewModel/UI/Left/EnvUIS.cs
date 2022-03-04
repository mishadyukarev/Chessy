namespace Chessy.Game
{
    sealed class EnvUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal EnvUIS( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            var idx_sel = E.SelectedIdxC.Idx;

            UIE.LeftEnvEs.Envs[ResourceTypes.Food].TextUI.text = ((int)(E.FertilizeC(idx_sel).Resources * 100)).ToString();
            UIE.LeftEnvEs.Envs[ResourceTypes.Wood].TextUI.text = ((int)(E.AdultForestC(idx_sel).Resources * 100)).ToString();
            UIE.LeftEnvEs.Envs[ResourceTypes.Ore].TextUI.text = ((int)(E.HillC(idx_sel).Resources * 100)).ToString();
        }
    }
}