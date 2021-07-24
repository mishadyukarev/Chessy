using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Game.Components
{
    internal struct UpgradeModTypeComponent
    {
        internal UpgradeModTypes UpgradeModType { get; set; }

        internal void StartFill(UpgradeModTypes upgradeModType = default) => UpgradeModType = upgradeModType;
    }
}
