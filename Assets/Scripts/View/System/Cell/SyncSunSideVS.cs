using Chessy.Model.Model.Entity;
using Chessy.Model.Values;
using Chessy.Model.View.System;
using System.Collections.Generic;

namespace Chessy.Model
{
    sealed class SyncSunSideVS : SystemViewAbstract
    {
        readonly bool[] _needActive;
        readonly EntitiesView _eVG;

        internal SyncSunSideVS(in EntitiesView eVG, in EntitiesModel eMG) : base(eMG)
        {
            _needActive = new bool[StartValues.CELLS];
            _eVG = eVG;
        }

        internal override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _needActive[cellIdxCurrent] = false;
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.SelectedCellIdx == cellIdxCurrent && _e.WeatherE.SunSideT.IsAcitveSun())
                {
                    var simpleUnqiueCells = new HashSet<byte>();
                    foreach (var item in _e.AttackSimpleCellsC(cellIdxCurrent).Idxs)
                    {
                        simpleUnqiueCells.Add(item);
                    }
                    foreach (var item in _e.AttackUniqueCellsC(cellIdxCurrent).Idxs)
                    {
                        simpleUnqiueCells.Add(item);
                    }



                    foreach (var cellIdxAttack in simpleUnqiueCells)
                    {
                        foreach (var sunDirectT in _e.WeatherE.SunSideT.RaysSun())
                        {
                            var invertSunDirectT = sunDirectT.Invert();

                            if (_e.AroundCellsE(cellIdxCurrent).Direct(cellIdxAttack) == invertSunDirectT)
                            {
                                _needActive[cellIdxAttack] = true;
                            }
                        }
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _eVG.CellEs(cellIdxCurrent).SunSideSRC.SetActive(_needActive[cellIdxCurrent]);
            }

        }
    }
}