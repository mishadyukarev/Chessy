using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Game.Components
{
    internal struct ProtectRelaxComponent
    {
        internal ConditionTypes ProtectRelaxType { get; set; }


        internal void StartFill(ConditionTypes protectRelaxType = default) => ProtectRelaxType = protectRelaxType;
    }
}
