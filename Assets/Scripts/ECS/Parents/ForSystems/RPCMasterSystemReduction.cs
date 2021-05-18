using static MainGame;
internal abstract class RPCMasterSystemReduction : SystemMasterReduction
{
    protected PhotonPunRPC _photonPunRPC;

    internal RPCMasterSystemReduction(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonPunRPC = InstanceGame.PhotonGameManager.PhotonPunRPC;
    }
}
