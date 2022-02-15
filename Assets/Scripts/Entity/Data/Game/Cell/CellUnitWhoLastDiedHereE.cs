using ECS;

namespace Game.Game
{
    public sealed class CellUnitWhoLastDiedHereE : CellEntityAbstract
    {
        public ref UnitTC UnitTC => ref Ent.Get<UnitTC>();
        public ref LevelTC LevelTC => ref Ent.Get<LevelTC>();
        public ref PlayerTC PlayerTC => ref Ent.Get<PlayerTC>();

        public bool HaveDeadUnit => !UnitTC.Is(UnitTypes.None, UnitTypes.End);

        internal CellUnitWhoLastDiedHereE(in CellEs cellEs, in EcsWorld gameW) : base(cellEs, gameW)
        {
        }

        public void SetLastDied(in CellUnitE unitE)
        {
            UnitTC.Unit = unitE.UnitTC.Unit;
            LevelTC.Level = unitE.LevelTC.Level;
            PlayerTC.Player = unitE.PlayerTC.Player;
        }
        public void Clear()
        {
            UnitTC.Unit = UnitTypes.None;
            LevelTC.Level = LevelTypes.None;
            PlayerTC.Player = PlayerTypes.None;
        }
    }
}