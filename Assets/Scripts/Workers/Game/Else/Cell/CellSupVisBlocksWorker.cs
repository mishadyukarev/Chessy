using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers.Game.Else.Cell
{
    internal sealed class CellSupVisBlocksWorker
    {
        private static CellSupVisBlocksEntsContainer _cellSupVisBlocksContainer;

        internal CellSupVisBlocksWorker(CellSupVisBlocksEntsContainer cellSupVisBlocksContainer)
        {
            _cellSupVisBlocksContainer = cellSupVisBlocksContainer;
        }

        internal static void EnableCellSupVisBlocksSR(bool isEnabled, CellSupVisBlocksTypes cellSupVisBlocksType, int[] xy)
        {
            switch (cellSupVisBlocksType)
            {
                case CellSupVisBlocksTypes.None:
                    throw new Exception();

                case CellSupVisBlocksTypes.Condition:
                    _cellSupVisBlocksContainer.CellProtectRelaxEnt_SpriteRendererCom(xy).SpriteRenderer.enabled = isEnabled;
                    break;

                case CellSupVisBlocksTypes.MaxSteps:
                    _cellSupVisBlocksContainer.CellMaxStepsEnt_SpriteRendererCom(xy).SpriteRenderer.enabled = isEnabled;
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
                    _cellSupVisBlocksContainer.CellProtectRelaxEnt_SpriteRendererCom(xy).SpriteRenderer.color = color;
                    break;

                case CellSupVisBlocksTypes.MaxSteps:
                    _cellSupVisBlocksContainer.CellMaxStepsEnt_SpriteRendererCom(xy).SpriteRenderer.color = color;
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}
