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

            var rpc = eV.PhotonC.PhotonView.gameObject.AddComponent<Rpc>().FillRpcWithSystems(sM);

            eV.PhotonC.PhotonView.ObservedComponents = new List<Component>() { rpc };

            var photonParent = new GameObject("Photons");

            var currentIdPhoton = 1001;

            for (byte cellIdx = 0; cellIdx < IndexCellsValues.CELLS; cellIdx++)
            {
                var forPhoton = new GameObject();
                forPhoton.transform.SetParent(photonParent.transform);

                for (byte i = 1; i <= 17; i++)
                {
                    currentIdPhoton++;

                    var photonV = forPhoton.AddComponent<PhotonView>();
                    photonV.ViewID = currentIdPhoton;

                    var system = photonV.gameObject.AddComponent<CellOnPhotonSerializeView>();
                    system.GiveData(cellIdx, i, eM);

                    photonV.ObservedComponents = new List<Component>() { system };
                }
            }

            
            gameObject.AddComponent<PhotonSceneManager>().GiveSystems(sM);



            PhotonNetwork.PhotonServerSettings.AppSettings.Protocol = ConnectionProtocol.Tcp;

            //PhotonNetwork.SendRate = 5;
            //PhotonNetwork.SerializationRate = 5;
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