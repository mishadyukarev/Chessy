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
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy
{
    sealed class Main : MonoBehaviour
    {
        [SerializeField] TestModes TestModeT = default;

        List<IEcsRunSystem> _runs;


        void Start()
        {
            #region Common

            var eVCommon = new EntitiesViewCommon(transform, TestModeT, out var sound, out var commonZone);
            var eUICommon = new EntitiesViewUICommon(commonZone);
            var eMCommon = new EntitiesModelCommon(TestModeT, sound);

            var sMCommon = new SystemsModelCommon(eMCommon);
            var sUICommon = new SystemsViewUICommon(eMCommon, eVCommon, eUICommon);

            new EventsCommon(eUICommon, eVCommon, eMCommon);

            #endregion


            #region Menu

            var eVMenu = new EntitiesViewMenu();
            var eUIMenu = new EntitiesViewUIMenu(eUICommon);
            var eMMenu = new EntitiesModelMenu();

            var sMMenu = new SystemsModelMenu(eUIMenu, eUICommon, eMCommon);

            new EventsMenu(eMCommon, eUIMenu);

            #endregion


            #region Game

            var eVGame = new EntitiesViewGame(out var forData, eVCommon);
            var eMGame = new EntitiesModelGame(forData, Rpc.NamesMethods);
            var eUIGame = new EntitiesViewUIGame(eUICommon);

            var sMGame = new SystemsModelGame(eMGame, eMCommon);
            var sUIGame = new SystemsViewUIGame(eMCommon, eUIGame, eMGame);
            var sVGame = new SystemsViewGame(eVGame, eMGame, eVCommon, eMCommon);

            new EventsUIGame(eUICommon, eMCommon, sMGame, eUIGame, eMGame);

            #endregion


            #region NeedReplace

            var rpc = eVGame.PhotonC.PhotonView.gameObject.AddComponent<Rpc>().GiveData(sMGame, eMGame, eMCommon);


            new IAPCore(eUICommon.ShopE, eMCommon);
            new MyYodo();

            var togglerScenes = new List<IToggleScene>()
            {
                sMCommon,
                sUICommon,
                sMGame,
                rpc,
            };

            gameObject.AddComponent<PhotonSceneManager>().StartMy(rpc, togglerScenes);

            #endregion


            _runs = new List<IEcsRunSystem>()
            {
                sMCommon,
                sUICommon,
                sMGame,
                sVGame,
                sUIGame,
                sMMenu,
            };



            #region ComeToTraining

            eMCommon.VolumeMusic = eUICommon.SettingsE.SliderC.Slider.value;

            PhotonNetwork.OfflineMode = true;
            eMCommon.GameModeTC.GameMode = GameModes.TrainingOff;
            PhotonNetwork.CreateRoom(default);

            #endregion
        }

        void Update() => _runs.ForEach((IEcsRunSystem iRun) => iRun.Run());
    }
}