using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.View.UI.Entity;

namespace Chessy.View.UI
{
    sealed class SyncSettingsUIS : SystemUIAbstract
    {
        readonly SettingsUIE _settingsUIE;

        internal SyncSettingsUIS(in SettingsUIE settingsUIE, in EntitiesModel eM) : base(eM)
        {
            _settingsUIE = settingsUIE;
        }

        internal override void Sync()
        {
            _settingsUIE.ParentGOC.TrySetActive(_e.SettingsC.IsOpenedBarWithSettings);
        }
    }
}