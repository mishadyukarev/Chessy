﻿using Chessy.Model.Entity;
using Chessy.View.Component;
using UnityEditor;
using UnityEngine;
using Chessy.Model.Values;

namespace Chessy.View.System
{

    sealed class CircularAttackKingVS : SystemViewAbstract
    {
        readonly Vector3[] _possitions = new Vector3[IndexCellsValues.CELLS];
        readonly AnimationVC[] _animationVCs;

        public CircularAttackKingVS(in AnimationVC[] animationVCs, in EntitiesModel eM) : base(eM)
        {
            _animationVCs = animationVCs;
        }

        internal override void Sync()
        {
            //for (byte currentCellIdx = 0; currentCellIdx < IndexCellsValues.CELLS; currentCellIdx++)
            //{
            //    _possitions[currentCellIdx] = default;

            //    if (_e.SkinInfoUnitC(currentCellIdx).HaveData)
            //    {
            //        var dataIdxCell = _e.SkinInfoUnitC(currentCellIdx).DataIdxCell;

            //        _possitions[currentCellIdx] = _e.UnitMainC(currentCellIdx).Possition;
            //    }
            //}

            //for (byte currentCellIdx = 0; currentCellIdx < IndexCellsValues.CELLS; currentCellIdx++)
            //{
            //    _animationVCs[currentCellIdx].Animation.transform.position = _possitions[currentCellIdx];
            //}
        }
    }
}