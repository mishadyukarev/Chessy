using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Workers.UI.Info
{
    internal class InfoResourcesUIWorker : MainGeneralUIWorker
    {
        private static TextMeshProUGUI GetResourceTMP(ResourceTypes economyTypes)
        {
            switch (economyTypes)
            {
                case ResourceTypes.None:
                    throw new Exception();

                case ResourceTypes.Food:
                    return EGGUIM.FoodInfoUIEnt_TextMeshProUGUICom.TextMeshProUGUI;

                case ResourceTypes.Wood:
                    return EGGUIM.WoodInfoUIEnt_TextMeshProUGUICom.TextMeshProUGUI;

                case ResourceTypes.Ore:
                    return EGGUIM.OreInfoUIEnt_TextMeshProUGUICom.TextMeshProUGUI;

                case ResourceTypes.Iron:
                    return EGGUIM.IronInfoUIEnt_TextMeshProUGUICom.TextMeshProUGUI;

                case ResourceTypes.Gold:
                    return EGGUIM.GoldInfoUIEnt_TextMeshProUGUICom.TextMeshProUGUI;

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
                    EGGUIM.FoodInfoUIEnt_AddingTMPUICom.TextMeshProUGUI.text = text;
                    break;

                case ResourceTypes.Wood:
                    EGGUIM.FoodInfoUIEnt_AddingTMPUICom.TextMeshProUGUI.text = text;
                    break;

                case ResourceTypes.Ore:
                    EGGUIM.FoodInfoUIEnt_AddingTMPUICom.TextMeshProUGUI.text = text;
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
