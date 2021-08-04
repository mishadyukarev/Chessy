using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class SystemAbstManager
    {
        protected EcsSystems UpdateSystems { get; private set; }

        protected SystemAbstManager(EcsWorld world)
        {
            UpdateSystems = new EcsSystems(world);
        }

        internal virtual void Init()
        {
            UpdateSystems.Init();
        }

        internal virtual void RunUpdate() => UpdateSystems.Run();



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