//using ECS;

//namespace Game.Game
//{
//    public sealed class MaxAvailablePawnsE : EntityAbstract
//    {
//        readonly PlayerTypes _playerT;



//        public int MaxPawns => MaxPawnsCRef.Amount;

//        //public bool CanGetPawn(in WhereWorker whereW)
//        //{
//        //    return MaxPawns - whereW.AmountPaws(_playerT) > 0;
//        //}

//        internal MaxAvailablePawnsE(in PlayerTypes player, in EcsWorld gameW) : base(gameW)
//        {
//            _playerT = player;
//            MaxPawnsCRef.Amount = 3;
//        }
//    }
//}