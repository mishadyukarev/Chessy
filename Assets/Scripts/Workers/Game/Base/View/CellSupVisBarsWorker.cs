using Assets.Scripts.ECS.Game.General.Entities.Containers;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers.Cell
{
    internal class CellSupVisBarsWorker
    {
        private static CellBarsViewContainerEnts _cellSupVisBarsContainer;

        internal CellSupVisBarsWorker(CellBarsViewContainerEnts cellSupVisBarsContainer)
        {
            _cellSupVisBarsContainer = cellSupVisBarsContainer;
        }


        internal static void ActiveVision(bool isActive, SupportStaticTypes supportStaticType, int[] xy)
        {
            switch (supportStaticType)
            {
                case SupportStaticTypes.None:
                    throw new Exception();

                case SupportStaticTypes.Fertilizer:
                    _cellSupVisBarsContainer.CellFertilizeSupStatEnt_SprRendCom(xy).SpriteRenderer.enabled = isActive;
                    break;

                case SupportStaticTypes.Wood:
                    _cellSupVisBarsContainer.CellWoodSupStatEnt_SprRendCom(xy).SpriteRenderer.enabled = isActive;
                    break;

                case SupportStaticTypes.Ore:
                    _cellSupVisBarsContainer.CellOreSupStatEnt_SprRendCom(xy).SpriteRenderer.enabled = isActive;
                    break;

                case SupportStaticTypes.Hp:
                    _cellSupVisBarsContainer.CellHpSupStatEnt_SpriteRendererCom(xy).SpriteRenderer.enabled = isActive;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void SetColor(SupportStaticTypes supportStaticType, Color color, int[] xy)
        {
            switch (supportStaticType)
            {
                case SupportStaticTypes.None:
                    throw new Exception();

                case SupportStaticTypes.Fertilizer:
                    _cellSupVisBarsContainer.CellFertilizeSupStatEnt_SprRendCom(xy).SpriteRenderer.color = color;
                    break;

                case SupportStaticTypes.Wood:
                    _cellSupVisBarsContainer.CellWoodSupStatEnt_SprRendCom(xy).SpriteRenderer.color = color;
                    break;

                case SupportStaticTypes.Ore:
                    _cellSupVisBarsContainer.CellOreSupStatEnt_SprRendCom(xy).SpriteRenderer.color = color;
                    break;

                case SupportStaticTypes.Hp:
                    _cellSupVisBarsContainer.CellHpSupStatEnt_SpriteRendererCom(xy).SpriteRenderer.color = color;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void SetScale(SupportStaticTypes supportStaticType, Vector3 vector, int[] xy)
        {
            switch (supportStaticType)
            {
                case SupportStaticTypes.None:
                    throw new Exception();

                case SupportStaticTypes.Fertilizer:
                    _cellSupVisBarsContainer.CellFertilizeSupStatEnt_SprRendCom(xy).SpriteRenderer.transform.localScale = vector;
                    break;

                case SupportStaticTypes.Wood:
                    _cellSupVisBarsContainer.CellWoodSupStatEnt_SprRendCom(xy).SpriteRenderer.transform.localScale = vector;
                    break;

                case SupportStaticTypes.Ore:
                    _cellSupVisBarsContainer.CellOreSupStatEnt_SprRendCom(xy).SpriteRenderer.transform.localScale = vector;
                    break;

                case SupportStaticTypes.Hp:
                    _cellSupVisBarsContainer.CellHpSupStatEnt_SpriteRendererCom(xy).SpriteRenderer.transform.localScale = vector;
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}
