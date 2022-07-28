namespace Chessy.Model.Component
{
    public sealed class UnitOnCellC
    {
        internal UnitTypes UnitT;
        internal LevelTypes LevelT;
        internal PlayerTypes PlayerT;
        internal ConditionUnitTypes ConditionT;
        internal bool IsArcherDirectedToRight;
        internal int HowManySecondUnitWasHereInThisCondition;

        public UnitTypes UnitType => UnitT;
        public LevelTypes LevelType => LevelT;
        public PlayerTypes PlayerType => PlayerT;
        public ConditionUnitTypes ConditionType => ConditionT;
        public bool IsArcherDirectedToRightP => IsArcherDirectedToRight;

        public bool HaveUnit => UnitT.HaveUnit();
        public bool IsAnimal => UnitT.IsAnimal();


        internal void Clone(in UnitOnCellC newUnitC)
        {
            UnitT = newUnitC.UnitT;
            LevelT = newUnitC.LevelT;
            PlayerT = newUnitC.PlayerT;
            ConditionT = newUnitC.ConditionT;
            IsArcherDirectedToRight = newUnitC.IsArcherDirectedToRight;
            HowManySecondUnitWasHereInThisCondition = newUnitC.HowManySecondUnitWasHereInThisCondition;
        }

        internal void Set(in (UnitTypes, LevelTypes, PlayerTypes, ConditionUnitTypes, bool) unitC)
        {
            UnitT = unitC.Item1;
            LevelT = unitC.Item2;
            PlayerT = unitC.Item3;
            ConditionT = unitC.Item4;
            IsArcherDirectedToRight = unitC.Item5;
        }

        internal void Dispose()
        {
            UnitT = default;
            LevelT = default;
            PlayerT = default;
            ConditionT = default;
            IsArcherDirectedToRight = default;
            HowManySecondUnitWasHereInThisCondition = default;
        }
    }
}