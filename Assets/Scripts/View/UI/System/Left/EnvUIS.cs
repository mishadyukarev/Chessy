using Chessy.Model.Entity;
using Chessy.View.UI.Entity;
namespace Chessy.Model
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
            var idx_sel = IndexesCellsC.Selected;

            eUI.LeftEnvEs.Envs[ResourceTypes.Food].TextUI.text = ((int)(100 * _environmentCs[idx_sel].Resources(EnvironmentTypes.Fertilizer))).ToString();
            eUI.LeftEnvEs.Envs[ResourceTypes.Wood].TextUI.text = ((int)(100 * _environmentCs[idx_sel].Resources(EnvironmentTypes.AdultForest))).ToString();
            eUI.LeftEnvEs.Envs[ResourceTypes.Ore].TextUI.text = ((int)(100 * _environmentCs[idx_sel].Resources(EnvironmentTypes.Hill))).ToString();
        }
    }
}