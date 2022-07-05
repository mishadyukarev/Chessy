using Chessy.Model.Values;
namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        internal void GetCellsForShiftUnit()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.IsBorder(cellIdxCurrent)) continue;


                

                for (byte idxCell = 0; idxCell < IndexCellsValues.CELLS; idxCell++)
                {
                    _e.WhereUnitCanShiftC(cellIdxCurrent).SetWhereUnitCanShift(idxCell, false);
                    _e.HowManyEnergyNeedForShiftingUnitC(cellIdxCurrent).SetHowManyEnergyNeedForShiftingToHere(idxCell, 0);
                }


                if (!_e.UnitEffectsC(cellIdxCurrent).IsStunned && _e.UnitT(cellIdxCurrent).HaveUnit() && !_e.UnitT(cellIdxCurrent).IsAnimal())
                {
                    for (var dirT = (DirectTypes)1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_to = _e.AroundCellsE(cellIdxCurrent).IdxCell(dirT);

                        if (!_e.UnitT(idx_to).HaveUnit() && !_e.MountainC(idx_to).HaveAnyResources)
                        {
                            float needEnergy = StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL;

                            if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Tree))
                            {
                                needEnergy = 1;
                            }
                            else
                            {
                                if (!_e.UnitT(cellIdxCurrent).Is(UnitTypes.Undead))
                                {
                                    if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Pawn) && _e.MainToolWeaponT(cellIdxCurrent).Is(ToolsWeaponsWarriorTypes.Staff))
                                    {
                                        needEnergy /= 2;
                                    }
                                    else if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Snowy))
                                    {
                                        if (_e.WaterOnCellC(idx_to).HaveAnyResources)
                                        {
                                            needEnergy = StepValues.FOR_SHIFT_ATTACK_EMPTY_CELL / 2;
                                        }
                                    }


                                    if (_e.AdultForestC(idx_to).HaveAnyResources)
                                    {
                                        if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Pawn))
                                        {
                                            if (!_e.MainToolWeaponT(cellIdxCurrent).Is(ToolsWeaponsWarriorTypes.Staff))
                                            {
                                                needEnergy += StepValues.ADULT_FOREST;

                                                if (_e.HealthTrail(idx_to).IsAlive(dirT.Invert())) needEnergy -= StepValues.BONUS_TRAIL;
                                            }
                                        }
                                        else if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Elfemale))
                                        {
                                            needEnergy /= 2;
                                        }
                                        else
                                        {
                                            needEnergy += StepValues.ADULT_FOREST;

                                            if (_e.HealthTrail(idx_to).IsAlive(dirT.Invert())) needEnergy -= StepValues.BONUS_TRAIL;
                                        }
                                    }
                                    else
                                    {
                                        if (!_e.UnitT(cellIdxCurrent).Is(UnitTypes.Elfemale))
                                        {

                                        }
                                    }

                                    if (_e.HillC(idx_to).HaveAnyResources)
                                    {
                                        if (!_e.MainToolWeaponT(cellIdxCurrent).Is(ToolsWeaponsWarriorTypes.Staff))
                                        {
                                            needEnergy += StepValues.HILL;
                                        }
                                    }
                                }
                            }



                            _e.HowManyEnergyNeedForShiftingUnitC(cellIdxCurrent).SetHowManyEnergyNeedForShiftingToHere(idx_to, needEnergy);

                            _e.WhereUnitCanShiftC(cellIdxCurrent).SetWhereUnitCanShift(idx_to, true);

                            //if (needEnergy <= _e.EnergyUnitC(cellIdxCurrent).Energy || _e.EnergyUnitC(cellIdxCurrent).Energy >= StepValues.MAX)
                            //{
                                
                            //}
                        }
                    }
                }
            }
        }
    }
}