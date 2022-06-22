using Chessy.Common.Entity;

namespace Chessy.Common.View.UI.System
{
    public sealed class SystemsViewUICommon : IUpdate
    {
        readonly EntitiesViewUICommon _eUIC;
        readonly EntitiesModelCommon _eMC;

        readonly SyncBookUIS _syncBookS;
        readonly SyncSettingsUIS _syncSettingsS;


        public SystemsViewUICommon(in EntitiesModelCommon eMC, in EntitiesViewUICommon eUIC)
        {
            _eMC = eMC;
            _eUIC = eUIC;

            _syncBookS = new SyncBookUIS(eUIC.BookE, eMC);
            _syncSettingsS = new SyncSettingsUIS(_eUIC.SettingsE, eMC);
        }

        public void Update()
        {
            if (_eMC.NeedUpdateView)
            {
                _syncBookS.Sync();
                _syncSettingsS.Sync();

                _eUIC.ShopE.ShopZoneGOC.SetActive(_eMC.ShopC.IsOpenedShopZone);
            }
        }
    }
}