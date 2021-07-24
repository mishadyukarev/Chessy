using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Game.Components
{
    internal struct ProtectRelaxComponent
    {
        internal ProtectRelaxTypes ProtectRelaxType { get; set; }


        internal void StartFill(ProtectRelaxTypes protectRelaxType = default) => ProtectRelaxType = protectRelaxType;
    }
}
