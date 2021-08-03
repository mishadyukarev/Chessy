using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class SystemAbstManager
    {
        protected EcsSystems InitSystems { get; set; }
        protected EcsSystems RunUpdateSystems { get; set; }

        protected SystemAbstManager(EcsWorld gameWorld)
        {
            InitSystems = new EcsSystems(gameWorld);
            RunUpdateSystems = new EcsSystems(gameWorld);
        }

        internal virtual void Init()
        {
            InitSystems.Init();
            RunUpdateSystems.Init();
        }

        internal virtual void RunUpdate() => RunUpdateSystems.Run();



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