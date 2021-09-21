using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

internal sealed class DonerUISystem : IEcsRunSystem
{
    private EcsFilter<DonerUICom> _donerUIFilter = default;

    public void Run()
    {
        ref var donerViewUICom = ref _donerUIFilter.Get1(0);

        //donerViewUICom.SetTextDoner(LanguageComComp.GetText(GameLanguageTypes.Done));
        //donerViewUICom.SetTextWait(LanguageComComp.GetText(GameLanguageTypes.WaitPlayer));


        //if (donerDataUICom.IsDoned(PhotonNetwork.IsMasterClient))
        //{
        //    donerViewUICom.EnableWait();
        //    donerViewUICom.SetColor(Color.red);
        //}
        //else
        //{
        donerViewUICom.DisableWait();
        //    donerViewUICom.SetColor(Color.white);
        //}
    }
}
