using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Entity.View;
using Chessy.Common.Enum;
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
using UnityEngine;

namespace Chessy
{
    sealed class Main : MonoBehaviour
    {
        [SerializeField] TestModes testMode = default;


        #region Entity

        EntitiesModelCommon _eMCommon;
        EntitiesViewCommon _eVCommon;
        EntitiesViewUICommon _eUICommon;

        EntitiesModelMenu _eMM;
        EntitiesViewMenu _eVM;
        EntitiesViewUIMenu _eUIM;

        EntitiesModelGame _eMGame;
        EntitiesViewGame _eVGame;
        EntitiesViewUIGame _eUIGame;

        #endregion


        #region System

        readonly SystemsModelCommon _sMCommon = default;
        readonly SystemsViewUICommon _sUICommon = default;

        readonly SystemsModelMenu _sMMenu = default;

        readonly SystemsModelGame _sMGame = default;
        SystemsViewUIGame _sUIGame;
        SystemsViewGame _sVGame;

        #endregion


        void Start()
        {
            #region Entity

            _eVCommon = new EntitiesViewCommon(transform, testMode, out var sound, out var commonZone);
            _eUICommon = new EntitiesViewUICommon(GameObject.Instantiate(Resources.Load<Canvas>("Canvas")), commonZone);
            _eMCommon = new EntitiesModelCommon(testMode, sound);

            _eVM = new EntitiesViewMenu();
            _eUIM = new EntitiesViewUIMenu(_eUICommon);
            _eMM = new EntitiesModelMenu(_eVCommon);

            _eVGame = new EntitiesViewGame(out var forData, _eVCommon);
            _eMGame = new EntitiesModelGame(forData, Rpc.NamesMethods, _eMCommon);
            _eUIGame = new EntitiesViewUIGame(_eUICommon);

            #endregion


            #region System

            _sUIGame = new SystemsViewUIGame(_eMCommon, _eUIGame, _eMGame);
            _sVGame = new SystemsViewGame(_eVGame, _eMGame, _eVCommon);

            new EventsCommon(_eUICommon, _eVCommon, _eMCommon);
            new IAPCore(_eUICommon.ShopE);
            new MyYodo();
            gameObject.AddComponent<PhotonSceneManager>().StartMy(ToggleScene);
            _eVGame.PhotonC.PhotonView.gameObject.AddComponent<Rpc>().GiveData(_sMGame, _eMGame, _eMCommon);

            #endregion


            #region Event

            new EventsMenu(_eMCommon, _eUIM);
            new EventsUIGame(_eUICommon, _eMCommon, _sMGame, _eUIGame, _eMGame);

            #endregion
        }

        void Update()
        {
            switch (_eMCommon.SceneC.Scene)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    break;

                case SceneTypes.Game:
                    _sMGame.UpdateS.Run(_sMGame, _eMGame);

                    _eMGame.TimerUpdateUIC.Timer += Time.deltaTime;
                    if (_eMGame.TimerUpdateUIC.Timer >= 0.04f)
                    {
                        _sVGame.UpdateS.Run();
                        _sUIGame.UpdateS.Run();
                        _eMGame.TimerUpdateUIC.Timer = 0;
                    }
                    break;

                default:
                    throw new Exception();
            }


            #region NeedReplace



            #endregion
        }


        void FixedUpdate()
        {
            _sUICommon.UpdateS.Update(_eMCommon, _eVCommon, _eUICommon, _sUICommon); 

            new AdLaunchS().Run(ref _eMCommon.AdC, _eMCommon.SceneC);

            switch (_eMCommon.SceneC.Scene)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    _sMMenu.SyncS.Run(_eUICommon, _eMCommon);
                    _sMMenu.ConnectorMenuS.Run(_eUIM);
                    break;

                case SceneTypes.Game:

                    break;

                default:
                    throw new Exception();
            }
        }


        void ToggleScene(SceneTypes newScene)
        {
            if (_eMCommon.SceneC.Is(newScene)) throw new Exception("Need other scene");

            _eMCommon.SceneC.Scene = newScene;

            switch (newScene)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    {
                        _eUICommon.CanvasE.MenuCanvasGOC.SetActive(true);
                        _eUICommon.CanvasE.GameCanvasGOC.SetActive(false);

                        _sMMenu.LaunchLikeGameAndShopS.Run(ref _eMCommon.WasLikeGameZone, ref _eMCommon.TimeStartGameC, _eUICommon.ShopE);
                        break;
                    }

                case SceneTypes.Game:
                    {
                        _eUICommon.CanvasE.MenuCanvasGOC.SetActive(false);
                        _eUICommon.CanvasE.GameCanvasGOC.SetActive(true);

                        _eMCommon.BookC.IsOpenedBook = true;
                        _eMCommon.BookC.PageBookT = PageBoookTypes.Main;

                        _eMGame.StartGame(_eMCommon.GameModeTC);

                        Rpc.SyncAllMaster();
                        break;
                    }
                default: throw new Exception();
            }
        }
    }
}