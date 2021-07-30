using Assets.Scripts.ECS.Entities.Game.General.UI.Vis.Containers;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Workers.Game.UI.Vis.Up
{
    internal sealed class ResourcesVisUIWorker
    {
        private static ResourcesVisUIContainer _currentContainer;

        internal ResourcesVisUIWorker(ResourcesVisUIContainer ourContainer)
        {
            _currentContainer = ourContainer;
        }

        private static TextMeshProUGUI GetResourceTMP(ResourceTypes economyTypes)
        {
            switch (economyTypes)
            {
                case ResourceTypes.None:
                    throw new Exception();

                case ResourceTypes.Food:
                    return _currentContainer.FoodInfoUIEnt_TextMeshProUGUICom.TextMeshProUGUI;

                case ResourceTypes.Wood:
                    return _currentContainer.WoodInfoUIEnt_TextMeshProUGUICom.TextMeshProUGUI;

                case ResourceTypes.Ore:
                    return _currentContainer.OreInfoUIEnt_TextMeshProUGUICom.TextMeshProUGUI;

                case ResourceTypes.Iron:
                    return _currentContainer.IronInfoUIEnt_TextMeshProUGUICom.TextMeshProUGUI;

                case ResourceTypes.Gold:
                    return _currentContainer.GoldInfoUIEnt_TextMeshProUGUICom.TextMeshProUGUI;

                default:
                    throw new Exception();
            }
        }

        internal static void SetMainText(ResourceTypes economyType, string text) => GetResourceTMP(economyType).text = text;
        internal static void SetMainColor(ResourceTypes economyType, Color color) => GetResourceTMP(economyType).color = color;

        internal static void SetAddingText(ResourceTypes resourceType, string text)
        {
            switch (resourceType)
            {
                case ResourceTypes.None:
                    throw new Exception();

                case ResourceTypes.Food:
                    _currentContainer.FoodInfoUIEnt_AddingTMPUICom.TextMeshProUGUI.text = text;
                    break;

                case ResourceTypes.Wood:
                    _currentContainer.WoodInfoUIEnt_AddingTMPUICom.TextMeshProUGUI.text = text;
                    break;

                case ResourceTypes.Ore:
                    _currentContainer.OreInfoUIEnt_AddingTMPUICom.TextMeshProUGUI.text = text;
                    break;

                case ResourceTypes.Iron:
                    throw new Exception();

                case ResourceTypes.Gold:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }
    }
}
