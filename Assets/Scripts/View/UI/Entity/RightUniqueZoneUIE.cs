//using ECS;
//using UnityEngine; using Chessy.Model.Entity;

//namespace Game.Game
//{
//    public sealed class RightUniqueZoneUIE : EntityAbstract
//    {
//        public ref GameObjectVC Zone => ref Ent.Get<GameObjectVC>();

//        public RightUniqueZoneUIE(in EcsWorld gameW, in Transform uniqueZone) : base(gameW)
//        {
//            Ent.Add(new GameObjectVC(uniqueZone.gameObject));
//        }
//    }
//}