﻿using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class SystemAbstManager
    {
        protected EcsSystems EventSystems { get; private set; }
        protected EcsSystems InitSystems { get; private set; }
        protected EcsSystems RunSystems { get; private set; }

        protected SystemAbstManager(EcsWorld world)
        {
            EventSystems = new EcsSystems(world);
            InitSystems = new EcsSystems(world);
            RunSystems = new EcsSystems(world);
        }
        internal virtual void RunUpdate() => RunSystems.Run();



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