using Chessy.Game.Entity.Model;
using System;

namespace Chessy.Game
{
    sealed class EnvUIS : SystemUIAbstract
    {
        readonly EntitiesViewUIGame eUI;

        internal EnvUIS( in EntitiesViewUIGame entsUI, in EntitiesModelGame ents) : base(ents)
        {
            eUI = entsUI;
        }

        internal void Run()
        {
            var idx_sel = e.CellsC.Selected;

            eUI.LeftEnvEs.Envs[ResourceTypes.Food].TextUI.text = ((int)(100 * e.FertilizeC(idx_sel).Resources)).ToString();
            eUI.LeftEnvEs.Envs[ResourceTypes.Wood].TextUI.text = ((int)(100 * e.AdultForestC(idx_sel).Resources)).ToString();
            eUI.LeftEnvEs.Envs[ResourceTypes.Ore].TextUI.text = ((int)(100 * e.HillC(idx_sel).Resources)).ToString();
        }
    }
}