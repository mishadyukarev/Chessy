//using Leopotam.Ecs;
//using UnityEngine;

//namespace Game.Common
//{
//    public abstract class SystemAbstManager
//    {
//        protected EcsSystems InitOnlySystems { get; private set; }
//        protected EcsSystems UpdateOnlySystems { get; private set; }
//        protected EcsSystems InitUpdateSystems { get; private set; }

//        protected SystemAbstManager(EcsWorld world, EcsSystems allSystems)
//        {
//            InitOnlySystems = new EcsSystems(world);
//            UpdateOnlySystems = new EcsSystems(world);
//            InitUpdateSystems = new EcsSystems(world);

//            allSystems
//                .Add(InitOnlySystems)
//                .Add(UpdateOnlySystems)
//                .Add(InitUpdateSystems);
//        }
//        public virtual void RunUpdate()
//        {
//            UpdateOnlySystems.Run();
//            InitUpdateSystems.Run();
//        }



//        internal static bool TryInvokeRunSystem(string namedSystem, EcsSystems currentSystems)
//        {
//            var numberOfNamedSystem = currentSystems.GetNamedRunSystem(namedSystem);

//            var haveSystem = numberOfNamedSystem != -1;

//            if (haveSystem) currentSystems.GetRunSystems().Items[numberOfNamedSystem].System.Run();
//            else Debug.Log("Не нашёл систему");

//            return haveSystem;
//        }
//        internal bool TryActiveRunSystem(bool isActive, string namedSystem, EcsSystems currentSystems)
//        {
//            var numberOfNamedSystem = currentSystems.GetNamedRunSystem(namedSystem);

//            if (numberOfNamedSystem != -1)
//            {
//                currentSystems.GetRunSystems().Items[numberOfNamedSystem].Active = isActive;
//                return true;
//            }
//            else
//            {
//                Debug.Log("Не нашёл систему");
//                return false;
//            }
//        }
//    }
//}