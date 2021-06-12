using Leopotam.Ecs;
using UnityEngine;

internal abstract class SystemsManager
{
    internal EcsSystems RunUpdateSystems;

    internal virtual void ProcessInjects()
    {
        RunUpdateSystems.ProcessInjects();
    }

    internal virtual void Init()
    {
        RunUpdateSystems.Init();
    }

    internal void Update() => RunUpdateSystems.Run();



    internal bool TryInvokeRunSystem(string namedSystem, EcsSystems currentSystems)
    {
        var numberOfNamedSystem = currentSystems.GetNamedRunSystem(namedSystem);

        var haveSystem = numberOfNamedSystem != -1;

        if (haveSystem) currentSystems.GetRunSystems().Items[numberOfNamedSystem].System.Run();
        else Debug.Log("Не нашёл систему");

        return haveSystem;
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
