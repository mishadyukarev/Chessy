using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

internal sealed class DonerUISystem : IEcsRunSystem
{
    private EcsFilter<DonerUICom> _donerUIFilter = default;

    public void Run()
    {
        ref var donerViewUICom = ref _donerUIFilter.Get1(0);

        if (GameModesCom.IsOnlineMode)
        {          
            donerViewUICom.SetTextDoner(LanguageComCom.GetText(GameLanguageTypes.Done));
            donerViewUICom.SetTextWait(LanguageComCom.GetText(GameLanguageTypes.WaitPlayer));


            if (WhoseMoveCom.IsMyOnlineMove)
            {
                donerViewUICom.DisableWait();
                donerViewUICom.SetColor(Color.white);
            }
            else
            {
                donerViewUICom.EnableWait();
                donerViewUICom.SetColor(Color.red); 
            }
        }

        else
        {
            donerViewUICom.DisableWait();
        }
    }
}
