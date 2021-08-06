using Assets.Scripts.Abstractions.ValuesConsts;
using Photon.Pun;

namespace Assets.Scripts.ECS.Component.Common
{
    internal struct PhotonViewComponent
    {
        internal static PhotonView PhotonView { get; private set; }

        internal PhotonViewComponent(PhotonView photonView)
        {
            PhotonView = photonView;

            PhotonView.FindObservables(true);

            if (PhotonNetwork.IsMasterClient) PhotonNetwork.AllocateViewID(PhotonView);
            else PhotonView.ViewID = ValuesConst.NUMBER_PHOTON_VIEW;
        }
    }
}
