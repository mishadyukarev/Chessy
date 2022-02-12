using ECS;

namespace Game.Game
{
    public sealed class CellUnitWhoLastDiedHereE : CellEntityAbstract
    {
        ref UnitTC UnitTCRef => ref Ent.Get<UnitTC>();
        ref LevelTC LevelTCRef => ref Ent.Get<LevelTC>();
        ref PlayerTC OwnerCRef => ref Ent.Get<PlayerTC>();

        public UnitTC UnitTC => Ent.Get<UnitTC>();
        public LevelTC LevelTC => Ent.Get<LevelTC>();
        public PlayerTC OwnerC => Ent.Get<PlayerTC>();

        public bool HaveDeadUnit => UnitTC.Unit != UnitTypes.None;

        internal CellUnitWhoLastDiedHereE(in byte idx, in EcsWorld gameW) : base(idx, gameW)
        {
        }

        internal void SetLastDied(in CellUnitE unitE)
        {
            UnitTCRef.Unit = unitE.UnitTC.Unit;
            LevelTCRef.Level = unitE.Level;
            OwnerCRef.Player = unitE.Owner;
        }
        internal void Clear()
        {
            UnitTCRef.Unit = UnitTypes.None;
            LevelTCRef.Level = LevelTypes.None;
            OwnerCRef.Player = PlayerTypes.None;
        }
    }
}