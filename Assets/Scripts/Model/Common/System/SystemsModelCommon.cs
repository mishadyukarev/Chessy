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
        readonly EntitiesModelCommon _e;

        public SystemsModelCommon(in EntitiesModelCommon eMC)
        {
            _e = eMC;

            Application.runInBackground = true;

            var nowTime = DateTime.Now;
            _e.AdC = new AdC(nowTime);

            _e.OpenedNowPageBookT = PageBookTypes.None;

            _e.SceneT = SceneTypes.Menu;
        }

        public void Update()
        {
            _e.ForUpdateViewTimer += Time.deltaTime;

            if (_e.ForUpdateViewTimer >= 0.5f)
            {
                _e.NeedUpdateView = true;
                _e.ForUpdateViewTimer = 0;
            }
        }


        public void ToggleScene(in SceneTypes newSceneT)
        {
            _e.SceneT = newSceneT;

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
            _e.SceneT = SceneTypes.Menu;
            _e.NeedUpdateView = true;
        }

        public void OnPlayerLeftRoom(in Player otherPlayer)
        {
            if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
        }
    }
}