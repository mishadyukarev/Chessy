using ECS;

namespace Game.Game
{
    public sealed class SelectedUnitE : EntityAbstract
    {
        ref UnitTC UnitTCRef => ref Ent.Get<UnitTC>();
        ref LevelTC LevelTCRef => ref Ent.Get<LevelTC>();

        public UnitTC UnitTC => Ent.Get<UnitTC>();
        public LevelTC LevelTC => Ent.Get<LevelTC>();

        public LevelTypes LevelT
        {
            get => LevelTCRef.Level;
            internal set => LevelTCRef.Level = value;
        }
        public bool IsSelectedUnit => UnitTC.Unit != UnitTypes.None && UnitTC.Unit != UnitTypes.End;

        internal SelectedUnitE(in EcsWorld gameW) : base(gameW)
        {

        }

        public void SetSelectedUnit(in UnitTypes unitT, in LevelTypes level, in ClickerObjectE clickerObjectE)
        {
            UnitTCRef.Unit = unitT;
            LevelTCRef.Level = level;
            clickerObjectE.CellClickCRef.Click = CellClickTypes.SetUnit;
        }
    }
}