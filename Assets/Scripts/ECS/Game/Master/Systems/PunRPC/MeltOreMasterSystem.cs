using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;

internal sealed class MeltOreMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;


    public override void Run()
    {
        base.Run();

        if (EconomyManager.CanMeltOre(InfoFrom.Sender, out bool[] haves))
        {
            EconomyManager.MeltOre(InfoFrom.Sender);
            _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Melting);
        }
        else
        {
            _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
            _photonPunRPC.MistakeEconomyToGeneral(InfoFrom.Sender, haves);
        }
    }
}
