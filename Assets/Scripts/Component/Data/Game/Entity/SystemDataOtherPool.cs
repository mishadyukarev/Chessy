//using ECS;
//using System.Collections.Generic;

//namespace Game.Game
//{
//    public struct SystemDataOtherPool
//    {
//        static Dictionary<string, Entity> _ents;

//        public static ref C Sync<C>() where C : struct => ref _ents["0"].Get<C>();

//        public SystemDataOtherPool(in Dictionary<string, Entity> ents)
//        {
//            _ents = ents;
//        }
//    }
//}