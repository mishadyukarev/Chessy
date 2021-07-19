using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Game.Components
{
    internal struct UpgradeModTypeComponent
    {
        private UpgradeModTypes _upgradeModType;
        internal UpgradeModTypes UpgradeModType => _upgradeModType;


        internal void StartFill(UpgradeModTypes upgradeModType = default) => _upgradeModType = upgradeModType;
        internal void SetUpgradeModType(UpgradeModTypes upgradeModType) => _upgradeModType = upgradeModType;
        internal void ResetUpgradeModType() => _upgradeModType = default;
        internal bool IsUpgradeModType(UpgradeModTypes upgradeModType) => _upgradeModType == upgradeModType;
    }
}
