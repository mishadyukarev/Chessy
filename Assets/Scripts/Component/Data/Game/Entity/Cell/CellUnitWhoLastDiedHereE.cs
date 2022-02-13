using ECS;

namespace Game.Game
{
    public sealed class CellUnitWhoLastDiedHereE : CellEntityAbstract
    {
        ref UnitTC UnitTC => ref Ent.Get<UnitTC>();
        ref LevelTC LevelTC => ref Ent.Get<LevelTC>();
        ref PlayerTC PlayerTC => ref Ent.Get<PlayerTC>();

        public UnitTypes Unit
        {
            get => UnitTC.Unit;
            set => UnitTC.Unit = value;
        }
        public LevelTypes Level
        {
            get => LevelTC.Level;
            set => LevelTC.Level = value;
        }
        public PlayerTypes Owner
        {
            get => PlayerTC.Player;
            set => PlayerTC.Player = value;
        }

        public bool Is(params UnitTypes[] units) => UnitTC.Is(units);
        public bool Is(params LevelTypes[] levels) => LevelTC.Is(levels);
        public bool Is(params PlayerTypes[] players) => PlayerTC.Is(players);

        public bool HaveDeadUnit => !Is(UnitTypes.None, UnitTypes.End);

        internal CellUnitWhoLastDiedHereE(in byte idx, in EcsWorld gameW) : base(idx, gameW)
        {
        }

        internal void SetLastDied(in CellUnitE unitE)
        {
            UnitTC.Unit = unitE.Unit;
            LevelTC.Level = unitE.Level;
            PlayerTC.Player = unitE.Owner;
        }
        internal void Clear()
        {
            UnitTC.Unit = UnitTypes.None;
            LevelTC.Level = LevelTypes.None;
            PlayerTC.Player = PlayerTypes.None;
        }
    }
}