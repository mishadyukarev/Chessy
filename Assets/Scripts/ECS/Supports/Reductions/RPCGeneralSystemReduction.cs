using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    internal abstract class RPCGeneralSystemReduction : SystemGeneralReduction
    {
        protected PhotonPunRPC _photonPunRPC;

        internal RPCGeneralSystemReduction()
        {
            _photonPunRPC = Instance.PhotonManager.PhotonPunRPC;
        }
    }
}