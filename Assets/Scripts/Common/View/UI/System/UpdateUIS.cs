using Chessy.Common.Entity;
using Chessy.Common.Entity.View;

namespace Chessy.Common.View.UI.System
{
    public struct UpdateUIS
    {
        public void Update(in EntitiesModelCommon eMC, in EntitiesViewCommon eVC, in EntitiesViewUICommon eUIC, in SystemsViewUICommon sUIC)
        {
            sUIC.SyncBookS.Sync(eUIC.BookE, eMC.BookC);
            sUIC.SyncMusicSoundS.Sync(eMC, eVC);
            sUIC.SyncSettingsS.Sync(eMC.IsOpenSettings, eUIC.SettingsE);
        }
    }
}