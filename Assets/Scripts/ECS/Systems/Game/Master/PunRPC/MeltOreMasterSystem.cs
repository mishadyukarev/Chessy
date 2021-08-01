using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Info;

internal sealed class MeltOreMasterSystem : SystemMasterReduction
{
    public override void Run()
    {
        base.Run();

        if (ResourcesDataUIWorker.CanMeltOre(RpcWorker.InfoFrom.Sender, out bool[] haves))
        {
            ResourcesDataUIWorker.BuyMeltOre(RpcWorker.InfoFrom.Sender);
            PhotonPunRPC.SoundToGeneral(RpcWorker.InfoFrom.Sender, SoundEffectTypes.Melting);
        }
        else
        {
            PhotonPunRPC.SoundToGeneral(RpcWorker.InfoFrom.Sender, SoundEffectTypes.Mistake);
            PhotonPunRPC.MistakeEconomyToGeneral(RpcWorker.InfoFrom.Sender, haves);
        }
    }
}
