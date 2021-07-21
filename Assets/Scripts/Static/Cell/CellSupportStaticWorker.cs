using System;
using UnityEngine;

namespace Assets.Scripts.Static.Cell
{
    internal class CellSupportStaticWorker : MainWorker
    {
        internal static void ActiveVision(bool isActive, SupportStaticTypes supportStaticType, int[] xy)
        {
            switch (supportStaticType)
            {
                case SupportStaticTypes.None:
                    throw new Exception();

                case SupportStaticTypes.Fertilizer:
                    EGGM.CellFertilizeSupStatEnt_SprRendCom(xy).Enabled = isActive;
                    break;

                case SupportStaticTypes.Wood:
                    EGGM.CellWoodSupStatEnt_SprRendCom(xy).Enabled = isActive;
                    break;

                case SupportStaticTypes.Ore:
                    EGGM.CellOreSupStatEnt_SprRendCom(xy).Enabled = isActive;
                    break;

                case SupportStaticTypes.Hp:
                    EGGM.CellHpSupStatEnt_SpriteRendererCom(xy).Enabled = isActive;
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
                    EGGM.CellFertilizeSupStatEnt_SprRendCom(xy).Color = color;
                    break;

                case SupportStaticTypes.Wood:
                    EGGM.CellWoodSupStatEnt_SprRendCom(xy).Color = color;
                    break;

                case SupportStaticTypes.Ore:
                    EGGM.CellOreSupStatEnt_SprRendCom(xy).Color = color;
                    break;

                case SupportStaticTypes.Hp:
                    EGGM.CellHpSupStatEnt_SpriteRendererCom(xy).Color = color;
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
                    EGGM.CellFertilizeSupStatEnt_SprRendCom(xy).LocalScale = vector;
                    break;

                case SupportStaticTypes.Wood:
                    EGGM.CellWoodSupStatEnt_SprRendCom(xy).LocalScale = vector;
                    break;

                case SupportStaticTypes.Ore:
                    EGGM.CellOreSupStatEnt_SprRendCom(xy).LocalScale = vector;
                    break;

                case SupportStaticTypes.Hp:
                    EGGM.CellHpSupStatEnt_SpriteRendererCom(xy).LocalScale = vector;
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}
