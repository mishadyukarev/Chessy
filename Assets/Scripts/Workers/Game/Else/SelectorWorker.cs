using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Cell;
using System;

namespace Assets.Scripts.Workers
{
    internal class SelectorWorker : MainGeneralWorker
    {
        internal static bool IsSelectedCell => EGGM.SelectorEnt_SelectorCom.IsSelected;
        internal static bool HaveAnySelectorUnit => EGGM.SelectorEnt_UnitTypeCom.UnitType != UnitTypes.None;
        internal static UnitTypes SelectorUnitType => EGGM.SelectorEnt_UnitTypeCom.UnitType;
        internal static UpgradeModTypes UpgradeModType
        {
            get => EGGM.SelectorEnt_UpgradeModTypeCom.UpgradeModType;
            set => EGGM.SelectorEnt_UpgradeModTypeCom.UpgradeModType = value;
        }
        internal static bool IsUpgradeModType(UpgradeModTypes upgradeModType) => UpgradeModType == upgradeModType;
        internal static void ResetUpgradeModType() => UpgradeModType = UpgradeModTypes.None;

        internal static void SetXy(SelectorCellTypes selectorCellType, params int[] xy)
        {
            switch (selectorCellType)
            {
                case SelectorCellTypes.None:
                    throw new Exception();

                case SelectorCellTypes.Current:
                    EGGM.XyCurrentCellEnt_XyCellCom.SetXyCell(xy);
                    break;

                case SelectorCellTypes.Selected:
                    EGGM.XySelectedCellEnt_XyCellCom.SetXyCell(xy);
                    break;

                case SelectorCellTypes.Previous:
                    EGGM.XyPreviousCellEnt_XyCellCom.SetXyCell(xy);
                    break;

                case SelectorCellTypes.PreviousVision:
                    EGGM.XyPreviousVisionCellEnt_XyCellCom.SetXyCell(xy);
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