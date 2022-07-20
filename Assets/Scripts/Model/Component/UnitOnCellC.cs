namespace Chessy.Model.Component
{
    public sealed class UnitOnCellC
    {
        internal UnitTypes UnitT;
        internal LevelTypes LevelT;
        internal PlayerTypes PlayerT;
        internal ConditionUnitTypes ConditionT;
        internal bool IsArcherDirectedToRight;

        internal double DamageSimpleAttack;
        internal double DamageOnCell;
        internal int HowManySecondUnitWasHereInThisCondition;
        internal int CooldownForAttackAnyUnitInSeconds;

        public UnitTypes UnitType => UnitT;
        public LevelTypes LevelType => LevelT;
        public PlayerTypes PlayerType => PlayerT;
        public ConditionUnitTypes ConditionType => ConditionT;
        public bool IsArcherDirectedToRightP => IsArcherDirectedToRight;

        public bool HaveCoolDownForAttackAnyUnit => CooldownForAttackAnyUnitInSeconds > 0;
        public bool HaveUnit => UnitT.HaveUnit();


        internal void Clone(in UnitOnCellC newUnitC)
        {
            UnitT = newUnitC.UnitT;
            LevelT = newUnitC.LevelT;
            PlayerT = newUnitC.PlayerT;
            ConditionT = newUnitC.ConditionT;
            IsArcherDirectedToRight = newUnitC.IsArcherDirectedToRight;

            DamageSimpleAttack = newUnitC.DamageSimpleAttack;
            DamageOnCell = newUnitC.DamageOnCell;
            HowManySecondUnitWasHereInThisCondition = newUnitC.HowManySecondUnitWasHereInThisCondition;
            CooldownForAttackAnyUnitInSeconds = newUnitC.CooldownForAttackAnyUnitInSeconds;
        }

        internal void Set(in (UnitTypes, LevelTypes, PlayerTypes, ConditionUnitTypes, bool) unitC)
        {
            UnitT = unitC.Item1;
            LevelT = unitC.Item2;
            PlayerT = unitC.Item3;
            ConditionT = unitC.Item4;
            IsArcherDirectedToRight = unitC.Item5;
        }
    }
}