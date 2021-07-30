using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers.Game.Else.Cell
{
    internal sealed class CellBlocksViewWorker
    {
        private static CellBlocksViewContainerEnts _cellBlocksViewContainerEnts;

        internal CellBlocksViewWorker(CellBlocksViewContainerEnts cellSupVisBlocksContainer)
        {
            _cellBlocksViewContainerEnts = cellSupVisBlocksContainer;
        }

        internal static void EnableCellSupVisBlocksSR(bool isEnabled, CellSupVisBlocksTypes cellSupVisBlocksType, int[] xy)
        {
            switch (cellSupVisBlocksType)
            {
                case CellSupVisBlocksTypes.None:
                    throw new Exception();

                case CellSupVisBlocksTypes.Condition:
                    _cellBlocksViewContainerEnts.CellProtectRelaxEnt_SpriteRendererCom(xy).SpriteRenderer.enabled = isEnabled;
                    break;

                case CellSupVisBlocksTypes.MaxSteps:
                    _cellBlocksViewContainerEnts.CellMaxStepsEnt_SpriteRendererCom(xy).SpriteRenderer.enabled = isEnabled;
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
                    _cellBlocksViewContainerEnts.CellProtectRelaxEnt_SpriteRendererCom(xy).SpriteRenderer.color = color;
                    break;

                case CellSupVisBlocksTypes.MaxSteps:
                    _cellBlocksViewContainerEnts.CellMaxStepsEnt_SpriteRendererCom(xy).SpriteRenderer.color = color;
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}
