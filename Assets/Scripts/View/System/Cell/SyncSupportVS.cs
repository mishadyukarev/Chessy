using Chessy.Model.Model.Entity;
using Chessy.Model.Values;
using Chessy.Model.View.System;
using UnityEngine;

namespace Chessy.Model
{
    sealed class SyncSupportVS : SystemViewAbstract
    {
        readonly static bool[] _needActive = new bool[StartValues.CELLS];
        readonly static Color[] _needColor = new Color[StartValues.CELLS];

        readonly EntitiesView _eVG;

        internal SyncSupportVS(in EntitiesView eVG, in EntitiesModel eMG) : base(eMG)
        {
            _eVG = eVG;
        }

        internal sealed override void Sync()
        {
            for (byte cell_start = 0; cell_start < StartValues.CELLS; cell_start++)
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
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var cell_target = _e.AroundCellsE(_e.WeatherE.CloudC.CellIdxCenterCloud).IdxCell(dirT);

                        _needActive[cell_target] = true;
                        _needColor[cell_target] = ColorsValues.Color(SupportCellVisionTypes.Shift);
                    }
                }

                else if (_e.SelectedE.AbilityT.Is(AbilityTypes.FireArcher))
                {
                    for (byte idxCell = 0; idxCell < StartValues.CELLS; idxCell++)
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
                    if (_e.UnitPlayerT(_e.SelectedCellIdx).Is(_e.CurPlayerIT))
                    {
                        if (!_e.CellClickT.Is(CellClickTypes.GiveTakeTW))
                        {
                            for (byte idxCell = 0; idxCell < StartValues.CELLS; idxCell++)
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



            for (byte cell_start = 0; cell_start < StartValues.CELLS; cell_start++)
            {
                _eVG.CellEs(cell_start).SupportCellEs.SupportSRC.SetActive(_needActive[cell_start]);
                _eVG.CellEs(cell_start).SupportCellEs.SupportSRC.SR.color = _needColor[cell_start];
            }
        }
    }
}