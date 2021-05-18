using static MainGame;
internal abstract class RPCGeneralReduction : SystemGeneralReduction
{
    protected PhotonPunRPC _photonPunRPC;

    internal RPCGeneralReduction(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonPunRPC = InstanceGame.PhotonGameManager.PhotonPunRPC;
    }
}

