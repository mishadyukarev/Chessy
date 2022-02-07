using ECS;

namespace Game.Game
{
    public sealed class EndTeleportE : EntityAbstract
    {
        ref IdxC WhereCRef => ref Ent.Get<IdxC>();
        ref PlayerTC OwnerCRef => ref Ent.Get<PlayerTC>();

        public IdxC WhereC => Ent.Get<IdxC>();
        public PlayerTC OwnerC => Ent.Get<PlayerTC>();

        public bool HaveEnd => WhereC.Idx > 0;

        internal EndTeleportE(in EcsWorld gameW) : base(gameW)
        {

        }

        public void Set(in byte idx, in PlayerTypes player)
        {
            WhereCRef.Idx = idx;
            OwnerCRef.Player = player;
        }
        public void Reset()
        {
            WhereCRef.Idx = 0;
            OwnerCRef.Player = PlayerTypes.None;
        }
    }
}