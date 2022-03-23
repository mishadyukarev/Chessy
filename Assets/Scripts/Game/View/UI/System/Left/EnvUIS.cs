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
            var idx_sel = E.CellsC.Selected;

            UIE.LeftEnvEs.Envs[ResourceTypes.Food].TextUI.text = ((int)(100 * E.FertilizeC(idx_sel).Resources)).ToString();
            UIE.LeftEnvEs.Envs[ResourceTypes.Wood].TextUI.text = ((int)(100 * E.AdultForestC(idx_sel).Resources)).ToString();
            UIE.LeftEnvEs.Envs[ResourceTypes.Ore].TextUI.text = ((int)(100 * E.HillC(idx_sel).Resources)).ToString();
        }
    }
}