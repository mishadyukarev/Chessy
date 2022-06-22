using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.View.System;
using System.Collections.Generic;

namespace Chessy.Game
{
    sealed class SyncSunSideVS : SystemViewGameAbs
    {
        readonly bool[] _needActive;
        readonly EntitiesViewGame _eVG;

        internal SyncSunSideVS(in EntitiesViewGame eVG, in EntitiesModelGame eMG) : base(eMG)
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