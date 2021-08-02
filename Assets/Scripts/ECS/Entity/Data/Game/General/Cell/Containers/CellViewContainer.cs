using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts
{
    public struct CellViewContainer
    {
        private static EcsEntity[,] _cellEnts;

        internal CellViewContainer(GameObject[,] cellParentGOs, EcsWorld gameWorld)
        {
            _cellEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

            for (int x = 0; x < CELL_COUNT_X; x++)
                for (int y = 0; y < CELL_COUNT_Y; y++)
                {
                    var parentGO = cellParentGOs[x, y].transform.Find("Cell").gameObject;

                    _cellEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new CellGOComponent(parentGO));
                }
        }

        private static GameObject GetCellGO(int[] xy) => _cellEnts[xy[X], xy[Y]].Get<CellGOComponent>().CellGO;

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