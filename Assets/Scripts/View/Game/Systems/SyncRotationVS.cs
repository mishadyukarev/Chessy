using Chessy.Common;
using Chessy.Common.Entity.View;
using Chessy.Game.Entity;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using System;
using UnityEngine;

namespace Chessy.Game
{
    sealed class SyncRotationVS : SystemViewCellGameAbs
    {
        readonly CellVEs _cellVEs;

        internal SyncRotationVS(in CellVEs cellVEs, in byte currentCell, in EntitiesModelGame eMG) : base(currentCell, eMG)
        {
            _cellVEs = cellVEs;
        }

        internal sealed override void Sync()
        {
            var curPlayer = e.CurPlayerIT;

            if (curPlayer == PlayerTypes.None) throw new Exception();
            _cellVEs.CellSR.ParentTransform.rotation = curPlayer == PlayerTypes.First
                ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);

            switch (curPlayer)
            {
                case PlayerTypes.First:
                    _cellVEs.TrailCellVC(DirectTypes.Up).ParentTransform.localEulerAngles = new Vector3(0, 0, 0);
                    break;

                case PlayerTypes.Second:
                    _cellVEs.TrailCellVC(DirectTypes.Up).ParentTransform.localEulerAngles = new Vector3(0, 0, 180);
                    break;

                default: throw new Exception();
            }
        }
    }
}