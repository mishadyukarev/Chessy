namespace Chessy.Model.Component
{
    public struct UnitOnCellC
    {
        internal UnitTypes UnitType;
        internal LevelTypes LevelType;
        internal PlayerTypes PlayerType;
        internal ConditionUnitTypes ConditionType;
        internal bool IsArcherDirectedToRight;

        internal double DamageSimpleAttack;
        internal double DamageOnCell;
        internal int HowManySecondUnitWasHereInThisCondition;
        internal int CooldownForAttackAnyUnitInSeconds;

        public UnitTypes UnitT => UnitType;
        public LevelTypes LevelT => LevelType;
        public PlayerTypes PlayerT => PlayerType;
        public ConditionUnitTypes ConditionT => ConditionType;
        public bool IsArcherDirectedToRightP => IsArcherDirectedToRight;

        public bool HaveCoolDownForAttackAnyUnit => CooldownForAttackAnyUnitInSeconds > 0;
    }
}