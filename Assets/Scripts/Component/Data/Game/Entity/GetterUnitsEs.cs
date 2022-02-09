//using ECS;
//using System.Collections.Generic;

//namespace Game.Game
//{
//    public struct GetterUnitsEs
//    {
//        static Entity _getter;

//        public static ref IsActiveC IsActiveC => ref _getter.Get<IsActiveC>();
//        public static ref TimerC TimerC => ref _getter.Get<TimerC>();

//        public GetterUnitsEs(in EcsWorld gameW)
//        {
//            _getter = gameW.NewEntity()
//                    .Add(new IsActiveC())
//                    .Add(new TimerC());
//        }
//    }
//}
