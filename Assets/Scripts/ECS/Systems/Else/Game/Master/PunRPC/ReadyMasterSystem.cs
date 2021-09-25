using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Leopotam.Ecs;

internal sealed class ReadyMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoFilter = default;
    private EcsFilter<ForReadyMasCom, NeedActiveSomethingMasCom> _forReadyFilter = default;
    private EcsFilter<ReadyDataUICom, ReadyViewUICom> _readyUIFilter = default;

    public void Run()
    {
        var sender = _infoFilter.Get1(0).FromInfo.sender;
        var needActiveReady = _forReadyFilter.Get2(0).NeedActiveSomething;
        ref var readyDataUICom = ref _readyUIFilter.Get1(0);


        readyDataUICom.SetIsReady(sender.IsMasterClient, needActiveReady);

        if (readyDataUICom.IsReady(true) && readyDataUICom.IsReady(false))
        {
            readyDataUICom.IsStartedGame = true;
        }

        else
        {
            readyDataUICom.IsStartedGame = false;
        }
    }
}
