using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.UI.Entity;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncSunSideVS : SystemViewAbstract
    {
        readonly bool[] _needActive = new bool[IndexCellsValues.CELLS];
        readonly bool[] _wasActivated = new bool[IndexCellsValues.CELLS];
        readonly GameObject[] _gos = new GameObject[IndexCellsValues.CELLS];
        readonly HashSet<byte> _simpleUnqiueCells = new HashSet<byte>();

        internal SyncSunSideVS(in EntitiesView eV, in EntitiesModel eM) : base(eM)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _gos[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).SunSideSRC.GO;
            }
        }

        internal override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needActive[cellIdxCurrent] = false;
            }

            for (byte cellIdxCurrent_0 = 0; cellIdxCurrent_0 < IndexCellsValues.CELLS; cellIdxCurrent_0++)
            {
                if (_cellCs[cellIdxCurrent_0].IsBorder) continue;

                if (_cellsC.Selected == cellIdxCurrent_0 && _sunC.IsAcitveSun)
                {
                    _simpleUnqiueCells.Clear();

                    for (byte cellIdx = 0; cellIdx < IndexCellsValues.CELLS; cellIdx++)
                    {
                        if (_whereSimpleAttackCs[cellIdxCurrent_0].Can(cellIdx))
                        {
                            _simpleUnqiueCells.Add(cellIdx);
                        }
                        if (_whereUniqueAttackCs[cellIdxCurrent_0].Can(cellIdx))
                        {
                            _simpleUnqiueCells.Add(cellIdx);
                        }
                    }



                    foreach (var cellIdxAttack in _simpleUnqiueCells)
                    {
                        foreach (var sunDirectT in _sunC.RaysSun)
                        {
                            var invertSunDirectT = sunDirectT.Invert();

                            if (_e.CellAroundC(cellIdxCurrent_0, cellIdxAttack).DirectT == invertSunDirectT)
                            {
                                _needActive[cellIdxAttack] = true;
                            }
                        }
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                var needActive = _needActive[cellIdxCurrent];
                ref var wasActivated = ref _wasActivated[cellIdxCurrent];

                if (needActive != wasActivated) _gos[cellIdxCurrent].SetActive(needActive);

                wasActivated = needActive;
            }
        }
    }
}