using Chessy.Game.Model.Entity;
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

        internal override void Sync()
        {
            var idx_sel = _e.CellsC.Selected;

            eUI.LeftEnvEs.Envs[ResourceTypes.Food].TextUI.text = ((int)(100 * _e.FertilizeC(idx_sel).Resources)).ToString();
            eUI.LeftEnvEs.Envs[ResourceTypes.Wood].TextUI.text = ((int)(100 * _e.AdultForestC(idx_sel).Resources)).ToString();
            eUI.LeftEnvEs.Envs[ResourceTypes.Ore].TextUI.text = ((int)(100 * _e.HillC(idx_sel).Resources)).ToString();
        }
    }
}