﻿using Assets.Scripts.Abstractions.ValuesConsts;
using System;
using static Assets.Scripts.Main;
using static Assets.Scripts.Abstractions.ValuesConsts.Colors;

namespace Assets.Scripts.Static.Cell
{
    public static class CellSupportVisionWorker
    {
        private static EntitiesGameGeneralManager EGGM => Instance.EntGGM;

        internal static void EnableSupVis(SupportVisionTypes supportVisionType, int[] xy)
        {
            EGGM.CellSupVisEnt_SpriteRenderer(xy).Enabled = true;

            switch (supportVisionType)
            {
                case SupportVisionTypes.None:
                    throw new Exception();

                case SupportVisionTypes.Selector:
                    EGGM.CellSupVisEnt_SpriteRenderer(xy).Color = Selector_Color;
                    break;

                case SupportVisionTypes.Spawn:
                    EGGM.CellSupVisEnt_SpriteRenderer(xy).Color = UniqueAttack_Color;
                    break;

                case SupportVisionTypes.Shift:
                    EGGM.CellSupVisEnt_SpriteRenderer(xy).Color = UniqueAttack_Color;
                    break;

                case SupportVisionTypes.SimpleAttack:
                    EGGM.CellSupVisEnt_SpriteRenderer(xy).Color = UniqueAttack_Color;
                    break;

                case SupportVisionTypes.UniqueAttack:
                    EGGM.CellSupVisEnt_SpriteRenderer(xy).Color = UniqueAttack_Color;
                    break;

                case SupportVisionTypes.Upgrade:
                    EGGM.CellSupVisEnt_SpriteRenderer(xy).Color = UniqueAttack_Color;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void DisableSupVis(int[] xy) => EGGM.CellSupVisEnt_SpriteRenderer(xy).Enabled = false;
    }
}
