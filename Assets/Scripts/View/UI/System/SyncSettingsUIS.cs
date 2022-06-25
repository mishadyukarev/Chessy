using Chessy.Model;

namespace Chessy.Common.View.UI.System
{
    sealed class SyncSettingsUIS : SyncUISystem
    {
        readonly SettingsUIE _settingsUIE;

        internal SyncSettingsUIS(in SettingsUIE settingsUIE, in EntitiesModel eM) : base(eM)
        {
            _settingsUIE = settingsUIE;
        }

        internal override void Sync()
        {
            _settingsUIE.ParentGOC.SetActive(_e.SettingsC.IsOpenedBarWithSettings);
        }
    }
}