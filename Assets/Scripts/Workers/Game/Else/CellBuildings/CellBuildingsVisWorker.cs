using Assets.Scripts.ECS.Game.General.Entities;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers.Game.Else.CellBuildings
{
    internal sealed class CellBuildingsVisWorker
    {
        private static CellBuildingEntsContainer _cellBuildingEntsContainer;

        private static SpritesData SpritesData => Main.Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig;


        internal CellBuildingsVisWorker(CellBuildingEntsContainer cellBuildingEntsContainer)
        {
            _cellBuildingEntsContainer = cellBuildingEntsContainer;
        }

        internal static SpriteRenderer GetBackSR(int[] xy) => _cellBuildingEntsContainer.CellBackBuildingEnt_SpriteRendererCom(xy).SpriteRenderer;
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

        internal static SpriteRenderer GetFrontSR(int[] xy) => _cellBuildingEntsContainer.CellBuildEnt_SpriteRendererCom(xy).SpriteRenderer;
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
