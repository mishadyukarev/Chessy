using UnityEngine;

namespace Scripts.Game
{
    public struct SelectorC
    {
        internal static RaycastHit2D RaycastHit2D { get; set; }
        internal static RaycastGettedTypes RaycastGettedType { get; set; }
        internal static bool Is(RaycastGettedTypes raycastGettedType) => RaycastGettedType == raycastGettedType;

        internal static CellClickTypes CellClickType { get; set; }
        internal static bool IsCellClickType(CellClickTypes cellClickType) => cellClickType == CellClickType;
        internal static void DefCellClickType() => CellClickType = default;


        internal static LevelUnitTypes LevelSelUnitType;
        internal static bool IsSelUnit => SelUnitType != default;
        internal static void DefSelectedUnit() => SelUnitType = default;

        internal static UnitTypes SelUnitType;


        internal static ToolWeaponTypes TWTypeForGive;
        internal static void DefTWTypeForGive() => TWTypeForGive = default;

        internal static LevelTWTypes LevelTWType;


        internal static UnitTypes UnitTypeOldToNew;


        internal static byte IdxCurCell { get; set; }
        internal static byte IdxSelCell { get; set; }
        internal static byte IdxPreCell { get; set; }
        internal static byte IdxPreVisionCell { get; set; }

        internal static bool IsSelCell => IdxSelCell != default;
        internal static bool IsStartDirectToCell => IdxCurCell == default;
        internal static void DefSelectedCell() => IdxSelCell = 0;


        internal SelectorC(ToolWeaponTypes toolAndWeaponType) : this()
        {
            TWTypeForGive = toolAndWeaponType;
            LevelTWType = LevelTWTypes.Iron;

            IdxSelCell = default;
        }
    }
}