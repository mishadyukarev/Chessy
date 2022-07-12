using Chessy.Model.System;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Model
{
    public sealed class PhotonSceneManager : MonoBehaviourPunCallbacks
    {
        SystemsModel _s;
        public void GiveSystems(in SystemsModel sM) { _s = sM; }

        public override sealed void OnLeftRoom() => _s.ForPhotonSceneS.OnLeftRoom();
        public override void OnPlayerLeftRoom(Player otherPlayer) => _s.ForPhotonSceneS.OnPlayerLeftRoom(otherPlayer);
        public override sealed void OnMasterClientSwitched(Player newMasterClient) => _s.ForPhotonSceneS.OnMasterClientSwitched(newMasterClient);

        public override sealed void OnJoinedRoom() => _s.ForPhotonSceneS.OnJoinedRoom();
        public override void OnPlayerEnteredRoom(Player newPlayer) => _s.ForPhotonSceneS.OnPlayerEnteredRoom(newPlayer);
        public override void OnDisconnected(DisconnectCause cause) => _s.ForPhotonSceneS.OnDisconnected();
        public override void OnConnected() => _s.ForPhotonSceneS.OnConnected();
        public override void OnConnectedToMaster() => _s.ForPhotonSceneS.OnConnectedToMaster();
    }
}