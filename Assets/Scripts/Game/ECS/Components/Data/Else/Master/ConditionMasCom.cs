using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Component.Game.Master
{
    internal struct ConditionMasCom
    {
        internal CondUnitTypes NeededCondUnitType { get; set; }
        internal byte IdxForCondition { get; set; }
    }
}
