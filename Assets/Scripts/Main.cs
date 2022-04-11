using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Entity.View;
using Chessy.Common.Interface;
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
    sealed class Main : MonoBehaviour
    {
        [SerializeField] TestModes TestModeT = default;

        List<IUpdate> _commonRuns;
        List<IUpdate> _gameRuns;
        List<IUpdate> _menuRuns;

        EntitiesModelCommon _eMC;

        void Start()
        {
            #region Common

            var eVCommon = new EntitiesViewCommon(transform, TestModeT, out var sound, out var commonZone, out var actions);
            var eUICommon = new EntitiesViewUICommon(commonZone);
            _eMC = new EntitiesModelCommon(sound);

            var sMCommon = new SystemsModelCommon(TestModeT, _eMC);
            var sUICommon = new SystemsViewUICommon(_eMC, eUICommon);

            new EventsCommon(sMCommon, eUICommon, eVCommon, _eMC);

            #endregion


            #region Menu

            var eVMenu = new EntitiesViewMenu();
            var eUIMenu = new EntitiesViewUIMenu(eUICommon);
            var eMMenu = new EntitiesModelMenu();

            var sMMenu = new SystemsModelMenu(_eMC);
            var sUIMenu = new SystemsViewUIMenu(eUIMenu, _eMC);

            new EventsMenu(_eMC, eUIMenu);

            #endregion


            #region Game

            var eViewGame = new EntitiesViewGame(out var forData, eVCommon);
            var eModelGame = new EntitiesModelGame(_eMC, forData, Rpc.NamesMethods_S, actions);
            var eUIGame = new EntitiesViewUIGame(eUICommon);

            var sModelGame = new SystemsModelGame(sMCommon, _eMC, eModelGame);
            var sUIGame = new SystemsViewUIGame(_eMC, eUIGame, eModelGame);
            var sViewGame = new SystemsViewGame(eViewGame, eModelGame, eVCommon, _eMC);

            new EventsUIGame(eUICommon, _eMC, sModelGame, eUIGame, eModelGame);

            #endregion


            #region NeedReplace

            var adLaunchS = new TryLaunchAdS(_eMC);
            new ShopS(_eMC);

            var rpc = eVCommon.PhotonC.PhotonView.gameObject.AddComponent<Rpc>().GiveData(sModelGame, eModelGame, _eMC);

            var togglerScenes = new List<IToggleScene>()
            {
                sMCommon,
                sUICommon,
                sModelGame,
                rpc,
            };



            gameObject.AddComponent<PhotonSceneManager>().StartMy(rpc, togglerScenes);

            #endregion


            _commonRuns = new List<IUpdate>()
            {
                sMCommon,
                sUICommon,
                adLaunchS,
            };

            _menuRuns = new List<IUpdate>()
            {
                sMMenu,
                sUIMenu,
            };

            _gameRuns = new List<IUpdate>()
            {
                sModelGame,
                sViewGame,
                sUIGame,
            };

            #region ComeToTraining

            PhotonNetwork.OfflineMode = true;
            _eMC.GameModeT = GameModeTypes.TrainingOff;
            PhotonNetwork.CreateRoom(default);

            #endregion
        }

        void Update()
        {
            _commonRuns.ForEach((IUpdate iRun) => iRun.Update());

            switch (_eMC.SceneTC.SceneT)
            {
                case SceneTypes.Menu:
                    _menuRuns.ForEach((IUpdate iRun) => iRun.Update());
                    break;

                case SceneTypes.Game:
                    _gameRuns.ForEach((IUpdate iRun) => iRun.Update());
                    break;

                default: throw new Exception();
            }
        }


        
    }
}