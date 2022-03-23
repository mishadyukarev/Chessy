using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Entity.View;
using Chessy.Common.Interface;
using Chessy.Common.Model.System;
using Chessy.Common.View.UI;
using Chessy.Common.View.UI.System;
using Chessy.Game;
using Chessy.Game.Entity.Model;
using Chessy.Game.EventsUI;
using Chessy.Game.System.Model;
using Chessy.Game.System.View;
using Chessy.Game.System.View.UI;
using Chessy.Menu;
using Chessy.Menu.View.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy
{
    sealed class Main : MonoBehaviour
    {
        [SerializeField] TestModes TestMode = default;


        #region Entity

        EntitiesModelCommon _eMCommon;
        EntitiesViewUICommon _eUICommon;

        EntitiesViewUIMenu _eUIM;

        EntitiesModelGame _eMGame;
        EntitiesViewGame _eVGame;
        EntitiesViewUIGame _eUIGame;

        #endregion


        #region System

        List<IEcsRunSystem> _runCommon;
        List<IEcsRunSystem> _runMenu;
        List<IEcsRunSystem> _runsGame;

        List<IToggleScene> _togglerScenes;

        SystemsModelMenu _sMMenu;

        #endregion


        void Start()
        {
            #region Entity

            var eVCommon = new EntitiesViewCommon(transform, TestMode, out var sound, out var commonZone);
            _eUICommon = new EntitiesViewUICommon(GameObject.Instantiate(Resources.Load<Canvas>("Canvas")), commonZone);
            _eMCommon = new EntitiesModelCommon(TestMode, sound);

            var eVM = new EntitiesViewMenu();
            _eUIM = new EntitiesViewUIMenu(_eUICommon);
            var eMM = new EntitiesModelMenu();

            _eVGame = new EntitiesViewGame(out var forData, eVCommon);
            _eMGame = new EntitiesModelGame(forData, Rpc.NamesMethods, _eMCommon);
            _eUIGame = new EntitiesViewUIGame(_eUICommon);

            #endregion


            #region System

            var sMCommon = new SystemsModelCommon();
            var sUICommon = new SystemsViewUICommon(_eMCommon, eVCommon, _eUICommon);
            _runCommon = new List<IEcsRunSystem>()
            {
                sMCommon,
                sUICommon,
            };

            _sMMenu = new SystemsModelMenu();
            _runMenu = new List<IEcsRunSystem>()
            {

            };

            var sMGame = new SystemsModelGame(_eMGame);
            var sUIGame = new SystemsViewUIGame(_eMCommon, _eUIGame, _eMGame);
            var sVGame = new SystemsViewGame(_eVGame, _eMGame, eVCommon);
            _runsGame = new List<IEcsRunSystem>()
            {
                sMGame,
                sVGame,
                sUIGame,
            };

            _togglerScenes = new List<IToggleScene>()
            {
                _eMCommon,
                _eUICommon
            };


            #region NeedReplace

            new EventsCommon(_eUICommon, eVCommon, _eMCommon);
            new IAPCore(_eUICommon.ShopE);
            new MyYodo();
            gameObject.AddComponent<PhotonSceneManager>().StartMy(ToggleScene);
            _eVGame.PhotonC.PhotonView.gameObject.AddComponent<Rpc>().GiveData(sMGame, _eMGame, _eMCommon);

            #endregion

            #endregion


            #region Event

            new EventsMenu(_eMCommon, _eUIM);
            new EventsUIGame(_eUICommon, _eMCommon, sMGame, _eUIGame, _eMGame);

            #endregion
        }

        void Update()
        {
            _runCommon.ForEach((IEcsRunSystem iRun) => iRun.Run());

            new AdLaunchS().Run(ref _eMCommon.AdC, _eMCommon.SceneC);

            switch (_eMCommon.SceneC.Scene)
            {
                case SceneTypes.Menu:
                    _runMenu.ForEach((IEcsRunSystem iRun) => iRun.Run());
                    _sMMenu.SyncS.Run(_eUICommon, _eMCommon);
                    _sMMenu.ConnectorMenuS.Run(_eUIM);
                    break;

                case SceneTypes.Game:
                    _runsGame.ForEach((IEcsRunSystem iRun) => iRun.Run());
                    break;

                default:
                    throw new Exception();
            }
        }

        void ToggleScene(SceneTypes newSceneT)
        {
            _togglerScenes.ForEach((IToggleScene iToggleScene) => iToggleScene.ToggleScene(newSceneT));

            switch (newSceneT)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    {
                        _sMMenu.LaunchLikeGameAndShopS.Run(ref _eMCommon.WasLikeGameZone, ref _eMCommon.TimeStartGameC, _eUICommon.ShopE);
                        break;
                    }

                case SceneTypes.Game:
                    {
                        _eMGame.StartGame(_eMCommon.GameModeTC);

                        Rpc.SyncAllMaster();
                        break;
                    }
                default: throw new Exception();
            }
        }
    }
}