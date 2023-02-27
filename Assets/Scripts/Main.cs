using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.System;
using Chessy.View.System;
using Chessy.View.UI;
using Chessy.View.UI.Entity;
using Chessy.View.UI.System;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy
{
    sealed class Main : MonoBehaviour
    {
        [SerializeField] TestModeTypes TestModeT = default;

        IUpdate[] _runs;

        void Start()
        {
            var eV = new EntitiesView(out var forData, transform, TestModeT, out var actions);
            var eM = new EntitiesModel(forData, Rpc.NameRpcMethod, actions, TestModeT);
            var eUI = new EntitiesViewUI();

            var sM = new SystemsModel(eM);
            var sUI = new SystemsViewUI(eUI, eM);
            var sV = new SystemsView(eV, eM);

            var eventsGame = new EventsUIGame(sM, eUI, eM);


            #region NeedReplace

            new ShopS(eM);
            var adLaunchS = new TryLaunchAdVideoAndBannerS(eM);



            #region Photon

            var photonV = eV.PhotonC.PhotonView;
            photonV.gameObject.AddComponent<Rpc>().Fill(sM);

            var idPhoton = 2;
            for (var syncT = (SyncTypes)1; syncT < SyncTypes.End; syncT++)
            {
                photonV = gameObject.AddComponent<PhotonView>();
                photonV.ViewID = idPhoton++;
                var photonSerializeView = photonV.gameObject.AddComponent<PhotonSerializeView>().Fill(syncT, sM);
                photonV.ObservedComponents = new List<Component>() { photonSerializeView };
                photonV.Synchronization = ViewSynchronization.ReliableDeltaCompressed;
            }

            #endregion


            gameObject.AddComponent<PhotonSceneManager>().GiveSystems(sM);



            //PhotonNetwork.KeepAliveInBackground = 180;

            //PhotonNetwork.PhotonServerSettings.AppSettings.Protocol = ConnectionProtocol.Tcp;

            //PhotonNetwork.SendRate = 12;
            //PhotonNetwork.SerializationRate = 60;
            //PhotonNetwork.IsMessageQueueRunning = true;


            #endregion

            _runs = new IUpdate[]
            {
                adLaunchS,
                sM,
                eventsGame,

                sUI,
                sV,
            };


            sM.ComeIntoTrainingAfterDownloadingGame();
        }

        void Update()
        {
            for (var i = 0; i < _runs.Length; i++) _runs[i].Update();
        }
    }
}