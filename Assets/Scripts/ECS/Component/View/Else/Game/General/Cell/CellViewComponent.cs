using Assets.Scripts.Abstractions.Enums;
using System;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.View.Else.Game.General.Cell
{
    internal struct CellViewComponent
    {
        private GameObject _cell_GO;

        internal CellViewComponent(GameObject cell_GO)
        {
            _cell_GO = cell_GO.transform.Find("Cell").gameObject;
        }

        internal int GetInstanceIDCell() => _cell_GO.GetInstanceID();
        internal bool IsActiveSelfParentCell() => _cell_GO.transform.parent.gameObject.activeSelf;

        internal float GetEulerAngle(XyzTypes xyzType, int[] xy)
        {
            switch (xyzType)
            {
                case XyzTypes.None:
                    throw new Exception();

                case XyzTypes.X:
                    return _cell_GO.transform.rotation.eulerAngles.x;

                case XyzTypes.Y:
                    return _cell_GO.transform.rotation.eulerAngles.y;

                case XyzTypes.Z:
                    return _cell_GO.transform.rotation.eulerAngles.z;

                default:
                    throw new Exception();
            }
        }
    }
}
