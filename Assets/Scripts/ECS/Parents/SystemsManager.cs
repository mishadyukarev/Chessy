using Leopotam.Ecs;
using UnityEngine;

public abstract class SystemsManager
{
    protected EcsWorld _ecsWorld;

    internal EcsSystems UpdateRunSystems;
    internal EcsSystems FixedUpdateSystems;
    internal EcsSystems SoloSystems;

    protected EcsSystems _currentSystemsForInvoke;

    protected SystemsManager(EcsWorld ecsWorld)
    {
        _ecsWorld = ecsWorld;

        UpdateRunSystems = new EcsSystems(ecsWorld);
        FixedUpdateSystems = new EcsSystems(ecsWorld);
        SoloSystems = new EcsSystems(ecsWorld);
    }


    internal virtual void InitAndProcessInjectsSystems()
    {
        UpdateRunSystems.ProcessInjects();
        FixedUpdateSystems.ProcessInjects();
        SoloSystems.ProcessInjects();

        UpdateRunSystems.Init();
        FixedUpdateSystems.Init();
        SoloSystems.Init();
    }

    internal void Update() => UpdateRunSystems.Run();
    internal void FixedUpdate() => FixedUpdateSystems.Run();

    internal void Destroy() => UpdateRunSystems.Destroy();



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
