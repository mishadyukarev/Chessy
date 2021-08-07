using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

internal sealed class DonerUISystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<TakerUnitsViewUICom> _takerUIFilter = default;
    private EcsFilter<DonerDataUIComponent, DonerViewUIComponent> _donerUIFilter = default;

    public void Init()
    {
        _donerUIFilter.Get2(0).AddListener(MistakeDone);
    }

    public void Run()
    {
        if (_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient))
        {
            _donerUIFilter.Get2(0).SetColor(Color.red);
        }
        else
        {
            _donerUIFilter.Get2(0).SetColor(Color.white);
        }
    }

    private void MistakeDone()
    {
        _takerUIFilter.Get1(0).SetColorButton(UnitTypes.King, Color.red);
    }
}
