using Leopotam.Ecs;
using UnityEngine;

public abstract class SystemsManager
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
