﻿using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;
using Photon.Realtime;

internal sealed class MeltOreMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoMasFilter = default;
    private EcsFilter<InventorResourcesComponent> _invResFilt = default;

    private Player Sender => _infoMasFilter.Get1(0).FromInfo.Sender;

    public void Run()
    {
        ref var invResCom = ref _invResFilt.Get1(0);

        if (invResCom.CanMeltOre(Sender, out bool[] haves))
        {
            invResCom.BuyMeltOre(Sender);
            RpcGameSystem.SoundToGeneral(Sender, SoundEffectTypes.Melting);
        }
        else
        {
            RpcGameSystem.SoundToGeneral(Sender, SoundEffectTypes.Mistake);
            RpcGameSystem.MistakeEconomyToGeneral(Sender, haves);
        }
    }
}
