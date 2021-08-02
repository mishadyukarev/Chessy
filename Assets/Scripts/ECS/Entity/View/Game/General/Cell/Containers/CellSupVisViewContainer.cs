using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using System;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;
using static Assets.Scripts.Abstractions.ValuesConsts.Colors;

namespace Assets.Scripts.Workers.Cell
{
    public struct CellSupVisViewContainer
    {
        private static EcsEntity[,] _cellSupportVisionEnts;
        internal ref SpriteRendererComponent CellSupVisEnt_SpriteRenderer(int[] xy) => ref _cellSupportVisionEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        internal CellSupVisViewContainer(GameObject[,] cellParentGOs, EcsWorld gameWorld)
        {
            _cellSupportVisionEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

            for (int x = 0; x < CELL_COUNT_X; x++)
                for (int y = 0; y < CELL_COUNT_Y; y++)
                {
                    var parentGO = cellParentGOs[x, y].transform.Find("SupportVision").gameObject;

                    var sr = parentGO.GetComponent<SpriteRenderer>();
                    _cellSupportVisionEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));
                }
        }

        internal static SpriteRenderer GetSR(int[] xy) => _cellSupportVisionEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer;

        internal static void ActiveSupVis(bool isEnabled, int[] xy) => GetSR(xy).enabled = isEnabled;

        internal static void SetColor(SupportVisionTypes supportVisionType, int[] xy)
        {
            switch (supportVisionType)
            {
                case SupportVisionTypes.None:
                    throw new Exception();

                case SupportVisionTypes.Selector:
                    GetSR(xy).color = SelectorColor;
                    break;

                case SupportVisionTypes.Spawn:
                    GetSR(xy).color = SpawnColor;
                    break;

                case SupportVisionTypes.Shift:
                    GetSR(xy).color = ShiftColor;
                    break;

                case SupportVisionTypes.SimpleAttack:
                    GetSR(xy).color = SimpleAttackColor;
                    break;

                case SupportVisionTypes.UniqueAttack:
                    GetSR(xy).color = UniqueAttackColor;
                    break;

                case SupportVisionTypes.Upgrade:
                    GetSR(xy).color = UpgradeColor;
                    break;

                case SupportVisionTypes.FireSelector:
                    GetSR(xy).color = FireSelectorColor;
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
