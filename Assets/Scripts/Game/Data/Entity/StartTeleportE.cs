//using ECS;

//namespace Game.Game
//{
//    public sealed class StartTeleportE : EntityAbstract
//    {


//        public byte Idx => IdxC.Idx;

//        internal StartTeleportE(in EcsWorld gameW) : base(gameW)
//        {

//        }

//        public void Set(in byte idx, in PlayerTypes player)
//        {
//            IdxC.Set(idx);
//            PlayerTC.Player = player;
//        }
//        public void Set(in EndTeleportE endTeleportE)
//        {
//            IdxC = endTeleportE.WhereC;
//            PlayerTC = endTeleportE.OwnerC;
//        }
//        public void Reset()
//        {
//            IdxC.Set(0);
//            PlayerTC.Player = PlayerTypes.None;
//        }
//    }
//}