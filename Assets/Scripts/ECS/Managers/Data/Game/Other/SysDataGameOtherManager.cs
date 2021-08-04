using Assets.Scripts;
using Assets.Scripts.ECS.Game.Components;
using Leopotam.Ecs;

public sealed class SysDataGameOtherManager : SystemAbstManager
{
    private EcsEntity _fromInfoEnt;
    internal ref FromInfoComponent FromInfoEnt_FromInfoCom => ref _fromInfoEnt.Get<FromInfoComponent>();


    internal SysDataGameOtherManager(EcsWorld gameWorld) : base(gameWorld)
    {
        _fromInfoEnt = gameWorld.NewEntity()
            .Replace(new FromInfoComponent());
    }


    internal override void Init()
    {
        base.Init();

    }
}
