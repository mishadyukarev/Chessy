using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class SystemsManager
    {
        protected EcsSystems StartFillSystems { get; set; }
        protected EcsSystems RunUpdateSystems { get; set; }

        protected SystemsManager(EcsWorld gameWorld)
        {
            StartFillSystems = new EcsSystems(gameWorld);
            RunUpdateSystems = new EcsSystems(gameWorld);
        }

        internal virtual void ProcessInjects()
        {
            StartFillSystems.ProcessInjects();
            RunUpdateSystems.ProcessInjects();
        }

        internal virtual void Init()
        {
            StartFillSystems.Init();
            RunUpdateSystems.Init();
        }

        internal void Update() => RunUpdateSystems.Run();



        internal static bool TryInvokeRunSystem(string namedSystem, EcsSystems currentSystems)
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
}