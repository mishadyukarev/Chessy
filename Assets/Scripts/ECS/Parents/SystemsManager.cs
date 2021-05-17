using Leopotam.Ecs;
using UnityEngine;

public abstract class SystemsManager
{
    protected EcsWorld _ecsWorld;

    internal EcsSystems RunUpdateSystems;
    internal EcsSystems FixedUpdateSystems;
    internal EcsSystems SoloSystems;

    protected EcsSystems _currentSystemsForInvoke;

    protected SystemsManager(EcsWorld ecsWorld)
    {
        _ecsWorld = ecsWorld;

        RunUpdateSystems = new EcsSystems(ecsWorld);
        FixedUpdateSystems = new EcsSystems(ecsWorld);
        SoloSystems = new EcsSystems(ecsWorld);
    }


    internal virtual void InitAndProcessInjectsSystems()
    {
        RunUpdateSystems.ProcessInjects();
        FixedUpdateSystems.ProcessInjects();
        SoloSystems.ProcessInjects();

        RunUpdateSystems.Init();
        FixedUpdateSystems.Init();
        SoloSystems.Init();
    }

    internal void Update() => RunUpdateSystems.Run();
    internal void FixedUpdate() => FixedUpdateSystems.Run();

    internal void Destroy() => RunUpdateSystems.Destroy();



    internal bool TryInvokeRunSystem(string namedSystem, EcsSystems currentSystems)
    {
        var numberOfNamedSystem = currentSystems.GetNamedRunSystem(namedSystem);

        if (numberOfNamedSystem != -1)
        {
            currentSystems.GetRunSystems().Items[numberOfNamedSystem].System.Run();
            return true;
        }
        else
        {
            Debug.Log("Не нашёл систему");
            return false;
        }
    }
    internal bool TryActiveRunSystem(bool isActive, string namedSystem, EcsSystems currentSystems)
    {
        var numberOfNamedSystem = currentSystems.GetNamedRunSystem(namedSystem);

        if (numberOfNamedSystem != -1)
        {
            currentSystems.GetRunSystems().Items[numberOfNamedSystem].Active = isActive;
            return true;
        }
        else
        {
            Debug.Log("Не нашёл систему");
            return false;
        }
    }
}
