//using Assets.Scripts.ECS.Components;
//using Assets.Scripts.ECS.Game.General.Systems.StartFill;
//using Assets.Scripts.ECS.System.Data.Common;
//using Leopotam.Ecs;
//using System;
//using UnityEngine;
//using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

//namespace Assets.Scripts.ECS.System.View.Game.General.Cell
//{
//    internal sealed class CellBuildViewSystem : IEcsInitSystem
//    {
//        private EcsWorld _gameWorld;

//        private static EcsEntity[,] _cellBuildingEnts;
//        private static EcsEntity[,] _cellBackBuildingEnts;


//        public void Init()
//        {
//            _cellBuildingEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
//            _cellBackBuildingEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

//            for (int x = 0; x < CELL_COUNT_X; x++)
//                for (int y = 0; y < CELL_COUNT_Y; y++)
//                {
//                    var parentGO = MainGameSystem.CellGOs[x, y].transform.Find("Building").gameObject;


//                    var sr = parentGO.GetComponent<SpriteRenderer>();

//                    _cellBuildingEnts[x, y] = _gameWorld.NewEntity()
//                        .Replace(new SpriteRendererComponent(sr));


//                    sr = parentGO.transform.Find("BackBuilding").GetComponent<SpriteRenderer>();

//                    _cellBackBuildingEnts[x, y] = _gameWorld.NewEntity()
//                        .Replace(new SpriteRendererComponent(sr));
//                }
//        }
//    }
//}
