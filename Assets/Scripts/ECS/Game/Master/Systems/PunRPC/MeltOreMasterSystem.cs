using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Info;
using Photon.Pun;

internal sealed class MeltOreMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;


    public override void Run()
    {
        base.Run();

        if (InfoResourcesDataWorker.CanMeltOre(InfoFrom.Sender, out bool[] haves))
        {
            InfoResourcesDataWorker.BuyMeltOre(InfoFrom.Sender);
            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Melting);
        }
        else
        {
            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
            PhotonPunRPC.MistakeEconomyToGeneral(InfoFrom.Sender, haves);
        }
    }
}
