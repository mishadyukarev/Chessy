using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

internal sealed class ReadyZoneUISystem : IEcsRunSystem
{
    private EcsFilter<ReadyDataUICom, ReadyViewUICom> _readyUIFilter = default;

    public void Run()
    {
        ref var readyDataUICom = ref _readyUIFilter.Get1(0);
        ref var readyViewUICom = ref _readyUIFilter.Get2(0);

        if (readyDataUICom.IsReady(PhotonNetwork.IsMasterClient))
        {
            readyViewUICom.SetColorReadyButton(Color.red);
        }
        else
        {
            readyViewUICom.SetColorReadyButton(Color.white);
        }

        if (readyDataUICom.IsStartedGame || PhotonNetwork.OfflineMode)
        {
            readyViewUICom.SetActiveParent(false);
        }
        else
        {
            readyViewUICom.SetTextWait(LanguageComCom.GetText(GameLanguageTypes.WaitReady));
            readyViewUICom.SetTextReady(LanguageComCom.GetText(GameLanguageTypes.ReadyBeforeGame));
            readyViewUICom.SetTextJoinForFind(LanguageComCom.GetText(GameLanguageTypes.JoinForFind));

            readyViewUICom.SetActiveParent(true);
        }
    }
}