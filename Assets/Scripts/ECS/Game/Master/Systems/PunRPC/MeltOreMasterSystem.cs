using Assets.Scripts;
using Photon.Pun;

internal sealed class MeltOreMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_RPCCom.FromInfo;


    public override void Run()
    {
        base.Run();

        if (EconomyManager.CanMeltOre(Info.Sender, out bool[] haves))
        {
            EconomyManager.MeltOre(Info.Sender);
        }
        else
        {
            _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haves);
        }
    }
}
