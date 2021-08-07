//using Assets.Scripts.ECS.Components;
//using Assets.Scripts.ECS.Game.General.Systems.StartFill;
//using Leopotam.Ecs;
//using System;
//using UnityEngine;
//using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;
//using static Assets.Scripts.Abstractions.ValuesConsts.Colors;

//namespace Assets.Scripts.ECS.System.View.Game.General.Cell
//{
//    internal sealed class CellSupViewSystem : IEcsInitSystem
//    {
//        private EcsWorld gameWorld;

//        private static EcsEntity[,] _cellSupportVisionEnts;

//        internal ref SpriteRendererComponent CellSupVisEnt_SpriteRenderer(int[] xy) => ref _cellSupportVisionEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


//        public void Init()
//        {
//            _cellSupportVisionEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

//            for (int x = 0; x < CELL_COUNT_X; x++)
//                for (int y = 0; y < CELL_COUNT_Y; y++)
//                {

//                }
//        }


        
//    }
//}
