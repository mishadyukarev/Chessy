using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Component.Game.Master
{
    internal struct ForUpgradeMasCom
    {
        internal UpgradeModTypes UpgradeModType { get; set; }
        internal byte IdxForUpgradeUnit { get; set; }

        internal BuildingTypes BuildingType { get; set; }
    }
}
