using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Leopotam.Ecs;
using System;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts.ECS.System.View.Game.General.Cell
{
    internal sealed class CellSupVisBarsViewSystem : IEcsInitSystem
    {
        private EcsWorld _gameWorld;

        private static EcsEntity[,] _cellHpSupStatEnts;
        private static EcsEntity[,] _cellFertilizeSupStatEnts;
        private static EcsEntity[,] _cellForestSupStatEnts;
        private static EcsEntity[,] _cellOreSupStatEnts;

        public void Init()
        {
            _cellHpSupStatEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
            _cellFertilizeSupStatEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
            _cellForestSupStatEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
            _cellOreSupStatEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

            for (int x = 0; x < CELL_COUNT_X; x++)
                for (int y = 0; y < CELL_COUNT_Y; y++)
                {
                    var parentGO = MainGameSystem.CellGOs[x, y].transform.Find("SupportStatic").gameObject;

                    var sr = parentGO.transform.Find("Hp").GetComponent<SpriteRenderer>();
                    _cellHpSupStatEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));

                    sr = parentGO.transform.Find("Fertilizer").GetComponent<SpriteRenderer>();
                    _cellFertilizeSupStatEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));

                    sr = parentGO.transform.Find("Forest").GetComponent<SpriteRenderer>();
                    _cellForestSupStatEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));

                    sr = parentGO.transform.Find("Ore").GetComponent<SpriteRenderer>();
                    _cellOreSupStatEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));
                }
        }



        private static SpriteRenderer GetSR(SupportStaticTypes supportStaticType, int[] xy)
        {
            switch (supportStaticType)
            {
                case SupportStaticTypes.None:
                    throw new Exception();

                case SupportStaticTypes.Fertilizer:
                    return _cellFertilizeSupStatEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer;

                case SupportStaticTypes.Wood:
                    return _cellForestSupStatEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer;

                case SupportStaticTypes.Ore:
                    return _cellOreSupStatEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer;

                case SupportStaticTypes.Hp:
                    return _cellHpSupStatEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer;

                default:
                    throw new Exception();
            }
        }

        internal static void ActiveVision(bool isActive, SupportStaticTypes supportStaticType, int[] xy) => GetSR(supportStaticType, xy).enabled = isActive;
        internal static void SetColor(SupportStaticTypes supportStaticType, Color color, int[] xy) => GetSR(supportStaticType, xy).color = color;
        internal static void SetScale(SupportStaticTypes supportStaticType, Vector3 scale, int[] xy) => GetSR(supportStaticType, xy).transform.localScale = scale;
    }
}
