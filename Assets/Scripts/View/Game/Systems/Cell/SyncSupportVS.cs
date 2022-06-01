using Chessy.Game.Entity;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.View.System;
using UnityEngine;

namespace Chessy.Game
{
    sealed class SyncSupportVS : SystemViewGameAbs
    {
        readonly static bool[] _needActive = new bool[StartValues.CELLS];
        readonly static Color[] _needColor = new Color[StartValues.CELLS];

        readonly EntitiesViewGame _eVG;

        internal SyncSupportVS(in EntitiesViewGame eVG, in EntitiesModelGame eMG) : base(eMG)
        {
            _eVG = eVG;
        }

        internal sealed override void Sync()
        {
            for (byte cell_start = 0; cell_start < StartValues.CELLS; cell_start++)
            {
                _needActive[cell_start] = false;


                switch (_e.CellClickTC.CellClickT)
                {
                    case CellClickTypes.UniqueAbility:

                        switch (_e.SelectedE.AbilityTC.Ability)
                        {
                            case AbilityTypes.ChangeDirectionWind:
                                if (_e.HaveFire(cell_start))
                                {
                                    _needActive[cell_start] = true;
                                    _needColor[cell_start] = ColorsValues.Color(_e.SelectedE.AbilityTC.Ability);
                                }
                                break;
                        }
                        break;

                    default:
                        break;
                }



            }

            _needActive[_e.SelectedCell] = true;
            _needColor[_e.SelectedCell] = ColorsValues.Color(SupportCellVisionTypes.Selector);


            if (_e.CellClickTC.Is(CellClickTypes.UniqueAbility))
            {
                if (_e.SelectedE.AbilityTC.Is(AbilityTypes.ChangeDirectionWind))
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var cell_target = _e.AroundCellsE(_e.WeatherE.CloudC.Center).IdxCell(dirT);

                        _needActive[cell_target] = true;
                        _needColor[cell_target] = ColorsValues.Color(SupportCellVisionTypes.Shift);
                    }
                }

                else if (_e.SelectedE.AbilityTC.Is(AbilityTypes.FireArcher))
                {
                    foreach (var idx in _e.UnitForArsonC(_e.CellsC.Selected).Idxs)
                    {
                        _needActive[idx] = true;
                        _needColor[idx] = ColorsValues.Color(AbilityTypes.FireArcher);
                    }
                }
            }


            else
            {
                if (_e.UnitTC(_e.CellsC.Selected).HaveUnit)
                {
                    if (_e.UnitPlayerTC(_e.CellsC.Selected).Is(_e.CurPlayerITC.PlayerT))
                    {
                        var idxs = _e.CellsForShift(_e.CellsC.Selected).Idxs;

                        if (!_e.CellClickTC.Is(CellClickTypes.GiveTakeTW))
                        {
                            foreach (var idx_0 in idxs)
                            {
                                _needActive[idx_0] = true;
                                _needColor[idx_0] = ColorsValues.Color(SupportCellVisionTypes.Shift);
                            }

                            foreach (var idx_0 in _e.AttackSimpleCellsC(_e.CellsC.Selected).Idxs)
                            {
                                _needActive[idx_0] = true;
                                _needColor[idx_0] = ColorsValues.Color(SupportCellVisionTypes.SimpleAttack);
                            }
                            foreach (var idx_0 in _e.AttackUniqueCellsC(_e.CellsC.Selected).Idxs)
                            {
                                _needActive[idx_0] = true;
                                _needColor[idx_0] = ColorsValues.Color(SupportCellVisionTypes.UniqueAttack);
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
        } }
}