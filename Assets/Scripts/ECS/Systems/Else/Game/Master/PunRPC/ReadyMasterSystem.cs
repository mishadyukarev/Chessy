using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;

internal sealed class ReadyMasterSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _currentGameWorld = default;

    private EcsFilter<InfoMasCom> _infoFilter = default;
    private EcsFilter<ForReadyMasCom, NeedActiveSomethingMasCom> _forReadyFilter = default;
    private EcsFilter<ReadyDataUICom, ReadyViewUICom> _readyUIFilter;

    public void Init()
    {
        _currentGameWorld.NewEntity()
            .Replace(new ForReadyMasCom())
            .Replace(new NeedActiveSomethingMasCom());
    }

    public void Run()
    {
        var sender = _infoFilter.Get1(0).FromInfo.Sender;
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
