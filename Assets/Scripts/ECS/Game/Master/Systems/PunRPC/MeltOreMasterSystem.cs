using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;

internal sealed class MeltOreMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;


    public override void Run()
    {
        base.Run();

        if (EconomyWorker.CanMeltOre(InfoFrom.Sender, out bool[] haves))
        {
            EconomyWorker.MeltOre(InfoFrom.Sender);
            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Melting);
        }
        else
        {
            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
            PhotonPunRPC.MistakeEconomyToGeneral(InfoFrom.Sender, haves);
        }
    }
}
