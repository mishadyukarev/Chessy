using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;

namespace Assets.Scripts.ECS.Component.Data.Else.Game.Master
{
    internal struct ForGiveTakeToolWeaponComp
    {
        internal ToolWeaponTypes ToolWeapType { get; set; }
        internal byte IdxCell { get; set; }
    }
}
