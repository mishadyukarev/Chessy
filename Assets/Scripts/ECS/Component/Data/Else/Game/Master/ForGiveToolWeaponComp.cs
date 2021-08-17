using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;

namespace Assets.Scripts.ECS.Component.Data.Else.Game.Master
{
    internal struct ForGiveToolWeaponComp
    {
        internal ToolWeaponTypes ToolAndWeaponType { get; set; }
        internal byte IdxCell { get; set; }
    }
}
