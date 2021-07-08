using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Game.Components
{
    internal struct ProtectRelaxComponent
    {
        private ProtectRelaxTypes _protectRelaxType;

        internal ProtectRelaxTypes ProtectRelaxType => _protectRelaxType;
        internal bool IsProtected => _protectRelaxType == ProtectRelaxTypes.Protected;
        internal bool IsRelaxed => _protectRelaxType == ProtectRelaxTypes.Relaxed;

        internal void StartFill() => _protectRelaxType = default;
        internal void SetProtectedRelaxedType(ProtectRelaxTypes protectRelaxType) => _protectRelaxType = protectRelaxType;
        internal void ResetProtectedRelaxedType() => _protectRelaxType = ProtectRelaxTypes.None;
    }
}
