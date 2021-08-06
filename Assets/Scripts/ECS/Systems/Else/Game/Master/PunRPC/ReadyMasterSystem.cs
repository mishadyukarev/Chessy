using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;

internal sealed class ReadyMasterSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _currentGameWorld;
    private EcsFilter<InfoMasCom> _infoFilter;
    private EcsFilter<ReadyMasCom, NeedActiveSomethingMasCom> _readyFilter;

    public void Init()
    {
        _currentGameWorld.NewEntity()
            .Replace(new ReadyMasCom())
            .Replace(new NeedActiveSomethingMasCom());
    }

    public void Run()
    {
        var sender = _infoFilter.Get1(0).FromInfo.Sender;
        var needActiveReady = _readyFilter.Get2(0).NeedActiveSomething;

        MiddleUIDataContainer.SetIsReady(sender.IsMasterClient, needActiveReady);

        if (MiddleUIDataContainer.IsReady(true) && MiddleUIDataContainer.IsReady(false))
        {
            MiddleUIDataContainer.IsStartedGame = true;
        }

        else
        {
            MiddleUIDataContainer.IsStartedGame = false;
        }
    }
}
