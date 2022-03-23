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

        List<IEcsRunSystem> _runs;


        void Start()
        {
            #region Entity

            var eVCommon = new EntitiesViewCommon(transform, TestMode, out var sound, out var commonZone);
            var eUICommon = new EntitiesViewUICommon(GameObject.Instantiate(Resources.Load<Canvas>("Canvas")), commonZone);
            var eMCommon = new EntitiesModelCommon(TestMode, sound);

            var eVMenu = new EntitiesViewMenu();
            var eUIMenu = new EntitiesViewUIMenu(eUICommon);
            var eMMenu = new EntitiesModelMenu();

            var eVGame = new EntitiesViewGame(out var forData, eVCommon);
            var eMGame = new EntitiesModelGame(forData, Rpc.NamesMethods, eMCommon);
            var eUIGame = new EntitiesViewUIGame(eUICommon);

            #endregion


            #region System

            var sMCommon = new SystemsModelCommon(eMCommon);
            var sUICommon = new SystemsViewUICommon(eMCommon, eVCommon, eUICommon);

            var _sMMenu = new SystemsModelMenu(eUIMenu, eUICommon, eMCommon);

            var sMGame = new SystemsModelGame(eMGame, eMCommon);
            var sUIGame = new SystemsViewUIGame(eMCommon, eUIGame, eMGame);
            var sVGame = new SystemsViewGame(eVGame, eMGame, eVCommon, eMCommon);

   
            #region NeedReplace

            var rpc = eVGame.PhotonC.PhotonView.gameObject.AddComponent<Rpc>().GiveData(sMGame, eMGame, eMCommon);

            new EventsCommon(eUICommon, eVCommon, eMCommon);
            new IAPCore(eUICommon.ShopE);
            new MyYodo();

            var togglerScenes = new List<IToggleScene>()
            {
                eMCommon,
                eUICommon,
                sMGame,
                rpc,
            };

            gameObject.AddComponent<PhotonSceneManager>().StartMy(togglerScenes);

            #endregion


            _runs = new List<IEcsRunSystem>()
            {
                sMCommon,
                sUICommon,
                sMGame,
                sVGame,
                sUIGame,
                _sMMenu,
            };



            #endregion


            #region Event

            new EventsMenu(eMCommon, eUIMenu);
            new EventsUIGame(eUICommon, eMCommon, sMGame, eUIGame, eMGame);

            #endregion
        }

        void Update() => _runs.ForEach((IEcsRunSystem iRun) => iRun.Run());
    }
}