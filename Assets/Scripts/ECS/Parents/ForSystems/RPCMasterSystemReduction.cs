using static Main;
internal abstract class RPCMasterSystemReduction : SystemMasterReduction
{
    protected PhotonPunRPC _photonPunRPC;

    internal RPCMasterSystemReduction() : base()
    {
        _photonPunRPC = Instance.PhotonGameManager.PhotonPunRPC;
    }
}
