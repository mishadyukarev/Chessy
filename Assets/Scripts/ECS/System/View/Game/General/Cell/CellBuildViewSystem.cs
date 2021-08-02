using Assets.Scripts.ECS.Components;
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
    internal sealed class CellBuildViewSystem : IEcsInitSystem
    {
        private EcsWorld _gameWorld;

        private static EcsEntity[,] _cellBuildingEnts;
        private static EcsEntity[,] _cellBackBuildingEnts;

        private static SpritesData SpritesData => Main.Instance.ECSmanager.EntDataCommElseManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig;


        public void Init()
        {
            _cellBuildingEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
            _cellBackBuildingEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

            for (int x = 0; x < CELL_COUNT_X; x++)
                for (int y = 0; y < CELL_COUNT_Y; y++)
                {
                    var parentGO = StartSpawnCellsViewSystem.CellGOs[x, y].transform.Find("Building").gameObject;


                    var sr = parentGO.GetComponent<SpriteRenderer>();

                    _cellBuildingEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));


                    sr = parentGO.transform.Find("BackBuilding").GetComponent<SpriteRenderer>();

                    _cellBackBuildingEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));
                }
        }



        internal static SpriteRenderer GetBackSR(int[] xy) => _cellBackBuildingEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer;
        internal static void SetBackColor(Color color, int[] xy) => GetBackSR(xy).color = color;
        internal static void SetSpriteBack(BuildingTypes buildingType, int[] xy)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    GetBackSR(xy).sprite = SpritesData.CityBack;
                    break;

                case BuildingTypes.Farm:
                    GetBackSR(xy).sprite = SpritesData.FarmBack;
                    break;

                case BuildingTypes.Woodcutter:
                    GetBackSR(xy).sprite = SpritesData.WoodcutterBack;
                    break;

                case BuildingTypes.Mine:
                    GetBackSR(xy).sprite = SpritesData.MineBack;
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void SetEnabledBackSR(bool isEnabled, int[] xy) => GetBackSR(xy).enabled = isEnabled;

        internal static SpriteRenderer GetFrontSR(int[] xy) => _cellBuildingEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>().SpriteRenderer;
        internal static void SetSpriteFront(BuildingTypes buildingType, int[] xy)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    GetFrontSR(xy).sprite = SpritesData.City;
                    break;

                case BuildingTypes.Farm:
                    GetFrontSR(xy).sprite = SpritesData.Farm;
                    break;

                case BuildingTypes.Woodcutter:
                    GetFrontSR(xy).sprite = SpritesData.Woodcutter;
                    break;

                case BuildingTypes.Mine:
                    GetFrontSR(xy).sprite = SpritesData.Mine;
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void SetEnabledFrontSR(bool isEnabled, int[] xy) => GetFrontSR(xy).enabled = isEnabled;
    }
}
