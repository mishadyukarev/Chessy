using Chessy.Common.Entity;
using Chessy.Common.Enum;
using Chessy.Common.Interface;
using System;

namespace Chessy.Common.Model.System
{
    public sealed class SystemsModelCommon : IUpdate, IToggleScene
    {
        readonly EntitiesModelCommon _eMCommon;
        readonly AdLaunchS _adLaunchS;

        public BuyPremiumProductS BuyProductS;

        public SystemsModelCommon(in EntitiesModelCommon eMCommon)
        {
            _eMCommon = eMCommon;

            _adLaunchS = new AdLaunchS(eMCommon);

            BuyProductS = new BuyPremiumProductS(eMCommon);
        }

        public void Update()
        {
            _adLaunchS.Update();
        }


        public void ToggleScene(in SceneTypes newSceneT)
        {
            _eMCommon.SceneTC.SceneT = newSceneT;

            switch (newSceneT)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    {
                        break;
                    }

                case SceneTypes.Game:
                    {
                        _eMCommon.IsOpenedBook = true;
                        _eMCommon.PageBookTC.PageBookT = PageBookTypes.Main;
                        break;
                    }
                default: throw new Exception();
            }
        }
    }
}