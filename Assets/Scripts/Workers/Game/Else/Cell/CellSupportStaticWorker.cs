using System;
using UnityEngine;

namespace Assets.Scripts.Workers.Cell
{
    internal class CellSupportStaticWorker : MainGeneralWorker
    {
        internal static void ActiveVision(bool isActive, SupportStaticTypes supportStaticType, int[] xy)
        {
            switch (supportStaticType)
            {
                case SupportStaticTypes.None:
                    throw new Exception();

                case SupportStaticTypes.Fertilizer:
                    EGGM.CellFertilizeSupStatEnt_SprRendCom(xy).SpriteRenderer.enabled = isActive;
                    break;

                case SupportStaticTypes.Wood:
                    EGGM.CellWoodSupStatEnt_SprRendCom(xy).SpriteRenderer.enabled = isActive;
                    break;

                case SupportStaticTypes.Ore:
                    EGGM.CellOreSupStatEnt_SprRendCom(xy).SpriteRenderer.enabled = isActive;
                    break;

                case SupportStaticTypes.Hp:
                    EGGM.CellHpSupStatEnt_SpriteRendererCom(xy).SpriteRenderer.enabled = isActive;
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
                    EGGM.CellFertilizeSupStatEnt_SprRendCom(xy).SpriteRenderer.color = color;
                    break;

                case SupportStaticTypes.Wood:
                    EGGM.CellWoodSupStatEnt_SprRendCom(xy).SpriteRenderer.color = color;
                    break;

                case SupportStaticTypes.Ore:
                    EGGM.CellOreSupStatEnt_SprRendCom(xy).SpriteRenderer.color = color;
                    break;

                case SupportStaticTypes.Hp:
                    EGGM.CellHpSupStatEnt_SpriteRendererCom(xy).SpriteRenderer.color = color;
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
                    EGGM.CellFertilizeSupStatEnt_SprRendCom(xy).SpriteRenderer.transform.localScale = vector;
                    break;

                case SupportStaticTypes.Wood:
                    EGGM.CellWoodSupStatEnt_SprRendCom(xy).SpriteRenderer.transform.localScale = vector;
                    break;

                case SupportStaticTypes.Ore:
                    EGGM.CellOreSupStatEnt_SprRendCom(xy).SpriteRenderer.transform.localScale = vector;
                    break;

                case SupportStaticTypes.Hp:
                    EGGM.CellHpSupStatEnt_SpriteRendererCom(xy).SpriteRenderer.transform.localScale = vector;
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}
