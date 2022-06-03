using Chessy.Common.Entity;
using Chessy.Common.Enum;
using Chessy.Common.Interface;
using System;
using UnityEngine;

namespace Chessy.Common.Model.System
{
    public sealed class SystemsModelCommon : IUpdate
    {
        readonly EntitiesModelCommon _eMC;

        public BuyPremiumProductS BuyProductS;

        public SystemsModelCommon(in TestModes testModeT, in EntitiesModelCommon eMC)
        {
            _eMC = eMC;



            BuyProductS = new BuyPremiumProductS(eMC);

            Application.runInBackground = true;

            var nowTime = DateTime.Now;
            _eMC.AdC = new AdC(nowTime);
            _eMC.TimeStartGameC = new TimeStartGameC(nowTime);
            _eMC.TestModeC = new TestModeC(testModeT);



            _eMC.IsOpenedBook = false;
            _eMC.PageBookT = PageBookTypes.Main;

            _eMC.SceneTC.SceneT = SceneTypes.Menu;
        }

        public void Update()
        {

        }


        public void ToggleScene(in SceneTypes newSceneT)
        {
            _eMC.SceneTC.SceneT = newSceneT;

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
                        //_eMC.IsOpenedBook = true;
                        //_eMC.PageBookTC.PageBookT = PageBookTypes.Main;
                        break;
                    }
                default: throw new Exception();
            }
        }
    }
}