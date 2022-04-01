namespace Chessy.Common.View.UI.System
{
    public struct SyncSettingsUIS
    {
        public void Sync(in bool isOpenSettings, in SettingsUIE settingsUIE)
        {
            settingsUIE.ParentGOC.SetActive(isOpenSettings);
        }
    }
}