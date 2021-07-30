using Assets.Scripts.ECS.Game.General.Entities.Containers;
using System;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.Colors;

namespace Assets.Scripts.Workers.Cell
{
    public class CellSupVisViewWorker
    {
        private static CellSupVisViewContainerEnts _cellSupVisEntsContainer;

        internal CellSupVisViewWorker(CellSupVisViewContainerEnts cellSupVisEntsContainer)
        {
            _cellSupVisEntsContainer = cellSupVisEntsContainer;
        }

        internal static SpriteRenderer GetSR(int[] xy) => _cellSupVisEntsContainer.CellSupVisEnt_SpriteRenderer(xy).SpriteRenderer;

        internal static void ActiveSupVis(bool isEnabled, int[] xy) => GetSR(xy).enabled = isEnabled;

        internal static void SetColor(SupportVisionTypes supportVisionType, int[] xy)
        {
            switch (supportVisionType)
            {
                case SupportVisionTypes.None:
                    throw new Exception();

                case SupportVisionTypes.Selector:
                    GetSR(xy).color = Selector_Color;
                    break;

                case SupportVisionTypes.Spawn:
                    GetSR(xy).color = UniqueAttack_Color;
                    break;

                case SupportVisionTypes.Shift:
                    GetSR(xy).color = UniqueAttack_Color;
                    break;

                case SupportVisionTypes.SimpleAttack:
                    GetSR(xy).color = UniqueAttack_Color;
                    break;

                case SupportVisionTypes.UniqueAttack:
                    GetSR(xy).color = UniqueAttack_Color;
                    break;

                case SupportVisionTypes.Upgrade:
                    GetSR(xy).color = UniqueAttack_Color;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void EnableSupVis(SupportVisionTypes supportVisionType, int[] xy)
        {
            ActiveSupVis(true, xy);
            SetColor(supportVisionType, xy);
        }
        internal static void DisableSupVis(int[] xy) => GetSR(xy).enabled = false;
    }
}
