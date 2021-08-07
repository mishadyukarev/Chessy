using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Leopotam.Ecs;
using System;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts.ECS.System.View.Game.General.Cell
{
    internal sealed class CellViewSystem : IEcsInitSystem
    {
        private EcsWorld _gameWorld;

        private static EcsEntity[,] _cellEnts;

        public void Init()
        {
            _cellEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

            for (int x = 0; x < CELL_COUNT_X; x++)
                for (int y = 0; y < CELL_COUNT_Y; y++)
                {
                    var parentGO = MainGameSystem.CellGOs[x, y].transform.Find("Cell").gameObject;

                    _cellEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new CellGOComponent(parentGO));
                }
        }

        private static GameObject GetCellGO(int[] xy) => _cellEnts[xy[X], xy[Y]].Get<CellGOComponent>().CellGO;
        private static GameObject GetCellGO(byte[] xy) => _cellEnts[xy[X], xy[Y]].Get<CellGOComponent>().CellGO;

        internal static int GetInstanceIDCell(byte[] xy) => GetCellGO(xy).GetInstanceID();
        internal static bool IsActiveSelfParentCell(int[] xy) => GetCellGO(xy).transform.parent.gameObject.activeSelf;

        internal static float GetEulerAngle(XyzTypes xyzType, int[] xy)
        {
            switch (xyzType)
            {
                case XyzTypes.None:
                    throw new Exception();

                case XyzTypes.X:
                    return GetCellGO(xy).transform.rotation.eulerAngles.x;

                case XyzTypes.Y:
                    return GetCellGO(xy).transform.rotation.eulerAngles.y;

                case XyzTypes.Z:
                    return GetCellGO(xy).transform.rotation.eulerAngles.z;

                default:
                    throw new Exception();
            }
        }
    }
}
