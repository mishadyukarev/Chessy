using Chessy.Common.Entity;
using Chessy.Common.Interface;
using System;

namespace Chessy.Common.View.UI.System
{
    public sealed class SystemsViewUICommon : IUpdate, IToggleScene
    {
        readonly EntitiesViewUICommon _eUICommon;

        readonly ToggleSceneUIS _toggleSceneS;

        readonly SyncBookUIS _syncBookS;
        readonly SyncSettingsUIS _syncSettingsS;


        public SystemsViewUICommon(in EntitiesModelCommon eMC, in EntitiesViewUICommon eUIC)
        {
            _eUICommon = eUIC;

            _toggleSceneS = new ToggleSceneUIS(eUIC);

            _syncBookS = new SyncBookUIS(eUIC.BookE, eMC);
            _syncSettingsS = new SyncSettingsUIS(_eUICommon.SettingsE, eMC);
        }

        public void Update()
        {
            _syncBookS.Sync();
            _syncSettingsS.Sync();
        }

        public void ToggleScene(in SceneTypes newSceneT)
        {
            _toggleSceneS.ToggleScene(newSceneT);
        }
    }
}