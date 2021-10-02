using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.Supports;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class MeltOreMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoCom> _infoMasFilter = default;
    private EcsFilter<InventResourCom> _invResFilt = default;

    public void Run()
    {
        var sender = _infoMasFilter.Get1(0).FromInfo.Sender;
        ref var invResCom = ref _invResFilt.Get1(0);
            
        PlayerTypes isMastSender = default;
        if (PhotonNetwork.OfflineMode) isMastSender = WhoseMoveCom.WhoseMoveOffline;
        else isMastSender = sender.GetPlayerType();


        if (invResCom.CanMeltOre(isMastSender, out bool[] haves))
        {
            invResCom.BuyMeltOre(isMastSender);
            RpcSys.SoundToGeneral(sender, SoundEffectTypes.Melting);
        }
        else
        {
            RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
            RpcSys.MistakeEconomyToGeneral(sender, haves);
        }
    }
}
