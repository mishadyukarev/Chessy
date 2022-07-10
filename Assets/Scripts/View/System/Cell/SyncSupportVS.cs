using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.System;
using Chessy.View.UI.Entity;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncSupportVS : SystemViewAbstract
    {
        readonly static bool[] _needActive = new bool[IndexCellsValues.CELLS];
        readonly static Color[] _needColor = new Color[IndexCellsValues.CELLS];

        readonly EntitiesView _eVG;

        internal SyncSupportVS(in EntitiesView eVG, in EntitiesModel eMG) : base(eMG)
        {
            _eVG = eVG;
        }

        internal sealed override void Sync()
        {
            for (byte cell_start = 0; cell_start < IndexCellsValues.CELLS; cell_start++)
            {
                _needActive[cell_start] = false;


                switch (_e.CellClickT)
                {
                    case CellClickTypes.UniqueAbility:

                        switch (_e.SelectedE.AbilityT)
                        {
                            case AbilityTypes.ChangeDirectionWind:
                                if (_e.HaveFire(cell_start))
                                {
                                    _needActive[cell_start] = true;
                                    _needColor[cell_start] = ColorsValues.Color(_e.SelectedE.AbilityT);
                                }
                                break;
                        }
                        break;

                    default:
                        break;
                }



            }

            _needActive[_e.SelectedCellIdx] = true;
            _needColor[_e.SelectedCellIdx] = ColorsValues.Color(SupportCellVisionTypes.Selector);


            if (_e.CellClickT.Is(CellClickTypes.UniqueAbility))
            {
                if (_e.SelectedE.AbilityT.Is(AbilityTypes.ChangeDirectionWind))
                {
                    for (byte curCellIdx = 0; curCellIdx < IndexCellsValues.CELLS; curCellIdx++)
                    {
                        if (_e.HaveCloud(curCellIdx))
                        {
                            if (!_e.IsCenterCloud(curCellIdx))
                            {
                                _needActive[curCellIdx] = true;
                                _needColor[curCellIdx] = ColorsValues.Color(SupportCellVisionTypes.Shift);
                            }
                        } 
                    }
                }

                else if (_e.SelectedE.AbilityT.Is(AbilityTypes.FireArcher))
                {
                    for (byte idxCell = 0; idxCell < IndexCellsValues.CELLS; idxCell++)
                    {
                        if (_e.WhereUnitCanFireAdultForestC(_e.SelectedCellIdx).Can(idxCell))
                        {
                            _needActive[idxCell] = true;
                            _needColor[idxCell] = ColorsValues.Color(AbilityTypes.FireArcher);
                        }
                    }
                }
            }


            else
            {
                if (_e.UnitT(_e.SelectedCellIdx).HaveUnit())
                {
                    if (_e.UnitPlayerT(_e.SelectedCellIdx).Is(_e.CurrentPlayerIT))
                    {
                        if (!_e.CellClickT.Is(CellClickTypes.GiveTakeTW))
                        {
                            for (byte idxCell = 0; idxCell < IndexCellsValues.CELLS; idxCell++)
                            {
                                if (_e.WhereUnitCanShiftC(_e.SelectedCellIdx).CanShiftHere(idxCell))
                                {
                                    _needActive[idxCell] = true;
                                    _needColor[idxCell] = ColorsValues.Color(SupportCellVisionTypes.Shift);
                                }

                                if (_e.WhereUnitCanAttackSimpleAttackToEnemyC(_e.SelectedCellIdx).Can(idxCell))
                                {
                                    _needActive[idxCell] = true;
                                    _needColor[idxCell] = ColorsValues.Color(SupportCellVisionTypes.SimpleAttack);
                                }

                                if (_e.WhereUnitCanAttackUniqueAttackToEnemyC(_e.SelectedCellIdx).Can(idxCell))
                                {
                                    _needActive[idxCell] = true;
                                    _needColor[idxCell] = ColorsValues.Color(SupportCellVisionTypes.UniqueAttack);
                                }
                            }
                        }
                    }
                }
            }



            for (byte cell_start = 0; cell_start < IndexCellsValues.CELLS; cell_start++)
            {
                _eVG.CellEs(cell_start).SupportCellEs.SupportSRC.SetActiveGO(_needActive[cell_start]);
                _eVG.CellEs(cell_start).SupportCellEs.SupportSRC.SetColor(_needColor[cell_start]);
            }
        }
    }
}