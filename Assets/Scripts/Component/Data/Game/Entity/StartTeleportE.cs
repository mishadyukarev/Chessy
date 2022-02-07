using ECS;

namespace Game.Game
{
    public sealed class StartTeleportE : EntityAbstract
    {
        ref IdxC WhereCRef => ref Ent.Get<IdxC>();
        ref PlayerTC OwnerCRef => ref Ent.Get<PlayerTC>();

        public IdxC WhereC => Ent.Get<IdxC>();
        public PlayerTC OwnerC => Ent.Get<PlayerTC>();

        public byte Where
        {
            get => WhereCRef.Idx;
            internal set => WhereCRef.Idx = value;
        }
        public bool HaveStart => WhereC.Idx > 0;

        internal StartTeleportE(in EcsWorld gameW) : base(gameW)
        {

        }

        public void Set(in byte idx, in PlayerTypes player)
        {
            WhereCRef.Idx = idx;
            OwnerCRef.Player = player;
        }
        public void Set(in EndTeleportE endTeleportE)
        {
            WhereCRef = endTeleportE.WhereC;
            OwnerCRef = endTeleportE.OwnerC;
        }
        public void Reset()
        {
            WhereCRef.Idx = 0;
            OwnerCRef.Player = PlayerTypes.None;
        }
    }
}