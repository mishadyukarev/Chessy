using ECS;

namespace Game.Game
{
    public sealed class CellUnitOwnerE : CellEntityAbstract
    {
        ref PlayerTC OwnerCRef => ref Ent.Get<PlayerTC>();
        public PlayerTC OwnerC => Ent.Get<PlayerTC>();

        public PlayerTypes PlayerT
        {
            get => OwnerC.Player;
            set => OwnerCRef.Player = value;
        }
        public bool Is(params PlayerTypes[] owners) => OwnerC.Is(owners);

        internal CellUnitOwnerE(in byte idx, in EcsWorld gameW) : base(idx, gameW)
        {
        }
    }
}