using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Entity.View;
using Chessy.Common.Enum;
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
using UnityEngine;

namespace Chessy
{
    sealed class Main : MonoBehaviour
    {
        [SerializeField] TestModes testMode = default;

        float _timer;


        #region Common

        EntitiesModelCommon _eMC;
        EntitiesViewCommon _eVC;
        EntitiesViewUICommon _eUIC;


        SystemsViewUICommon _sUIC = default;

        #endregion


        #region Menu

        EntitiesModelMenu _eMM;
        EntitiesViewMenu _eVM;
        EntitiesViewUIMenu _eUIM;

        readonly SystemsModelMenu _sMM = default;

        #endregion


        #region Game

        EntitiesModelGame _eMGame;
        EntitiesViewGame _eV;
        EntitiesViewUIGame _eUI;

        readonly SystemsModelGame _systemsM = default;
        readonly SystemsViewUI _systemUI = new SystemsViewUI(default);
        readonly SystemsView _systemsV = default;

        #endregion



        void Start()
        {
            #region Entity

            _eVC = new EntitiesViewCommon(transform, testMode, out var sound, out var commonZone);
            _eUIC = new EntitiesViewUICommon(GameObject.Instantiate(Resources.Load<Canvas>("Canvas")), commonZone);
            _eMC = new EntitiesModelCommon(testMode, sound);

            _eVM = new EntitiesViewMenu();
            _eUIM = new EntitiesViewUIMenu(_eUIC);
            _eMM = new EntitiesModelMenu(_eVC);

            _eV = new EntitiesViewGame(out var forData, _eVC);
            _eMGame = new EntitiesModelGame(forData, Rpc.NamesMethods, _eMC);
            _eUI = new EntitiesViewUIGame(_eUIC);

            #endregion


            #region System

            new EventsCommon(_eUIC, _eVC, _eMC);
            new IAPCore(_eUIC.ShopE);
            new MyYodo();
            gameObject.AddComponent<PhotonSceneManager>().StartMy(ToggleScene);
            _eV.PhotonC.PhotonView.gameObject.AddComponent<Rpc>().GiveData(_systemsM, _eMGame, _eMC);

            #endregion


            #region Event

            new EventsMenu(_eMC, _eUIM);
            new EventsUIGame(_eUIC, _eMC, _systemsM, _eUI, _eMGame);

            #endregion
        }

        void Update()
        {
            switch (_eMC.SceneC.Scene)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    break;

                case SceneTypes.Game:
                    _systemsM.UpdateS.Run(_systemsM, _eMGame);

                    _timer += Time.deltaTime;
                    if (_timer >= 0.04f)
                    {
                        _systemsV.UpdateS.Run(_systemsV, _eV, _eMGame, _eVC);
                        _systemUI.UpdateS.Run(_timer, _systemUI, _eMC, _eUI, _eMGame);
                        _timer = 0;
                    }
                    break;

                default:
                    throw new Exception();
            }
        }


        void FixedUpdate()
        {
            _sUIC.SyncBookS.Sync(_eUIC.BookE, _eMC.BookC);
            _sUIC.SyncMusicSoundS.Sync(_eMC, _eVC);
            _sUIC.SyncSettingsS.Sync(_eMC.IsOpenSettings, _eUIC.SettingsE);

            new AdLaunchS().Run(ref _eMC.AdC, _eMC.SceneC);

            switch (_eMC.SceneC.Scene)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    _sMM.SyncS.Run(_eUIC, _eMC);
                    _sMM.ConnectorMenuS.Run(_eUIM);
                    break;

                case SceneTypes.Game:

                    break;

                default:
                    throw new Exception();
            }
        }


        void ToggleScene(SceneTypes newScene)
        {
            if (_eMC.SceneC.Is(newScene)) throw new Exception("Need other scene");

            _eMC.SceneC.Scene = newScene;

            switch (newScene)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    {
                        _eUIC.CanvasE.MenuCanvasGOC.SetActive(true);
                        _eUIC.CanvasE.GameCanvasGOC.SetActive(false);

                        _sMM.LaunchLikeGameAndShopS.Run(ref _eMC.WasLikeGameZone, ref _eMC.TimeStartGameC, _eUIC.ShopE);
                        break;
                    }

                case SceneTypes.Game:
                    {
                        _eUIC.CanvasE.MenuCanvasGOC.SetActive(false);
                        _eUIC.CanvasE.GameCanvasGOC.SetActive(true);

                        _eMC.BookC.IsOpenedBook = true;
                        _eMC.BookC.PageBookT = PageBoookTypes.Main;

                        _eMGame.StartGame(_eMC.GameModeTC);

                        Rpc.SyncAllMaster();
                        break;
                    }
                default: throw new Exception();
            }
        }
    }
}