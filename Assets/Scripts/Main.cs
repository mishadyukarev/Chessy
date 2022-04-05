using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Entity.View;
using Chessy.Common.Interface;
using Chessy.Common.Model.System;
using Chessy.Common.View.UI;
using Chessy.Common.View.UI.System;
using Chessy.Game;
using Chessy.Game.Model.Entity;
using Chessy.Game.EventsUI;
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

        List<IEcsRunSystem> _commonRuns;
        List<IEcsRunSystem> _gameRuns;
        List<IEcsRunSystem> _menuRuns;

        EntitiesModelCommon _eMC;


        void Start()
        {
            #region Common

            var eVCommon = new EntitiesViewCommon(transform, TestModeT, out var sound, out var commonZone, out var actions);
            var eUICommon = new EntitiesViewUICommon(commonZone);
            _eMC = new EntitiesModelCommon(TestModeT, sound);

            var sMCommon = new SystemsModelCommon(_eMC);
            var sUICommon = new SystemsViewUICommon(_eMC, eVCommon, eUICommon);

            new EventsCommon(eUICommon, eVCommon, _eMC);

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
            var eModelGame = new EntitiesModelGame(forData, Rpc.NamesMethods_S, actions);
            var eUIGame = new EntitiesViewUIGame(eUICommon);

            var sModelGame = new SystemsModelGame(sMCommon, _eMC, eModelGame);
            var sUIGame = new SystemsViewUIGame(_eMC, eUIGame, eModelGame);
            var sViewGame = new SystemsViewGame(eViewGame, eModelGame, eVCommon, _eMC);

            new EventsUIGame(eUICommon, _eMC, sModelGame, eUIGame, eModelGame);

            #endregion


            #region NeedReplace

            var go = new GameObject();

            var rpc = eVCommon.PhotonC.PhotonView.gameObject.AddComponent<Rpc>().GiveData(sModelGame, eModelGame, _eMC);


            new IAPCore(_eMC);
            new MyYodo();

            var togglerScenes = new List<IToggleScene>()
            {
                sMCommon,
                sUICommon,
                sModelGame,
                rpc,
            };

            gameObject.AddComponent<PhotonSceneManager>().StartMy(rpc, togglerScenes);

            #endregion


            _commonRuns = new List<IEcsRunSystem>()
            {
                sMCommon,
                sUICommon,
            };

            _menuRuns = new List<IEcsRunSystem>()
            {
                sMMenu,
                sUIMenu,
            };

            _gameRuns = new List<IEcsRunSystem>()
            {
                sModelGame,
                sViewGame,
                sUIGame,
            };



            #region ComeToTraining

            _eMC.VolumeMusic = eUICommon.SettingsE.SliderC.Slider.value;

            PhotonNetwork.OfflineMode = true;
            _eMC.GameModeTC.GameModeT = GameModes.TrainingOff;
            PhotonNetwork.CreateRoom(default);

            #endregion
        }

        void Update()
        {
            _commonRuns.ForEach((IEcsRunSystem iRun) => iRun.Run());

            switch (_eMC.SceneTC.Scene)
            {
                case SceneTypes.Menu:
                    _menuRuns.ForEach((IEcsRunSystem iRun) => iRun.Run());
                    break;

                case SceneTypes.Game:
                    _gameRuns.ForEach((IEcsRunSystem iRun) => iRun.Run());
                    break;

                default: throw new Exception();
            }
        }
    }
}