using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.System.Data.Common;
using Leopotam.Ecs;
using System;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts.ECS.System.View.Game.General.Cell
{
    internal sealed class CellUnitViewSystem : IEcsInitSystem
    {
        private EcsWorld _gameWorld;

        private static EcsEntity[,] _cellUnitEnts;

        private static SpritesData SpritesData => MainCommonSystem.ResourcesEnt_ResourcesCommonCom.SpritesConfig;


        public void Init()
        {
            _cellUnitEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

            for (int x = 0; x < CELL_COUNT_X; x++)
                for (int y = 0; y < CELL_COUNT_Y; y++)
                {
                    var sr = StartSpawnCellsViewSystem.CellGOs[x, y].transform.Find("Unit").GetComponent<SpriteRenderer>();
                    _cellUnitEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));
                }
        }

        private static SpriteRenderer GetUnitSR(int[] xy) => _cellUnitEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer;

        internal static void SetSprite(UnitTypes unitType, params int[] xy)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    _cellUnitEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer.sprite = SpritesData.KingSprite;
                    break;

                case UnitTypes.Pawn:
                    _cellUnitEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer.sprite = SpritesData.PawnSprite;
                    break;

                case UnitTypes.PawnSword:
                    _cellUnitEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer.sprite = SpritesData.PawnSwordSprite;
                    break;

                case UnitTypes.Rook:
                    _cellUnitEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer.sprite = SpritesData.RookSprite;
                    break;

                case UnitTypes.RookCrossbow:
                    _cellUnitEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer.sprite = SpritesData.RookCrossbowSprite;
                    break;

                case UnitTypes.Bishop:
                    _cellUnitEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer.sprite = SpritesData.BishopSprite;
                    break;

                case UnitTypes.BishopCrossbow:
                    _cellUnitEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer.sprite = SpritesData.BishopCrossbowSprite;
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void EnableUnitSR(bool isEnabled, int[] xy) => GetUnitSR(xy).enabled = isEnabled;
        internal static void SetColorSR(Color color, int[] xy) => GetUnitSR(xy).color = color;

        internal static void SetEnabledUnit(bool isEnabled, int[] xy) => GetUnitSR(xy).enabled = isEnabled;

        internal static void Flip(bool isActivated, XyTypes flipType, int[] xy)
        {
            switch (flipType)
            {
                case XyTypes.X:
                    GetUnitSR(xy).flipX = isActivated;
                    break;

                case XyTypes.Y:
                    GetUnitSR(xy).flipY = isActivated;
                    break;

                default:
                    break;
            }
        }
        internal void SetRotation(Vector3 rotation, int[] xy) => GetUnitSR(xy).transform.rotation = Quaternion.Euler(rotation);


        internal static void ActiveSelectorVisionUnit(bool isActive, UnitTypes unitType, int[] xy)
        {
            if (isActive)
            {
                EnableUnitSR(isActive, xy);
                SetSprite(unitType, xy);
            }

            else
            {
                EnableUnitSR(isActive, xy);
            }
        }
    }
}
