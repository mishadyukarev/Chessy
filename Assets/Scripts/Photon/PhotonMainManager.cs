using Assets.Scripts.Abstractions.ValuesConsts;
using Photon.Pun;
using System;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    public sealed class PhotonMainManager
    {
        private PhotonView _photonView;
        private PhotonPunRPC _photonPunRPC;
        private PhotonSceneManager _sceneManager;

        public PhotonSceneManager SceneManager => _sceneManager;
        public PhotonPunRPC PhotonPunRPC => _photonPunRPC;
        public PhotonView PhotonView => _photonView;


        public PhotonMainManager()
        {
            _photonView = Instance.gameObject.AddComponent<PhotonView>();
            _sceneManager = Instance.gameObject.AddComponent<PhotonSceneManager>();
            _photonPunRPC = Instance.gameObject.AddComponent<PhotonPunRPC>();

            PhotonPunRPC.Constructor(_photonView);
            _sceneManager.Constructor();

            _photonView.FindObservables(true);

            if (Instance.IsMasterClient) PhotonNetwork.AllocateViewID(_photonView);
            else _photonView.ViewID = ValuesConst.NUMBER_PHOTON_VIEW;
        }

        internal void OwnUpdate(SceneTypes sceneType)
        {
            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    _sceneManager.OwnUpdate(sceneType);
                    break;

                case SceneTypes.Game:
                    _sceneManager.OwnUpdate(sceneType);
                    break;

                default:
                    throw new Exception();
            }
        }

        internal void ToggleScene(SceneTypes sceneType)
        {
            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    _sceneManager.ToggleScene(sceneType);
                    _photonPunRPC.ToggleScene(sceneType);
                    break;

                case SceneTypes.Game:
                    _sceneManager.ToggleScene(sceneType);
                    _photonPunRPC.ToggleScene(sceneType);
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}