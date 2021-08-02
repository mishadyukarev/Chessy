﻿using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts.Workers.Game.Else.Fire
{
    internal struct CellFireViewContainer
    {
        private static EcsEntity[,] _cellFireEnts;

        internal CellFireViewContainer(GameObject[,] cellParentGOs, EcsWorld gameWorld)
        {
            _cellFireEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

            for (int x = 0; x < CELL_COUNT_X; x++)
                for (int y = 0; y < CELL_COUNT_Y; y++)
                {
                    var parentGO = cellParentGOs[x, y].transform.Find("Fire").gameObject;

                    var sr = parentGO.GetComponent<SpriteRenderer>();
                    _cellFireEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));
                }
        }


        internal static SpriteRenderer GetSR(int[] xy) => _cellFireEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer;

        private static void ActiveSR(bool isEnabled, int[] xy) => GetSR(xy).enabled = isEnabled;
        internal static void EnableSR(int[] xy) => ActiveSR(true, xy);
        internal static void DisableSR(int[] xy) => ActiveSR(false, xy);
    }
}
