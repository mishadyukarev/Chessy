﻿using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts.ECS.System.View.Game.General.Cell
{
    internal sealed class CellEnvViewSystem : IEcsInitSystem
    {
        private EcsWorld _gameWorld;

        private static EcsEntity[,] _cellFertilizerEnts;
        private static EcsEntity[,] _cellYoungForestEnts;
        private static EcsEntity[,] _cellAdultForestEnts;
        private static EcsEntity[,] _cellHillEnts;
        private static EcsEntity[,] _cellMountainEnts;
        public void Init()
        {
            _cellFertilizerEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
            _cellYoungForestEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
            _cellAdultForestEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
            _cellHillEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
            _cellMountainEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

            for (int x = 0; x < CELL_COUNT_X; x++)
                for (int y = 0; y < CELL_COUNT_Y; y++)
                {
                    var parentGO = StartSpawnCellsViewSystem.CellGOs[x, y].transform.Find("Environments").gameObject;

                    var sr = parentGO.transform.Find("Fertilizer").GetComponent<SpriteRenderer>();

                    _cellFertilizerEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));



                    sr = parentGO.transform.Find("YoungForest").GetComponent<SpriteRenderer>();

                    _cellYoungForestEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));



                    sr = parentGO.transform.Find("AdultForest").GetComponent<SpriteRenderer>();

                    _cellAdultForestEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));



                    sr = parentGO.transform.Find("Hill").GetComponent<SpriteRenderer>();

                    _cellHillEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));



                    sr = parentGO.transform.Find("Mountain").GetComponent<SpriteRenderer>();

                    _cellMountainEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));
                }
        }


        private static SpriteRenderer GetSR(EnvironmentTypes environmentType, int[] xy)
        {
            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    return _cellFertilizerEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer;

                case EnvironmentTypes.YoungForest:
                    return _cellYoungForestEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer;

                case EnvironmentTypes.AdultForest:
                    return _cellAdultForestEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer;

                case EnvironmentTypes.Hill:
                    return _cellHillEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer;

                case EnvironmentTypes.Mountain:
                    return _cellMountainEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer;

                default:
                    throw new Exception();
            }
        }

        internal static void ActiveEnvirVis(bool isEnabled, EnvironmentTypes environmentType, int[] xy) => GetSR(environmentType, xy).enabled = isEnabled;

    }
}