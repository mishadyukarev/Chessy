//using Assets.Scripts.ECS.Components;
//using Leopotam.Ecs;

//namespace Assets.Scripts.Workers.Common
//{
//    internal struct DataCommContainerElseSaver
//    {
//        private static EcsEntity _saverEnt;
//        internal DataCommContainerElseSaver(EcsWorld commWorld)
//        {
//            _saverEnt = commWorld.NewEntity()
//                .Replace(new SaverComponent(0.15f))
//                .Replace(new StepModeTypeComponent());
//        }

//        internal static float SliderVolume
//        {
//            get => _saverEnt.Get<SaverComponent>().SliderVolume;
//            set => _saverEnt.Get<SaverComponent>().SliderVolume = value;
//        }
//        internal static StepModeTypes StepModeType
//        {
//            get => _saverEnt.Get<StepModeTypeComponent>().StepModeType;
//            set => _saverEnt.Get<StepModeTypeComponent>().StepModeType = value;
//        }
//    }
//}
