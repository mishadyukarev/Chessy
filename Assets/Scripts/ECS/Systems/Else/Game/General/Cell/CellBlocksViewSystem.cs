using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Leopotam.Ecs;
using System;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts.ECS.System.View.Game.General.Cell
{
    internal sealed class CellBlocksViewSystem : IEcsInitSystem
    {
        private EcsWorld _gameWorld;

        private static EcsEntity[,] _cellProtectRelaxEnts;
        private static EcsEntity[,] _cellMaxStepsEnts;


        public void Init()
        {
            _cellProtectRelaxEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
            _cellMaxStepsEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

            for (int x = 0; x < CELL_COUNT_X; x++)
                for (int y = 0; y < CELL_COUNT_Y; y++)
                {

                }
        }

        internal static void EnableCellSupVisBlocksSR(bool isEnabled, CellSupVisBlocksTypes cellSupVisBlocksType, int[] xy)
        {
            switch (cellSupVisBlocksType)
            {
                case CellSupVisBlocksTypes.None:
                    throw new Exception();

                case CellSupVisBlocksTypes.Condition:
                    _cellProtectRelaxEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer.enabled = isEnabled;
                    break;

                case CellSupVisBlocksTypes.MaxSteps:
                    _cellMaxStepsEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer.enabled = isEnabled;
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void SetCellSupVisBlocksColor(Color color, CellSupVisBlocksTypes cellSupVisBlocksType, int[] xy)
        {
            switch (cellSupVisBlocksType)
            {
                case CellSupVisBlocksTypes.None:
                    throw new Exception();

                case CellSupVisBlocksTypes.Condition:
                    _cellProtectRelaxEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer.color = color;
                    break;

                case CellSupVisBlocksTypes.MaxSteps:
                    _cellMaxStepsEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer.color = color;
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}
