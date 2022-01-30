using ECS;

namespace Game.Game
{
    public sealed class CellUnitMainE : EntityAbstract
    {
        public ref UnitTC UnitC => ref Ent.Get<UnitTC>();
        public ref LevelTC LevelC => ref Ent.Get<LevelTC>();
        public ref PlayerTC OwnerC => ref Ent.Get<PlayerTC>();
        public ref ConditionUnitC ConditionC => ref Ent.Get<ConditionUnitC>();
        public ref IsC IsCorned => ref Ent.Get<IsC>();

        public CellUnitMainE(in EcsWorld gameW) : base(gameW) { }

        public void ChangeCorner() => IsCorned.Is = !IsCorned.Is;

        public void Shift(CellUnitMainE unitElse_from)
        {
            OwnerC = unitElse_from.OwnerC;
            LevelC = unitElse_from.LevelC;
            ConditionC.Reset();
            IsCorned = unitElse_from.IsCorned;

            UnitC.Unit = unitElse_from.UnitC.Unit;
            unitElse_from.UnitC.Reset();
        }

        public void SetNew(in UnitTypes unitT, in LevelTypes levelT, in PlayerTypes owner)
        {
            UnitC.Unit = unitT;
            LevelC.Level = levelT;
            OwnerC.Player = owner;
            ConditionC.Reset();
            IsCorned.Reset();
        }
        public void SetNew(in (UnitTypes, LevelTypes, PlayerTypes) unit)
        {
            UnitC.Unit = unit.Item1;
            LevelC.Level = unit.Item2;
            OwnerC.Player = unit.Item3;
            ConditionC.Reset();
            IsCorned.Reset();
        }
        public void Reset()
        {
            UnitC.Reset();
            LevelC.Reset();
            OwnerC.Reset();
            ConditionC.Reset();
            IsCorned.Reset();
        }
    }
}