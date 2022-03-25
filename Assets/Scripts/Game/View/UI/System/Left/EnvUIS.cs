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
            var idx_sel = eMGame.CellsC.Selected;

            eUI.LeftEnvEs.Envs[ResourceTypes.Food].TextUI.text = ((int)(100 * eMGame.FertilizeC(idx_sel).Resources)).ToString();
            eUI.LeftEnvEs.Envs[ResourceTypes.Wood].TextUI.text = ((int)(100 * eMGame.AdultForestC(idx_sel).Resources)).ToString();
            eUI.LeftEnvEs.Envs[ResourceTypes.Ore].TextUI.text = ((int)(100 * eMGame.HillC(idx_sel).Resources)).ToString();
        }
    }
}