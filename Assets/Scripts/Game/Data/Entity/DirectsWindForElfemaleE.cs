//using ECS;
//using System.Collections.Generic;

//namespace Game.Game
//{
//    public struct DirectsWindForElfemaleE
//    {
//        static Dictionary<DirectTypes, Entity> _directs;

//        public static ref C Idx<C>(in DirectTypes dir) where C : struct => ref _directs[dir].Get<C>();

//        public static HashSet<byte> IdxsDirects
//        {
//            get
//            {
//                var directs = new HashSet<byte>();
//                foreach (var item in _directs) directs.Add(Idx<IdxC>(item.Key).Idx);
//                return directs;
//            }
//        }

//        public DirectsWindForElfemaleE(in EcsWorld gameW)
//        {
//            _directs = new Dictionary<DirectTypes, Entity>();
//            for (var dir = DirectTypes.First; dir < DirectTypes.End; dir++)
//            {
//                _directs.Add(dir, gameW.NewEntity()
//                    .Add(new IdxC()));
//            }
//        }
//    }
//}