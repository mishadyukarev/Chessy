namespace Chessy.Game
{
    sealed class EnvUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal EnvUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var idx_sel = E.SelectedIdxC.Idx;

            UIEs.LeftEnvEs.Envs[ResourceTypes.Food].TextUI.text = ((int)(E.FertilizeC(idx_sel).Resources * 100)).ToString();
            UIEs.LeftEnvEs.Envs[ResourceTypes.Wood].TextUI.text = ((int)(E.AdultForestC(idx_sel).Resources * 100)).ToString();
            UIEs.LeftEnvEs.Envs[ResourceTypes.Ore].TextUI.text = ((int)(E.HillC(idx_sel).Resources * 100)).ToString();
        }
    }
}