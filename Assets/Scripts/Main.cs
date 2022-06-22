using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Entity.View;
using Chessy.Common.Model.System;
using Chessy.Common.View.UI;
using Chessy.Common.View.UI.System;
using Chessy.Game;
using Chessy.Game.EventsUI;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.System.View;
using Chessy.Game.System.View.UI;
using Chessy.Menu;
using Chessy.Menu.View.UI;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy
{
    sealed partial class Main : MonoBehaviour
    {
        [SerializeField] TestModes TestModeT = default;

        List<IUpdate> _runs;

        EntitiesModelCommon _eMC;

        void Start()
        {
            #region Common

            var eVCommon = new EntitiesViewCommon(transform, TestModeT, out var sound, out var commonZone, out var actions);
            var eUICommon = new EntitiesViewUICommon(commonZone);
            _eMC = new EntitiesModelCommon(TestModeT, sound);

            var sMCommon = new SystemsModelCommon(_eMC);
            var sUICommon = new SystemsViewUICommon(_eMC, eUICommon);

            new EventsCommon(sMCommon, eUICommon, eVCommon, _eMC);

            #endregion


            #region Menu

            var eVMenu = new EntitiesViewMenu();
            var eUIMenu = new EntitiesViewUIMenu(eUICommon);
            var eMMenu = new EntitiesModelMenu(_eMC);

            var sMMenu = new SystemsModelMenu(_eMC);
            var sUIMenu = new SystemsViewUIMenu(eUIMenu, eMMenu);

            new EventsMenu(_eMC, eUIMenu);

            #endregion


            #region Game

            var eViewGame = new EntitiesViewGame(out var forData, eVCommon);
            var eModelGame = new EntitiesModelGame(_eMC, forData, Rpc.NameRpcMethod, actions);
            var eUIGame = new EntitiesViewUIGame(eUICommon);

            var sModelGame = new SystemsModelGame(sMCommon, eModelGame);
            var sUIGame = new SystemsViewUIGame(_eMC, eUIGame, eModelGame);
            var sViewGame = new SystemsViewGame(eViewGame, eModelGame, eVCommon);

            var eventsGame = new EventsUIGame(eUICommon, _eMC, sModelGame, eUIGame, eModelGame);

            #endregion


            #region NeedReplace

            var adLaunchS = new TryLaunchAdS(_eMC);
            new ShopS(_eMC);

            var rpc = eVCommon.PhotonC.PhotonView.gameObject.AddComponent<Rpc>().GiveData(sModelGame);
            gameObject.AddComponent<PhotonSceneManager>().StartMy(sUICommon, sModelGame);

            #endregion


            _runs = new List<IUpdate>()
            {
                sMCommon,
                adLaunchS,
                sMMenu,
                sModelGame,
                eventsGame,

                sUICommon,
                sUIMenu,
                sUIGame,
                sViewGame,
            };


            #region ComeToTraining

            PhotonNetwork.OfflineMode = true;
            _eMC.GameModeT = GameModeTypes.TrainingOffline;
            PhotonNetwork.CreateRoom(default);

            #endregion
        }

        void Update()
        {
            _runs.ForEach((IUpdate iRun) => iRun.Update());

            _eMC.NeedUpdateView = false;
        }
    }
}