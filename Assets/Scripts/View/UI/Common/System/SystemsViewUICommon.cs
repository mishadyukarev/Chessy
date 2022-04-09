using Chessy.Common.Entity;
using Chessy.Common.Interface;
using System;

namespace Chessy.Common.View.UI.System
{
    public sealed class SystemsViewUICommon : IUpdate, IToggleScene
    {
        readonly EntitiesViewUICommon _eUIC;
        readonly EntitiesModelCommon _eMC;

        readonly ToggleSceneUIS _toggleSceneS;

        readonly SyncBookUIS _syncBookS;
        readonly SyncSettingsUIS _syncSettingsS;


        public SystemsViewUICommon(in EntitiesModelCommon eMC, in EntitiesViewUICommon eUIC)
        {
            _eMC = eMC;
            _eUIC = eUIC;

            _toggleSceneS = new ToggleSceneUIS(eUIC);

            _syncBookS = new SyncBookUIS(eUIC.BookE, eMC);
            _syncSettingsS = new SyncSettingsUIS(_eUIC.SettingsE, eMC);
        }

        public void Update()
        {
            _syncBookS.Sync();
            _syncSettingsS.Sync();

            _eUIC.ShopE.ShopZoneGOC.SetActive(_eMC.ShopC.IsOpenedShopZone);


        }

        public void ToggleScene(in SceneTypes newSceneT)
        {
            _toggleSceneS.ToggleScene(newSceneT);
        }
    }
}