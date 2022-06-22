using Chessy.Common.Entity;
using Chessy.Common.Enum;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

namespace Chessy.Common.Model.System
{
    public sealed partial class SystemsModelCommon : IUpdate
    {
        readonly EntitiesModelCommon _eMC;

        public SystemsModelCommon(in EntitiesModelCommon eMC)
        {
            _eMC = eMC;

            Application.runInBackground = true;

            var nowTime = DateTime.Now;
            _eMC.AdC = new AdC(nowTime);

            _eMC.PageBookT = PageBookTypes.None;

            _eMC.SceneT = SceneTypes.Menu;
        }

        public void Update()
        {

        }


        public void ToggleScene(in SceneTypes newSceneT)
        {
            _eMC.SceneT = newSceneT;

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

        public void OnLeftRoom()
        {
            _eMC.SceneT = SceneTypes.Menu;
            _eMC.NeedUpdateView = true;
        }

        public void OnPlayerLeftRoom(in Player otherPlayer)
        {
            if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
        }
    }
}