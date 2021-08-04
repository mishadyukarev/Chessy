using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using System;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;
using static Assets.Scripts.Abstractions.ValuesConsts.Colors;

namespace Assets.Scripts.ECS.System.View.Game.General.Cell
{
    internal sealed class CellSupViewSystem : IEcsInitSystem
    {
        private EcsWorld gameWorld;

        private static EcsEntity[,] _cellSupportVisionEnts;

        internal ref SpriteRendererComponent CellSupVisEnt_SpriteRenderer(int[] xy) => ref _cellSupportVisionEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        public void Init()
        {
            _cellSupportVisionEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

            for (int x = 0; x < CELL_COUNT_X; x++)
                for (int y = 0; y < CELL_COUNT_Y; y++)
                {
                    var parentGO = StartSpawnCellsViewSystem.CellGOs[x, y].transform.Find("SupportVision").gameObject;

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
