using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.System;
using Chessy.View.UI.Entity;
using System.Collections.Generic;

namespace Chessy.View.System
{
    sealed class SyncSunSideVS : SystemViewAbstract
    {
        readonly bool[] _needActive;
        readonly EntitiesView _eVG;

        internal SyncSunSideVS(in EntitiesView eVG, in EntitiesModel eMG) : base(eMG)
        {
            _needActive = new bool[IndexCellsValues.CELLS];
            _eVG = eVG;
        }

        internal override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needActive[cellIdxCurrent] = false;
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.SelectedCellIdx == cellIdxCurrent && _e.SunSideT.IsAcitveSun())
                {
                    var simpleUnqiueCells = new HashSet<byte>();

                    for (byte cellIdx = 0; cellIdx < IndexCellsValues.CELLS; cellIdx++)
                    {
                        if (_e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxCurrent).Can(cellIdx))
                        {
                            simpleUnqiueCells.Add(cellIdx);
                        }
                        if (_e.WhereUnitCanAttackUniqueAttackToEnemyC(cellIdxCurrent).Can(cellIdx))
                        {
                            simpleUnqiueCells.Add(cellIdx);
                        }
                    }



                    foreach (var cellIdxAttack in simpleUnqiueCells)
                    {
                        foreach (var sunDirectT in _e.SunSideT.RaysSun())
                        {
                            var invertSunDirectT = sunDirectT.Invert();

                            if (_e.DirectionAround(cellIdxCurrent, cellIdxAttack) == invertSunDirectT)
                            {
                                _needActive[cellIdxAttack] = true;
                            }
                        }
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _eVG.CellEs(cellIdxCurrent).SunSideSRC.SetActiveGO(_needActive[cellIdxCurrent]);
            }

        }
    }
}