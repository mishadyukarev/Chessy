using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using UnityEngine;

namespace Assets.Scripts
{
    public class CellGameWorker
    {
        private static CellEntsContainer _cellContainer;

        internal static int Xamount => _cellContainer.Xamount;
        internal static int Yamount => _cellContainer.Yamount;


        internal CellGameWorker(CellEntsContainer cellContainer)
        {
            _cellContainer = cellContainer;
        }

        private static GameObject GetCellGO(int[] xy) => _cellContainer.CellEnt_CellGOCom(xy).CellGO;
        internal static int GetInstanceIDCell(int[] xy) => GetCellGO(xy).GetInstanceID();
        internal static bool IsActiveSelfParentCell(int[] xy) => GetCellGO(xy).transform.parent.gameObject.activeSelf;

        internal static float GetEulerAngle(XyzTypes xyzType, int[] xy)
        {
            switch (xyzType)
            {
                case XyzTypes.None:
                    throw new System.Exception();

                case XyzTypes.X:
                    return GetCellGO(xy).transform.rotation.eulerAngles.x;

                case XyzTypes.Y:
                    return GetCellGO(xy).transform.rotation.eulerAngles.y;

                case XyzTypes.Z:
                    return GetCellGO(xy).transform.rotation.eulerAngles.z;

                default:
                    throw new System.Exception();
            }
        }
    }
}