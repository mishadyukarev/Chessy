//using ECS;
//using UnityEngine;

//namespace Game.Game
//{
//    public sealed class RightBuildingZoneUIE : EntityAbstract
//    {
//        public ref Chessy.Common.Component.GameObjectVC Parent => ref Ent.Get<GameObjectVC>();

//        public RightBuildingZoneUIE(in EcsWorld gameW, in Transform buildingZone) : base(gameW)
//        {
//            Ent
//                .Add(new Chessy.Common.Component.GameObjectVC(buildingZone.gameObject));
//        }
//    }
//}