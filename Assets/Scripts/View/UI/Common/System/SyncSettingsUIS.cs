using Chessy.Common.Entity;

namespace Chessy.Common.View.UI.System
{
    sealed class SyncSettingsUIS : SyncUISystem
    {
        readonly SettingsUIE _settingsUIE;

        internal SyncSettingsUIS(in SettingsUIE settingsUIE, in EntitiesModelCommon eMC) : base(eMC)
        {
            _settingsUIE = settingsUIE;
        }

        internal override void Sync()
        {
            _settingsUIE.ParentGOC.SetActive(e.IsOpenSettings);
        }
    }
}