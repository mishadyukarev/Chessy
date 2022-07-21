using Chessy.Model.Entity;
using Chessy.View.UI.Entity; namespace Chessy.Model
{
    sealed class EnvUIS : SystemUIAbstract
    {
        readonly EntitiesViewUI eUI;

        internal EnvUIS(in EntitiesViewUI entsUI, in EntitiesModel ents) : base(ents)
        {
            eUI = entsUI;
        }

        internal override void Sync()
        {
            var idx_sel = _cellsC.Selected;

            eUI.LeftEnvEs.Envs[ResourceTypes.Food].TextUI.text = ((int)(100 * _e.WaterOnCellC(idx_sel).ResourcesP)).ToString();
            eUI.LeftEnvEs.Envs[ResourceTypes.Wood].TextUI.text = ((int)(100 * _e.AdultForestC(idx_sel).ResourcesP)).ToString();
            eUI.LeftEnvEs.Envs[ResourceTypes.Ore].TextUI.text = ((int)(100 * _e.HillC(idx_sel).ResourcesP)).ToString();
        }
    }
}