using ECS;

namespace Game.Game
{
    public sealed class MaxPawnsE : EntityAbstract
    {
        readonly PlayerTypes _playerT;

        ref AmountC MaxPawnsCRef => ref Ent.Get<AmountC>();

        public int MaxPawns => MaxPawnsCRef.Amount;

        public bool CanGetPawn(in WhereWorker whereW)
        {
            return MaxPawns - whereW.AmountPaws(_playerT) > 0;
        }

        internal MaxPawnsE(in PlayerTypes player, in EcsWorld gameW) : base(gameW)
        {
            _playerT = player;
            MaxPawnsCRef.Amount = 3;
        }
    }
}