using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

internal sealed class DonerUISystem : IEcsRunSystem
{
    private EcsFilter<DonerDataUIComponent, DonerViewUIComponent> _donerUIFilter = default;

    public void Run()
    {
        ref var donerDataUICom = ref _donerUIFilter.Get1(0);
        ref var donerViewUICom = ref _donerUIFilter.Get2(0);

        if (donerDataUICom.IsDoned(PhotonNetwork.IsMasterClient))
        {
            donerViewUICom.SetColor(Color.red);
        }
        else
        {
            donerViewUICom.SetColor(Color.white);
        }
    }
}
