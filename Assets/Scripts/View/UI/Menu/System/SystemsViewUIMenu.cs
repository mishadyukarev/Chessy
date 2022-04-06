using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.View.UI;
using Chessy.Menu.View.UI;

namespace Chessy.Menu
{
    public sealed class SystemsViewUIMenu : IUpdate
    {
        public readonly ConnectorMenuS ConnectorMenuS;

        readonly EntitiesModelCommon _eMCommon;
        readonly EntitiesViewUIMenu _eUIMenu;


        public SystemsViewUIMenu(in EntitiesViewUIMenu eUIMenu, in EntitiesModelCommon eMCommon)
        {
            _eMCommon = eMCommon;
            _eUIMenu = eUIMenu;
        }


        public void Update()
        {

            //SyncS.Run(_eUICommon, _eMCommon);
            ConnectorMenuS.Run(_eUIMenu);

        }
    }
}

