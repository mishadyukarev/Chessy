//using ECS;

//namespace Game.Game
//{
//    public sealed class EndTeleportE : EntityAbstract
//    {
//        public IdxC WhereC => Ent.Get<IdxC>();
//        public ref PlayerTC OwnerC => ref Ent.Get<PlayerTC>();



//        internal EndTeleportE(in EcsWorld gameW) : base(gameW)
//        {

//        }

//        public void Set(in byte idx, in PlayerTypes player)
//        {
//            WhereC.Set(idx);
//            OwnerC.Player = player;
//        }
//        public void Reset()
//        {
//            WhereC.Set(0);
//            OwnerC.Player = PlayerTypes.None;
//        }
//    }
//}