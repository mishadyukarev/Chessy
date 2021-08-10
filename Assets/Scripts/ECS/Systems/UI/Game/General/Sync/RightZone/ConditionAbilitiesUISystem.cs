using Leopotam.Ecs;

internal sealed class ConditionAbilitiesUISystem : IEcsRunSystem
{
    //private EcsFilter<UnitZoneViewUICom> _unitZoneUIFilter = default;
    //private EcsFilter<SelectorComponent> _selectorFilter = default;
    //private int[] XySelectedCell => _selectorFilter.Get1(0).XySelectedCell;

    public void Run()
    {

        //if (CellUnitsDataSystem.HaveAnyUnit(XySelectedCell))
        //{
        //    if (CellUnitsDataSystem.HaveOwner(XySelectedCell))
        //    {
        //        if (CellUnitsDataSystem.IsMine(XySelectedCell))
        //        {
        //            switch (CellUnitsDataSystem.UnitType(XySelectedCell))
        //            {
        //                case UnitTypes.None:
        //                    ActiveStandartAbilities(false);
        //                    break;

        //                case UnitTypes.King:
        //                    ActiveStandartAbilities(true);
        //                    break;

        //                case UnitTypes.Pawn:
        //                    ActiveStandartAbilities(true);
        //                    break;

        //                case UnitTypes.PawnSword:
        //                    ActiveStandartAbilities(true);
        //                    break;

        //                case UnitTypes.Rook:
        //                    ActiveStandartAbilities(true);
        //                    break;

        //                case UnitTypes.RookCrossbow:
        //                    ActiveStandartAbilities(true);
        //                    break;

        //                case UnitTypes.Bishop:
        //                    ActiveStandartAbilities(true);
        //                    break;

        //                case UnitTypes.BishopCrossbow:
        //                    ActiveStandartAbilities(true);
        //                    break;

        //                default:
        //                    break;
        //            }
        //        }

        //        else
        //        {
        //            ActiveStandartAbilities(false);
        //        }
        //    }

        //    else if (CellUnitsDataSystem.IsBot(XySelectedCell))
        //    {
        //        ActiveStandartAbilities(false);
        //    }

        //}
        //else
        //{
        //    ActiveStandartAbilities(false);
        //}

        //void ActiveStandartAbilities(bool isActive)
        //{
        //    _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Condition, isActive);

        //    if (isActive)
        //    {
        //        if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Protected, XySelectedCell))
        //        {
        //            _unitZoneUIFilter.Get1(0).SetColorToConditionButton(ConditionUnitTypes.Protected, Color.yellow);
        //        }

        //        else
        //        {
        //            _unitZoneUIFilter.Get1(0).SetColorToConditionButton(ConditionUnitTypes.Protected, Color.white);
        //        }

        //        if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Relaxed, XySelectedCell))
        //        {
        //            _unitZoneUIFilter.Get1(0).SetColorToConditionButton(ConditionUnitTypes.Relaxed, Color.green);
        //        }
        //        else
        //        {
        //            _unitZoneUIFilter.Get1(0).SetColorToConditionButton(ConditionUnitTypes.Relaxed, Color.white);
        //        }
        //    }
        //}
    }
}
