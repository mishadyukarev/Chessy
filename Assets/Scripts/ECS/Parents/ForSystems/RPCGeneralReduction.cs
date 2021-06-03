using static Main;
internal abstract class RPCGeneralReduction : SystemGeneralReduction
{
    protected PhotonPunRPC _photonPunRPC;

    internal RPCGeneralReduction()
    {
        _photonPunRPC = Instance.PhotonGameManager.PhotonPunRPC;
    }
}

