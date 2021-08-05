using Assets.Scripts.Abstractions.ValuesConsts;
using Photon.Pun;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    public sealed class PhotonMainManager
    {
        private PhotonView _photonView;
        private PhotonPunRPC _photonPunRPC;
        //private PhotonScene _sceneManager;

        public PhotonMainManager()
        {
            _photonView = Instance.gameObject.AddComponent<PhotonView>();
            //_sceneManager = Instance.gameObject.AddComponent<PhotonScene>();
            _photonPunRPC = Instance.gameObject.AddComponent<PhotonPunRPC>();

            _photonPunRPC.Constructor(_photonView);
            //_sceneManager.Constructor();

            _photonView.FindObservables(true);

            if (PhotonNetwork.IsMasterClient) PhotonNetwork.AllocateViewID(_photonView);
            else _photonView.ViewID = ValuesConst.NUMBER_PHOTON_VIEW;
        }

        internal void ToggleScene(SceneTypes sceneType)
        {
            _photonPunRPC.ToggleScene(sceneType);
        }
    }
}