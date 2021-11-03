namespace Scripts.Game
{
    public struct ConditionUnitC
    {
        public CondUnitTypes CondUnitType { get; set; }
        public bool HaveCondition => CondUnitType != default;

        public void DefCondition() => CondUnitType = default;
        public bool Is(CondUnitTypes condUnitType) => CondUnitType == condUnitType;
        public bool Is(CondUnitTypes[] condUnitTypes)
        {
            foreach (var condUnitType in condUnitTypes)
                if (Is(condUnitType)) return true;
            return false;
        }
    }
}