using static Main;
internal abstract class RPCGeneralSystemReduction : SystemGeneralReduction
{
    protected PhotonPunRPC _photonPunRPC;

    internal RPCGeneralSystemReduction()
    {
        _photonPunRPC = Instance.PhotonGameManager.PhotonPunRPC;
    }
}

