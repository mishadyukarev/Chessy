using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Menu.View.UI;

namespace Chessy.Menu
{
    public class SystemsViewUIMenu : IUpdate
    {
        readonly ConnectorMenuUIS _connectorMenuS;

        readonly EntitiesModelMenu _eMM;
        readonly EntitiesViewUIMenu _eUIMenu;


        public SystemsViewUIMenu(in EntitiesViewUIMenu eUIMenu, in EntitiesModelMenu eMM)
        {
            _eMM = eMM;
            _eUIMenu = eUIMenu;

            _connectorMenuS = new ConnectorMenuUIS();
        }


        public void Update()
        {
            if (_eMM.Common.NeedUpdateView)
            {
                _eUIMenu.ParentGOC.SetActive(_eMM.Common.SceneT == SceneTypes.Menu);

                if (_eMM.Common.SceneT == SceneTypes.Menu)
                {
                    _connectorMenuS.Run(_eUIMenu);
                }
            }
        }
    }
}

