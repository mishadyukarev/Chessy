using Chessy.Common;
using Chessy.Common.Enum;
using Chessy.Game;
using Chessy.Game.EventsUI;
using Chessy.Game.System.Model;
using Chessy.Game.System.View;
using Chessy.Game.System.View.UI;
using Chessy.Game.System.View.UI.Center;
using Chessy.Menu;
using ECS;
using System;
using UnityEngine;

namespace Chessy
{
    sealed class Main : MonoBehaviour
    {
        [SerializeField] TestModes testMode = default;

        float _timer;


        #region Common

        Common.Entity.EntitiesModel _eC;
        Common.Entity.View.EntitiesView _eVC;

        #endregion


        #region Menu

        readonly SystemsManager SystemsManager = default;

        #endregion


        #region Game

        Game.Entity.Model.EntitiesModel _eGame;
        EntitiesView _eV;
        Game.EntitiesViewUI _eUI;

        readonly SystemsModel _systemsM = default;
        readonly SystemsViewUI _systemUI = new SystemsViewUI(default);
        readonly SystemsView _systemsV = default;

        #endregion



        void Start()
        {
            _eC = new Common.Entity.EntitiesModel(testMode);
            _eVC = new Common.Entity.View.EntitiesView(transform, testMode);


            var bookE = _eVC.BookE;
            bookE.ExitButtonC.AddListener(delegate
            {
                _eC.IsOpenedBook = false;
                _eVC.Sound(Common.Enum.ClipTypes.CloseBook).Play();
            });

            bookE.NextButtonC.AddListener(delegate
            {
                if (_eC.CurrentPageBookT < PageBoookTypes.End - 1)
                {
                    _eC.CurrentPageBookT++;
                    _eVC.Sound(Common.Enum.ClipTypes.ShiftBookSheet).Play();
                }
            });

            bookE.BackButtonC.AddListener(delegate
            {
                if (_eC.CurrentPageBookT > 0)
                {
                    _eC.CurrentPageBookT--;
                    _eVC.Sound(Common.Enum.ClipTypes.ShiftBookSheet).Play();
                }
            });


            //_eC.IsOpenedBook = !_eC.IsOpenedBook;
            //e.Sound(eC.IsOpenedBook ? ClipTypes.OpenBook : ClipTypes.CloseBook).Invoke();





            new Common.CreateSs(ToggleScene);
            new Common.CreateVSs(gameObject);

            ToggleScene(SceneTypes.Menu);
        }

        void Update()
        {
            new SyncBookUIS().Sync(_eVC.BookE, _eC);


            switch (CurSceneC.Scene)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    break;

                case SceneTypes.Game:
                    _systemsM.UpdateS.Run(_systemsM, _eGame);

                    _timer += Time.deltaTime;
                    if (_timer >= 0.04f)
                    {
                        _systemsV.UpdateS.Run(_systemsV, _eV, _eGame);
                        _systemUI.UpdateS.Run(_timer, _systemUI, _eC, _eUI, _eGame);
                        _timer = 0;
                    }
                    break;

                default:
                    throw new Exception();
            }
        }


        void FixedUpdate()
        {
            Common.DataSC.RunUpdate();

            switch (CurSceneC.Scene)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    SystemsManager.SyncS.Run(_eC);
                    SystemsManager.ConnectorMenuS.Run();
                    break;

                case SceneTypes.Game:

                    break;

                default:
                    throw new Exception();
            }
        }


        void ToggleScene(SceneTypes newScene)
        {
            if (CurSceneC.Is(newScene)) throw new Exception("Need other scene");

            CurSceneC.Scene = newScene;
            switch (newScene)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    {
                        new EntitieManager(_eVC, _eC);
                        SystemsManager.LaunchLikeGameAndShopS.Run();
                        new Menu.Events();
                        break;
                    }

                case SceneTypes.Game:
                    {
                        _eC.IsOpenedBook = true;
                        _eC.CurrentPageBookT = PageBoookTypes.Main;

                        _eV = new EntitiesView(out var forData);
                        _eGame = new Game.Entity.Model.EntitiesModel(forData, Rpc.NamesMethods);
                        _eUI = new Game.EntitiesViewUI(_eVC, _eGame);

                        var eventsUI = new EventsUIManager(_eC, _systemsM, _eUI, _eGame);

                        _eV.EntityVPool.Photon.AddComponent<Rpc>().GiveData(_systemsM, _eGame, eventsUI);
                        Rpc.SyncAllMaster();

                        break;
                    }
                default: throw new Exception();
            }
        }
    }
}