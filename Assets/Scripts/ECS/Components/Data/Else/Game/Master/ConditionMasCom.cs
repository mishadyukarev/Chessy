using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Component.Game.Master
{
    internal struct ConditionMasCom
    {
        internal ConditionUnitTypes NeededCondUnitType { get; set; }
        internal byte IdxForCondition { get; set; }
    }
}
