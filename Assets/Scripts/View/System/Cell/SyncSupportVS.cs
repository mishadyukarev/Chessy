using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
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


                switch (aboutGameC.CellClickType)
                {
                    case CellClickTypes.UniqueAbility:

                        switch (aboutGameC.AbilityType)
                        {
                            case AbilityTypes.ChangeDirectionWind:
                                if (fireCs[cell_start].HaveFire)
                                {
                                    _needActive[cell_start] = true;
                                    _needColor[cell_start] = ColorsValues.Color(aboutGameC.AbilityType);
                                }
                                break;
                        }
                        break;

                    default:
                        break;
                }



            }

            _needActive[indexesCellsC.Selected] = true;
            _needColor[indexesCellsC.Selected] = ColorsValues.Color(SupportCellVisionTypes.Selector);


            if (aboutGameC.CellClickType == CellClickTypes.UniqueAbility)
            {
                if (aboutGameC.AbilityType == AbilityTypes.ChangeDirectionWind)
                {
                    for (byte curCellIdx = 0; curCellIdx < IndexCellsValues.CELLS; curCellIdx++)
                    {
                        if (cloudCs[curCellIdx].IsCenterP)
                        {
                            foreach (var item in idxsAroundCellCs[curCellIdx].IdxCellsAroundArray)
                            {
                                _needActive[item] = true;
                                _needColor[item] = ColorsValues.Color(SupportCellVisionTypes.Shift);
                            }
                        }
                    }
                }

                else if (aboutGameC.AbilityType == AbilityTypes.FireArcher)
                {
                    for (byte idxCell = 0; idxCell < IndexCellsValues.CELLS; idxCell++)
                    {
                        if (_whereUnitCanFireAdultForestCs[indexesCellsC.Selected].Can(idxCell))
                        {
                            _needActive[idxCell] = true;
                            _needColor[idxCell] = ColorsValues.Color(AbilityTypes.FireArcher);
                        }
                    }
                }
            }


            else
            {
                if (unitCs[indexesCellsC.Selected].HaveUnit)
                {
                    if (unitCs[indexesCellsC.Selected].PlayerType == aboutGameC.CurrentPlayerIType)
                    {
                        if (aboutGameC.CellClickType != CellClickTypes.GiveTakeTW)
                        {
                            for (byte idxCell = 0; idxCell < IndexCellsValues.CELLS; idxCell++)
                            {
                                if (WhereUnitCanShiftC(indexesCellsC.Selected).CanShiftHere(idxCell))
                                {
                                    _needActive[idxCell] = true;
                                    _needColor[idxCell] = ColorsValues.Color(SupportCellVisionTypes.Shift);
                                }

                                if (_whereSimpleAttackCs[indexesCellsC.Selected].Can(idxCell))
                                {
                                    _needActive[idxCell] = true;
                                    _needColor[idxCell] = ColorsValues.Color(SupportCellVisionTypes.SimpleAttack);
                                }

                                if (_whereUniqueAttackCs[indexesCellsC.Selected].Can(idxCell))
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