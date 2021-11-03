using UnityEngine;

namespace Scripts.Game
{
    public struct SelectorC
    {
        public static RaycastHit2D RaycastHit2D { get; set; }
        public static RaycastGettedTypes RaycastGettedType { get; set; }
        public static bool Is(RaycastGettedTypes raycastGettedType) => RaycastGettedType == raycastGettedType;

        public static CellClickTypes CellClickType { get; set; }
        public static bool Is(CellClickTypes cellClickType) => cellClickType == CellClickType;
        public static void DefCellClickType() => CellClickType = default;


        public static LevelUnitTypes LevelSelUnitType;
        public static bool IsSelUnit => SelUnitType != default;
        public static void DefSelectedUnit() => SelUnitType = default;

        public static UnitTypes SelUnitType;


        public static ToolWeaponTypes TWTypeForGive;
        public static void DefTWTypeForGive() => TWTypeForGive = default;

        //public static LevelTWTypes LevelTWType;


        public static UnitTypes UnitTypeOldToNew;


        public static byte IdxCurCell { get; set; }
        public static byte IdxSelCell { get; set; }
        public static byte IdxPreCell { get; set; }
        public static byte IdxPreVisionCell { get; set; }

        public static bool IsSelCell => IdxSelCell != default;
        public static bool IsStartDirectToCell => IdxCurCell == default;
        public static void DefSelectedCell() => IdxSelCell = 0;


        public SelectorC(ToolWeaponTypes toolAndWeaponType) : this()
        {
            TWTypeForGive = toolAndWeaponType;
            //LevelTWType = LevelTWTypes.Iron;

            IdxSelCell = default;
        }
    }
}