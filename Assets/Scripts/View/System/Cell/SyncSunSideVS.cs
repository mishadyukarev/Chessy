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
                if (_e.SelectedCellIdx == cellIdxCurrent && _e.WeatherE.SunC.SunSideT.IsAcitveSun())
                {
                    var simpleUnqiueCells = new HashSet<byte>();

                    for (byte cellIdx = 0; cellIdx < StartValues.CELLS; cellIdx++)
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
                        foreach (var sunDirectT in _e.WeatherE.SunC.SunSideT.RaysSun())
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