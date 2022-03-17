using System;

namespace Chessy.Game
{
    sealed class EnvUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal EnvUIS( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            var idx_sel = E.CellsC.Selected;

            UIE.LeftEnvEs.Envs[ResourceTypes.Food].TextUI.text = (Math.Truncate(100 * E.FertilizeC(idx_sel).Resources) / 100).ToString();
            UIE.LeftEnvEs.Envs[ResourceTypes.Wood].TextUI.text = (Math.Truncate(100 * E.AdultForestC(idx_sel).Resources) / 100).ToString();
            UIE.LeftEnvEs.Envs[ResourceTypes.Ore].TextUI.text = (Math.Truncate(100 * E.HillC(idx_sel).Resources) / 100).ToString();
        }
    }
}