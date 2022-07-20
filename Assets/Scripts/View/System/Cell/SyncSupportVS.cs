using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using Chessy.View.System;
using Chessy.View.UI.Entity;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncSupportVS : SystemViewAbstract
    {
        readonly bool[] _needActive = new bool[IndexCellsValues.CELLS];
        readonly bool[] _wasActivated = new bool[IndexCellsValues.CELLS];
        readonly Color[] _needColor = new Color[IndexCellsValues.CELLS];

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

                        switch (_aboutGameC.AbilityType)
                        {
                            case AbilityTypes.ChangeDirectionWind:
                                if (_e.HaveFire(cell_start))
                                {
                                    _needActive[cell_start] = true;
                                    _needColor[cell_start] = ColorsValues.Color(_aboutGameC.AbilityType);
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
                if (_aboutGameC.AbilityType == AbilityTypes.ChangeDirectionWind)
                {
                    for (byte curCellIdx = 0; curCellIdx < IndexCellsValues.CELLS; curCellIdx++)
                    {
                        if (_e.IsCenterCloud(curCellIdx))
                        {
                            foreach (var item in _e.IdxsCellsAround(curCellIdx))
                            {
                                _needActive[item] = true;
                                _needColor[item] = ColorsValues.Color(SupportCellVisionTypes.Shift);
                            }
                        }
                    }
                }

                else if (_aboutGameC.AbilityType == AbilityTypes.FireArcher)
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
                    if (_e.UnitPlayerT(_e.SelectedCellIdx).Is(_aboutGameC.CurrentPlayerIType))
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
                var needActive = _needActive[cell_start];
                ref var wasActivated = ref _wasActivated[cell_start];

                if (needActive != wasActivated) _eVG.CellEs(cell_start).SupportCellEs.SupportSRC.GO.SetActive(needActive);
                if (needActive) _eVG.CellEs(cell_start).SupportCellEs.SupportSRC.Color = _needColor[cell_start];

                wasActivated = needActive;
            }
        }
    }
}