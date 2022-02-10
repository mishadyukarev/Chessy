namespace Game.Game
{
    sealed class EnvUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal EnvUIS(in Entities ents, in EntitiesUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var build_sel = BuildEs(Es.SelectedIdxE.IdxC.Idx).BuildingE.BuildTC;

            UIEs.LeftEs.EnvironmentEs.Resources<TextUIC>(ResourceTypes.Food).Text = Es.EnvFertilizeE(Es.SelectedIdxE.Idx).Resources.ToString();
            UIEs.LeftEs.EnvironmentEs.Resources<TextUIC>(ResourceTypes.Wood).Text = Es.EnvAdultForestE(Es.SelectedIdxE.Idx).Resources.ToString();
            UIEs.LeftEs.EnvironmentEs.Resources<TextUIC>(ResourceTypes.Ore).Text = Es.EnvHillE(Es.SelectedIdxE.Idx).Resources.ToString();
        }
    }
}