using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.Cell;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;

namespace Assets.Scripts.ECS.Component.Data.Else.Game.Master
{
    internal struct ForGiveExtraPawnToolComp
    {
        internal GiveTakeTypes TakeGiveType { get; set; }
        internal UnitSlotTypes UnitSlotType { get; set; }
        internal ToolWeaponTypes ToolAndWeaponType { get; set; }
        internal byte IdxCell { get; set; }
    }
}
