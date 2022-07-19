using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.System;
using Chessy.Model.Values;
using Chessy.View.System;
using Chessy.View.UI;
using Chessy.View.UI.Entity;
using Chessy.View.UI.System;
using ExitGames.Client.Photon;
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

            var eventsGame = new EventsUIGame(sM, eUI, eM);


            #region NeedReplace

            var adLaunchS = new TryLaunchAdS(eM);
            new ShopS(eM);


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


            _runs = new List<IUpdate>()
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
            _runs.ForEach((IUpdate iRun) => iRun.Update());
        }
    }
}