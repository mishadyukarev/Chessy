using Assets.Scripts.ECS.Game.General.Entities.Containers;
using System;
using static Assets.Scripts.Abstractions.ValuesConsts.Colors;

namespace Assets.Scripts.Workers.Cell
{
    public class CellSupVisWorker
    {
        private static CellSupVisEntsContainer _cellSupVisEntsContainer;

        internal CellSupVisWorker(CellSupVisEntsContainer cellSupVisEntsContainer)
        {
            _cellSupVisEntsContainer = cellSupVisEntsContainer;
        }


        internal static void EnableSupVis(SupportVisionTypes supportVisionType, int[] xy)
        {
            _cellSupVisEntsContainer.CellSupVisEnt_SpriteRenderer(xy).SpriteRenderer.enabled = true;

            switch (supportVisionType)
            {
                case SupportVisionTypes.None:
                    throw new Exception();

                case SupportVisionTypes.Selector:
                    _cellSupVisEntsContainer.CellSupVisEnt_SpriteRenderer(xy).SpriteRenderer.color = Selector_Color;
                    break;

                case SupportVisionTypes.Spawn:
                    _cellSupVisEntsContainer.CellSupVisEnt_SpriteRenderer(xy).SpriteRenderer.color = UniqueAttack_Color;
                    break;

                case SupportVisionTypes.Shift:
                    _cellSupVisEntsContainer.CellSupVisEnt_SpriteRenderer(xy).SpriteRenderer.color = UniqueAttack_Color;
                    break;

                case SupportVisionTypes.SimpleAttack:
                    _cellSupVisEntsContainer.CellSupVisEnt_SpriteRenderer(xy).SpriteRenderer.color = UniqueAttack_Color;
                    break;

                case SupportVisionTypes.UniqueAttack:
                    _cellSupVisEntsContainer.CellSupVisEnt_SpriteRenderer(xy).SpriteRenderer.color = UniqueAttack_Color;
                    break;

                case SupportVisionTypes.Upgrade:
                    _cellSupVisEntsContainer.CellSupVisEnt_SpriteRenderer(xy).SpriteRenderer.color = UniqueAttack_Color;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void DisableSupVis(int[] xy) => _cellSupVisEntsContainer.CellSupVisEnt_SpriteRenderer(xy).SpriteRenderer.enabled = false;
    }
}
