using Assets.Scripts.Abstractions.ValuesConsts;
using System;
using static Assets.Scripts.Main;
using static Assets.Scripts.Abstractions.ValuesConsts.Colors;

namespace Assets.Scripts.Workers.Cell
{
    public class CellSupportVisionWorker : MainGeneralWorker
    {
        internal static void EnableSupVis(SupportVisionTypes supportVisionType, int[] xy)
        {
            EGGM.CellSupVisEnt_SpriteRenderer(xy).SpriteRenderer.enabled = true;

            switch (supportVisionType)
            {
                case SupportVisionTypes.None:
                    throw new Exception();

                case SupportVisionTypes.Selector:
                    EGGM.CellSupVisEnt_SpriteRenderer(xy).SpriteRenderer.color = Selector_Color;
                    break;

                case SupportVisionTypes.Spawn:
                    EGGM.CellSupVisEnt_SpriteRenderer(xy).SpriteRenderer.color = UniqueAttack_Color;
                    break;

                case SupportVisionTypes.Shift:
                    EGGM.CellSupVisEnt_SpriteRenderer(xy).SpriteRenderer.color = UniqueAttack_Color;
                    break;

                case SupportVisionTypes.SimpleAttack:
                    EGGM.CellSupVisEnt_SpriteRenderer(xy).SpriteRenderer.color = UniqueAttack_Color;
                    break;

                case SupportVisionTypes.UniqueAttack:
                    EGGM.CellSupVisEnt_SpriteRenderer(xy).SpriteRenderer.color = UniqueAttack_Color;
                    break;

                case SupportVisionTypes.Upgrade:
                    EGGM.CellSupVisEnt_SpriteRenderer(xy).SpriteRenderer.color = UniqueAttack_Color;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void DisableSupVis(int[] xy) => EGGM.CellSupVisEnt_SpriteRenderer(xy).SpriteRenderer.enabled = false;
    }
}
