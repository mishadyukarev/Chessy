using Leopotam.Ecs;
using UnityEngine;

public abstract class SystemsManager
{
    protected EcsWorld _ecsWorld;

    protected EcsSystems _updateSystems;
    protected EcsSystems _elseSystems;

    protected EcsSystems _currentSystemsForInvoke;

    protected SystemsManager(EcsWorld ecsWorld)
    {
        _ecsWorld = ecsWorld;

        _updateSystems = new EcsSystems(ecsWorld);
        _elseSystems = new EcsSystems(ecsWorld);
    }


    internal void InitAndProcessInjectsSystems()
    {
        _updateSystems.ProcessInjects();
        _elseSystems.ProcessInjects();

        _updateSystems.Init();
        _elseSystems.Init();
    }

    internal void RunUpdate() => _updateSystems.Run();

    internal void Destroy() => _updateSystems.Destroy();



    protected bool TryInvokeRunSystem(string namedSystem, EcsSystems currentSystems)
    {
        var numberOfNamedSystem = currentSystems.GetNamedRunSystem(namedSystem);

        if (numberOfNamedSystem != -1)
        {
            var ecsSystemsRunItem = currentSystems.GetRunSystems().Items[numberOfNamedSystem];
            ecsSystemsRunItem.System.Run();
            return true;
        }
        else
        {
            Debug.Log("Не нашёл систему");
            return false;
        }
    }
}
