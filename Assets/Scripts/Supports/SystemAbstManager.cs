using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class SystemAbstManager
    {
        protected EcsSystems InitOnlySystems { get; private set; }
        protected EcsSystems RunOnlySystems { get; private set; }
        protected EcsSystems InitRunSystems { get; private set; }

        protected SystemAbstManager(EcsWorld world, EcsSystems allSystems)
        {
            InitOnlySystems = new EcsSystems(world);
            RunOnlySystems = new EcsSystems(world);
            InitRunSystems = new EcsSystems(world);

            allSystems
                .Add(InitOnlySystems)
                .Add(RunOnlySystems)
                .Add(InitRunSystems);
        }
        internal virtual void RunUpdate()
        {
            RunOnlySystems.Run();
            InitRunSystems.Run();
        }



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