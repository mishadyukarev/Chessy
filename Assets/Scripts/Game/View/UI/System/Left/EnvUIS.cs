using System;

namespace Chessy.Game
{
    sealed class EnvUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal EnvUIS( in EntitiesViewUIGame entsUI, in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            var idx_sel = e.CellsC.Selected;

            eUI.LeftEnvEs.Envs[ResourceTypes.Food].TextUI.text = ((int)(100 * e.FertilizeC(idx_sel).Resources)).ToString();
            eUI.LeftEnvEs.Envs[ResourceTypes.Wood].TextUI.text = ((int)(100 * e.AdultForestC(idx_sel).Resources)).ToString();
            eUI.LeftEnvEs.Envs[ResourceTypes.Ore].TextUI.text = ((int)(100 * e.HillC(idx_sel).Resources)).ToString();
        }
    }
}