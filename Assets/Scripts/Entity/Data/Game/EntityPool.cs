//using ECS;
//using Game.Common;
//using System.Collections.Generic;

//namespace Game.Game
//{
//    public readonly struct EntityPool
//    {
//        static Dictionary<string, Entity> _ents;
//        //static Entity _background;

//        public static ref C ClickerObject<C>() where C : struct, IClickerObjectE => ref _ents[nameof(IClickerObjectE)].Get<C>();
//        //public static ref C Background<C>() where C : struct => ref _background.Get<C>();
//        public EntityPool(in EcsWorld gameW, in List<object> actions, in List<string> namesMethods)
//        {
//            _ents = new Dictionary<string, Entity>();


//            _ents[nameof(IClickerObjectE)] = gameW.NewEntity()
//                .Add(new CellClickC(CellClickTypes.SimpleClick))
//                .Add(new RayCastTC());

//        }
//    }
//}