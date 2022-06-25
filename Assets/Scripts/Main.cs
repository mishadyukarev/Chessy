﻿using Chessy.Common;
using Chessy.Model;
using Chessy.Model.EventsUI;
using Chessy.Model;
using Chessy.Model.System.View;
using Chessy.Model.System.View.UI;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy
{
    sealed partial class Main : MonoBehaviour
    {
        [SerializeField] TestModeTypes TestModeT = default;

        List<IUpdate> _runs;

        void Start()
        {
            var eV = new EntitiesView(out var forData, transform, TestModeT, out var actions);
            var eM = new EntitiesModel(forData, Rpc.NameRpcMethod, actions, TestModeT);
            var eUI = new EntitiesViewUI();

            var sM = new SystemsModel(eM);
            var sUI = new SystemsViewUI(eUI, eM);
            var sV = new SystemsView(eV, eM);

            var eventsGame = new EventsUIGame(sM, eUI, eV, eM);


            #region NeedReplace

            var adLaunchS = new TryLaunchAdS(eM);
            new ShopS(eM);

            var rpc = eV.PhotonC.PhotonView.gameObject.AddComponent<Rpc>().GiveData(sM);
            gameObject.AddComponent<PhotonSceneManager>().StartMy(sUI, sM);

            #endregion


            _runs = new List<IUpdate>()
            {
                adLaunchS,
                sM,
                eventsGame,

                sUI,
                sV,
            };

            sM.ComeToTrainingAfterDownloadingGame();
        }

        void Update()
        {
            _runs.ForEach((IUpdate iRun) => iRun.Update());
        }
    }
}