using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Info;

internal sealed class MeltOreMasterSystem : SystemMasterReduction
{
    public override void Run()
    {
        base.Run();

        if (ResourcesUIDataContainer.CanMeltOre(RpcMasterDataContainer.InfoFrom.Sender, out bool[] haves))
        {
            ResourcesUIDataContainer.BuyMeltOre(RpcMasterDataContainer.InfoFrom.Sender);
            PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Melting);
        }
        else
        {
            PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
            PhotonPunRPC.MistakeEconomyToGeneral(RpcMasterDataContainer.InfoFrom.Sender, haves);
        }
    }
}
