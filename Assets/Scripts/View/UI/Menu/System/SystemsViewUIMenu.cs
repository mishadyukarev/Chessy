using Chessy.Common.Entity;
using Chessy.Menu.View.UI;

namespace Chessy.Menu
{
    public class SystemsViewUIMenu : IUpdate
    {
        readonly ConnectorMenuUIS _connectorMenuS;

        readonly EntitiesModelCommon _eMCommon;
        readonly EntitiesViewUIMenu _eUIMenu;


        public SystemsViewUIMenu(in EntitiesViewUIMenu eUIMenu, in EntitiesModelCommon eMCommon)
        {
            _eMCommon = eMCommon;
            _eUIMenu = eUIMenu;

            _connectorMenuS = new ConnectorMenuUIS();
        }


        public void Update()
        {
            _connectorMenuS.Run(_eUIMenu);
        }
    }
}

