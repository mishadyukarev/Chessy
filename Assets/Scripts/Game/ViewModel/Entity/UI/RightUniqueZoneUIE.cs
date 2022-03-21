//using ECS;
//using UnityEngine;

//namespace Game.Game
//{
//    public sealed class RightUniqueZoneUIE : EntityAbstract
//    {
//        public ref Chessy.Common.Component.GameObjectVC Zone => ref Ent.Get<GameObjectVC>();

//        public RightUniqueZoneUIE(in EcsWorld gameW, in Transform uniqueZone) : base(gameW)
//        {
//            Ent.Add(new Chessy.Common.Component.GameObjectVC(uniqueZone.gameObject));
//        }
//    }
//}