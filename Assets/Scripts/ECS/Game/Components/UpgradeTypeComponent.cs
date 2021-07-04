using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Game.Components
{
    internal struct UpgradeTypeComponent
    {
        private UpgradeModTypes _upgradeModType;

        internal UpgradeModTypes UpgradeModType
        {
            get => _upgradeModType;
            set => _upgradeModType = value;
        }

        internal void StartFill()
        {

        }
    }
}
