using Photon.Pun;

internal sealed class MeltOreMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;


    public override void Run()
    {
        base.Run();

        if (_eM.CanMeltOre(Info.Sender, out bool[] haves))
        {
            _eM.MeltOre(Info.Sender);
        }
        else
        {
            _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haves);
        }
    }
}
