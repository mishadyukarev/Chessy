using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
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
            _photonPunRPC.SoundToGeneral(Info.Sender, SoundEffectTypes.Melting);
        }
        else
        {
            _photonPunRPC.SoundToGeneral(Info.Sender, SoundEffectTypes.Mistake);
            _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haves);
        }
    }
}
