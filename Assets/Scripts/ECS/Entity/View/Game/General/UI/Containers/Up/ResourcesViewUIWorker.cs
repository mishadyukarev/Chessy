using Assets.Scripts.ECS.Components.UI;
using Leopotam.Ecs;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Workers.Game.UI.Vis.Up
{
    internal struct ResourcesViewUIWorker
    {
        private static EcsEntity _foodInfoUIEnt;
        private static EcsEntity _woodInfoUIEnt;
        private static EcsEntity _oreInfoUIEnt;
        private static EcsEntity _ironInfoUIEnt;
        private static EcsEntity _goldInfoUIEnt;

        internal ResourcesViewUIWorker(EcsWorld gameWorld, GameObject upZoneGO)
        {
            _foodInfoUIEnt = gameWorld.NewEntity()
                .Replace(new TextMeshProUGUIComponent(upZoneGO.transform.Find("FoodAmount").GetComponent<TextMeshProUGUI>()))
                .Replace(new AddingTMPUIComponent(upZoneGO.transform.Find("FoodAdding_TMP").GetComponent<TextMeshProUGUI>()));

            _woodInfoUIEnt = gameWorld.NewEntity()
                .Replace(new TextMeshProUGUIComponent(upZoneGO.transform.Find("WoodAmount").GetComponent<TextMeshProUGUI>()))
                .Replace(new AddingTMPUIComponent(upZoneGO.transform.Find("WoodAdding_TMP").GetComponent<TextMeshProUGUI>()));

            _oreInfoUIEnt = gameWorld.NewEntity()
                .Replace(new TextMeshProUGUIComponent(upZoneGO.transform.Find("OreAmount").GetComponent<TextMeshProUGUI>()))
                .Replace(new AddingTMPUIComponent(upZoneGO.transform.Find("OreAdding_TMP").GetComponent<TextMeshProUGUI>()));

            _ironInfoUIEnt = gameWorld.NewEntity()
                .Replace(new TextMeshProUGUIComponent(upZoneGO.transform.Find("IronAmount").GetComponent<TextMeshProUGUI>()));

            _goldInfoUIEnt = gameWorld.NewEntity()
                .Replace(new TextMeshProUGUIComponent(upZoneGO.transform.Find("GoldAmount").GetComponent<TextMeshProUGUI>()));
        }

        private static TextMeshProUGUI GetResourceTMP(ResourceTypes economyTypes)
        {
            switch (economyTypes)
            {
                case ResourceTypes.None:
                    throw new Exception();

                case ResourceTypes.Food:
                    return _foodInfoUIEnt.Get<TextMeshProUGUIComponent>().TextMeshProUGUI;

                case ResourceTypes.Wood:
                    return _woodInfoUIEnt.Get<TextMeshProUGUIComponent>().TextMeshProUGUI;

                case ResourceTypes.Ore:
                    return _oreInfoUIEnt.Get<TextMeshProUGUIComponent>().TextMeshProUGUI;

                case ResourceTypes.Iron:
                    return _ironInfoUIEnt.Get<TextMeshProUGUIComponent>().TextMeshProUGUI;

                case ResourceTypes.Gold:
                    return _goldInfoUIEnt.Get<TextMeshProUGUIComponent>().TextMeshProUGUI;

                default:
                    throw new Exception();
            }
        }

        internal static void SetMainText(ResourceTypes economyType, string text) => GetResourceTMP(economyType).text = text;
        internal static void SetMainColor(ResourceTypes economyType, Color color) => GetResourceTMP(economyType).color = color;

        internal static void SetAddingText(ResourceTypes resourceType, string text)
        {
            var v = _foodInfoUIEnt.Get<TextMeshProUGUIComponent>().TextMeshProUGUI;

            switch (resourceType)
            {
                case ResourceTypes.None:
                    throw new Exception();

                case ResourceTypes.Food:
                    _foodInfoUIEnt.Get<AddingTMPUIComponent>().TextMeshProUGUI.text = text;
                    break;

                case ResourceTypes.Wood:
                    _woodInfoUIEnt.Get<AddingTMPUIComponent>().TextMeshProUGUI.text = text;
                    break;

                case ResourceTypes.Ore:
                    _oreInfoUIEnt.Get<AddingTMPUIComponent>().TextMeshProUGUI.text = text;
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
