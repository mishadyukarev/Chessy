using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    internal abstract class RPCMasterSystemReduction : SystemMasterReduction
    {
        protected PhotonPunRPC _photonPunRPC;

        internal RPCMasterSystemReduction() : base()
        {
            _photonPunRPC = Instance.PhotonManager.PhotonPunRPC;
        }
    }
}