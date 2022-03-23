using Chessy.Common.Entity;
using Chessy.Common.Entity.View;
using Chessy.Common.View.System;
using Chessy.Game.System.View.UI.Center;

namespace Chessy.Common.View.UI.System
{
    public readonly struct SystemsViewUICommon : IEcsRunSystem
    {
        readonly EntitiesModelCommon _eMC;
        readonly EntitiesViewCommon _eVC;
        readonly EntitiesViewUICommon _eUIC;

        public readonly SyncMusicSoundVS SyncMusicSoundS;
        public readonly SyncBookUIS SyncBookS;
        public readonly SyncSettingsUIS SyncSettingsS;


        public SystemsViewUICommon(in EntitiesModelCommon eMC, in EntitiesViewCommon eVC, in EntitiesViewUICommon eUIC)
        {
            _eMC = eMC;
            _eVC = eVC;
            _eUIC = eUIC;
        }

        public void Run()
        {
            SyncBookS.Sync(_eUIC.BookE, _eMC.BookC);
            SyncMusicSoundS.Sync(_eMC, _eVC);
            SyncSettingsS.Sync(_eMC.IsOpenSettings, _eUIC.SettingsE);
        }
    }
}