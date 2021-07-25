using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.Game.General.Entities.Containers
{
    internal sealed class CellSupVisBarsEntsContainer : CellEntsAbstractContainer
    {
        private EcsEntity[,] _cellHpSupStatEnts;
        internal ref SpriteRendererComponent CellHpSupStatEnt_SpriteRendererCom(int[] xy) => ref _cellHpSupStatEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        private EcsEntity[,] _cellFertilizeSupStatEnts;
        internal ref SpriteRendererComponent CellFertilizeSupStatEnt_SprRendCom(int[] xy) => ref _cellFertilizeSupStatEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        private EcsEntity[,] _cellForestSupStatEnts;
        internal ref SpriteRendererComponent CellWoodSupStatEnt_SprRendCom(int[] xy) => ref _cellForestSupStatEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        private EcsEntity[,] _cellOreSupStatEnts;
        internal ref SpriteRendererComponent CellOreSupStatEnt_SprRendCom(int[] xy) => ref _cellOreSupStatEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        internal CellSupVisBarsEntsContainer(GameObject[,] cellParent, EcsWorld gameWorld) : base(cellParent)
        {
            _cellHpSupStatEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];
            _cellFertilizeSupStatEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];
            _cellForestSupStatEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];
            _cellOreSupStatEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];

            for (int x = 0; x < Xamount; x++)
                for (int y = 0; y < Yamount; y++)
                {
                    var parentGO = cellParent[x, y].transform.Find("SupportStatic").gameObject;

                    var sr = parentGO.transform.Find("Hp").GetComponent<SpriteRenderer>();
                    _cellHpSupStatEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));

                    sr = parentGO.transform.Find("Fertilizer").GetComponent<SpriteRenderer>();
                    _cellFertilizeSupStatEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));

                    sr = parentGO.transform.Find("Forest").GetComponent<SpriteRenderer>();
                    _cellForestSupStatEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));

                    sr = parentGO.transform.Find("Ore").GetComponent<SpriteRenderer>();
                    _cellOreSupStatEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));
                }
        }
    }
}
