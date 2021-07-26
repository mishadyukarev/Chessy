using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Cell;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers
{
    internal class SelectorWorker : MainGeneralWorker
    {
        internal static UnitTypes SelectedUnitType
        {
            get => EGGM.SelectorEnt_UnitTypeCom.UnitType;
            set => EGGM.SelectorEnt_UnitTypeCom.UnitType = value;
        }

        internal static bool IsSelectedCell
        {
            get => EGGM.SelectorEnt_SelectorCom.IsSelected;
            set => EGGM.SelectorEnt_SelectorCom.IsSelected = value;
        }
        internal static bool HaveAnySelectorUnit => EGGM.SelectorEnt_UnitTypeCom.UnitType != UnitTypes.None;
        internal static UnitTypes SelectorUnitType => EGGM.SelectorEnt_UnitTypeCom.UnitType;

        internal static UpgradeModTypes UpgradeModType
        {
            get => EGGM.SelectorEnt_UpgradeModTypeCom.UpgradeModType;
            set => EGGM.SelectorEnt_UpgradeModTypeCom.UpgradeModType = value;
        }
        internal static bool IsUpgradeModType(UpgradeModTypes upgradeModType) => UpgradeModType == upgradeModType;
        internal static void ResetUpgradeModType() => UpgradeModType = UpgradeModTypes.None;

        internal static RaycastHit2D RaycastHit2D
        {
            get => EGGM.SelectorEnt_RayCom.RaycastHit2D;
            set => EGGM.SelectorEnt_RayCom.RaycastHit2D = value;
        }
        internal static RaycastGettedTypes RaycastGettedType
        {
            get => EGGM.SelectorEnt_RayCom.RaycastGettedType;
            set => EGGM.SelectorEnt_RayCom.RaycastGettedType = value;
        }

        internal static bool IsClick
        {
            get => EGGM.InputEnt_InputCom.IsClick;
            set => EGGM.InputEnt_InputCom.IsClick = value;
        }

        internal static bool CanShiftUnit
        {
            get => EGGM.SelectorEnt_SelectorCom.CanShiftUnit;
            set => EGGM.SelectorEnt_SelectorCom.CanShiftUnit = value;
        }
        internal static bool CanExecuteStartClick
        {
            get => EGGM.SelectorEnt_SelectorCom.CanExecuteStartClick;
            set => EGGM.SelectorEnt_SelectorCom.CanExecuteStartClick = value;
        }
        internal static bool IsStartSelectedDirect
        {
            get => EGGM.SelectorEnt_SelectorCom.IsStartSelectedDirect;
            set => EGGM.SelectorEnt_SelectorCom.IsStartSelectedDirect = value;
        }

        internal static void SetXy(SelectorCellTypes selectorCellType, params int[] xy)
        {
            switch (selectorCellType)
            {
                case SelectorCellTypes.None:
                    throw new Exception();

                case SelectorCellTypes.Current:
                    EGGM.XyCurrentCellEnt_XyCellCom.XyCell = xy;
                    break;

                case SelectorCellTypes.Selected:
                    EGGM.XySelectedCellEnt_XyCellCom.XyCell = xy;
                    break;

                case SelectorCellTypes.Previous:
                    EGGM.XyPreviousCellEnt_XyCellCom.XyCell = xy;
                    break;

                case SelectorCellTypes.PreviousVision:
                    EGGM.XyPreviousVisionCellEnt_XyCellCom.XyCell = xy;
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static int[] GetXy(SelectorCellTypes selectorCellType)
        {
            switch (selectorCellType)
            {
                case SelectorCellTypes.None:
                    throw new Exception();

                case SelectorCellTypes.Current:
                    return EGGM.XyCurrentCellEnt_XyCellCom.XyCell;

                case SelectorCellTypes.Selected:
                    return EGGM.XySelectedCellEnt_XyCellCom.XyCell;

                case SelectorCellTypes.Previous:
                    return EGGM.XyPreviousCellEnt_XyCellCom.XyCell;

                case SelectorCellTypes.PreviousVision:
                    return EGGM.XyPreviousVisionCellEnt_XyCellCom.XyCell;

                default:
                    throw new Exception();
            }
        }


    }
}