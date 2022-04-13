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


                switch (e.CellClickTC.CellClickT)
                {
                    case CellClickTypes.UniqueAbility:

                        switch (e.SelectedE.AbilityTC.Ability)
                        {
                            case AbilityTypes.ChangeDirectionWind:
                                if (e.HaveFire(cell_start))
                                {
                                    _needActive[cell_start] = true;
                                    _needColor[cell_start] = ColorsValues.Color(e.SelectedE.AbilityTC.Ability);
                                }
                                break;
                        }
                        break;

                    default:
                        break;
                }



            }

            _needActive[e.SelectedCell] = true;
            _needColor[e.SelectedCell] = ColorsValues.Color(SupportCellVisionTypes.Selector);


            if (e.CellClickTC.Is(CellClickTypes.UniqueAbility))
            {
                if (e.SelectedE.AbilityTC.Is(AbilityTypes.ChangeDirectionWind))
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var cell_target = e.AroundCellsE(e.WeatherE.CloudC.Center).IdxCell(dirT);

                        _needActive[cell_target] = true;
                        _needColor[cell_target] = ColorsValues.Color(SupportCellVisionTypes.Shift);
                    }
                }

                else if (e.SelectedE.AbilityTC.Is(AbilityTypes.FireArcher))
                {
                    foreach (var idx in e.UnitForArsonC(e.CellsC.Selected).Idxs)
                    {
                        _needActive[idx] = true;
                        _needColor[idx] = ColorsValues.Color(AbilityTypes.FireArcher);
                    }
                }
            }


            else
            {
                if (e.UnitTC(e.CellsC.Selected).HaveUnit)
                {
                    if (e.UnitPlayerTC(e.CellsC.Selected).Is(e.CurPlayerITC.PlayerT))
                    {
                        var idxs = e.CellsForShift(e.CellsC.Selected).Idxs;

                        if (!e.CellClickTC.Is(CellClickTypes.GiveTakeTW))
                        {
                            foreach (var idx_0 in idxs)
                            {
                                _needActive[idx_0] = true;
                                _needColor[idx_0] = ColorsValues.Color(SupportCellVisionTypes.Shift);
                            }

                            foreach (var idx_0 in e.AttackSimpleCellsC(e.CellsC.Selected).Idxs)
                            {
                                _needActive[idx_0] = true;
                                _needColor[idx_0] = ColorsValues.Color(SupportCellVisionTypes.SimpleAttack);
                            }
                            foreach (var idx_0 in e.AttackUniqueCellsC(e.CellsC.Selected).Idxs)
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