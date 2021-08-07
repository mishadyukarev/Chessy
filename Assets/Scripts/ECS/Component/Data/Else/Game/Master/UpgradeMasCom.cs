using Assets.Scripts.Abstractions.Enums;

namespace Assets.Scripts.ECS.Component.Game.Master
{
    internal struct UpgradeMasCom
    {
        internal UpgradeModTypes UpgradeModType { get; set; }
        internal BuildingTypes BuildingType { get; set; }
    }
}
