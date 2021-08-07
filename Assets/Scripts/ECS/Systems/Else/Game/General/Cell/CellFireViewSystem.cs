//using Assets.Scripts.ECS.Components;
//using Assets.Scripts.ECS.Game.General.Systems.StartFill;
//using Leopotam.Ecs;
//using UnityEngine;
//using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

//namespace Assets.Scripts.ECS.System.View.Game.General.Cell
//{
//    internal sealed class CellFireViewSystem : IEcsInitSystem
//    {
//        private EcsWorld _gameWorld = default;

//        private static EcsEntity[,] _cellFireEnts;

//        public void Init()
//        {
//            _cellFireEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

//            for (int x = 0; x < CELL_COUNT_X; x++)
//                for (int y = 0; y < CELL_COUNT_Y; y++)
//                {
//                    var parentGO = MainGameSystem.CellGOs[x, y].transform.Find("Fire").gameObject;

//                    var sr = parentGO.GetComponent<SpriteRenderer>();
//                    _cellFireEnts[x, y] = _gameWorld.NewEntity()
//                        .Replace(new SpriteRendererComponent(sr));
//                }
//        }


//    }
//}
