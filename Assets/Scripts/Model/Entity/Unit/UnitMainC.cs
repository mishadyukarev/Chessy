namespace Chessy.Model.Component
{
    public struct UnitMainC
    {
        public UnitTypes UnitT { get; internal set; }
        public LevelTypes LevelT { get; internal set; }
        public PlayerTypes PlayerT { get; internal set; }
        public ConditionUnitTypes ConditionT { get; internal set; }
        public bool IsRightArcher { get; internal set; }
        public double DamageSimpleAttack { get; internal set; }
        public double DamageOnCell { get; internal set; }
}
}