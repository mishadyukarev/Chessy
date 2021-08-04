using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    public sealed class PhotonScene : MonoBehaviourPunCallbacks
    {

        #region Menu



        internal void Constructor()
        {

        }


        internal static void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }


        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log(message);
            PhotonNetwork.PhotonServerSettings.StartInOfflineMode = true;
        }



        public override void OnJoinedRoom()
        {
            Instance.ToggleScene(SceneTypes.Game);
        }
        public override void OnLeftRoom()
        {
            base.OnLeftRoom();

            Instance.ToggleScene(SceneTypes.Menu);
        }



        public override void OnPlayerEnteredRoom(Player newPlayer)
        {

        }
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            LeaveRoom();
            Instance.ToggleScene(SceneTypes.Menu);
        }




        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            base.OnMasterClientSwitched(newMasterClient);

            LeaveRoom();
        }

        #endregion
    }
}