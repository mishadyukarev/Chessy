using Leopotam.Ecs;
using UnityEngine;

public abstract class SystemsManager
{
    protected EcsWorld _ecsWorld;

    protected EcsSystems _updateSystems;
    protected EcsSystems _fixedUpdateSystems;
    protected EcsSystems _timeUpdateSystems;
    protected EcsSystems _multipleSystems;

    protected EcsSystems _currentSystemsForInvoke;

    protected SystemsManager(EcsWorld ecsWorld)
    {
        _ecsWorld = ecsWorld;

        _updateSystems = new EcsSystems(ecsWorld);
        _fixedUpdateSystems = new EcsSystems(ecsWorld);

        _timeUpdateSystems = new EcsSystems(ecsWorld);
        _multipleSystems = new EcsSystems(ecsWorld);
    }


    internal virtual void InitAndProcessInjectsSystems()
    {
        _updateSystems.ProcessInjects();
        _fixedUpdateSystems.ProcessInjects();
        _timeUpdateSystems.ProcessInjects();
        _multipleSystems.ProcessInjects();

        _updateSystems.Init();
        _fixedUpdateSystems.Init();
        _timeUpdateSystems.Init();
        _multipleSystems.Init();
    }

    internal void Update() => _updateSystems.Run();
    internal void FixedUpdate() => _fixedUpdateSystems.Run();

    internal void Destroy() => _updateSystems.Destroy();



    protected bool TryInvokeRunSystem(string namedSystem, EcsSystems currentSystems)
    {
        var numberOfNamedSystem = currentSystems.GetNamedRunSystem(namedSystem);

        if (numberOfNamedSystem != -1)
        {
            var ecsSystemsRunItem = currentSystems.GetRunSystems().Items[numberOfNamedSystem];
            currentSystems.GetRunSystems().Items[numberOfNamedSystem].System.Run();
            return true;
        }
        else
        {
            Debug.Log("Не нашёл систему");
            return false;
        }
    }
    protected void ActiveRunSystem(bool isActive, string namedSystem, EcsSystems currentSystems)
    {
        var numberOfNamedSystem = currentSystems.GetNamedRunSystem(namedSystem);

        if (numberOfNamedSystem != -1)
        {
            var ecsSystemsRunItem = currentSystems.GetRunSystems().Items[numberOfNamedSystem];
            currentSystems.GetRunSystems().Items[numberOfNamedSystem].Active = isActive;
        }
        else
        {
            Debug.Log("Не нашёл систему");
        }
    }
}
