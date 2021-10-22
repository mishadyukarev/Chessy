using UnityEngine;

namespace Scripts.Game
{
    public struct SelectorCom
    {
        internal RaycastHit2D RaycastHit2D { get; set; }
        internal RaycastGettedTypes RaycastGettedType { get; set; }
        internal bool Is(RaycastGettedTypes raycastGettedType) => RaycastGettedType == raycastGettedType;

        internal CellClickTypes CellClickType { get; set; }
        internal bool IsCellClickType(CellClickTypes cellClickType) => cellClickType == CellClickType;
        internal void DefCellClickType() => CellClickType = default;


        internal LevelUnitTypes LevelSelUnitType;
        internal bool IsSelUnit => SelUnitType != default;
        internal void DefSelectedUnit() => SelUnitType = default;

        internal UnitTypes SelUnitType;


        internal ToolWeaponTypes TWTypeForGive;
        internal LevelTWTypes LevelTWType;


        internal byte IdxCurCell { get; set; }
        internal byte IdxSelCell { get; set; }
        internal byte IdxPreCell { get; set; }
        internal byte IdxPreVisionCell { get; set; }

        internal bool IsSelCell => IdxSelCell != 0;
        internal bool IsStartDirectToCell => IdxCurCell == default;
        internal void DefSelectedCell() => IdxSelCell = 0;


        internal SelectorCom(ToolWeaponTypes toolAndWeaponType) : this()
        {
            TWTypeForGive = toolAndWeaponType;
            LevelTWType = LevelTWTypes.Iron;
        }
    }
}