using System;
using UnityEngine;

namespace Assets.Scripts.Workers.UI.Info
{
    internal class InfoResourcesUIWorker : MainGeneralUIWorker
    {
        internal static void SetMainText(ResourceTypes economyTypes, string text)
        {
            switch (economyTypes)
            {
                case ResourceTypes.None:
                    throw new Exception();

                case ResourceTypes.Food:
                    EGGUIM.FoodInfoUIEnt_TextMeshProUGUICom.Text = text;
                    break;

                case ResourceTypes.Wood:
                    EGGUIM.WoodInfoUIEnt_TextMeshProUGUICom.Text = text;
                    break;

                case ResourceTypes.Ore:
                    EGGUIM.OreInfoUIEnt_TextMeshProUGUICom.Text = text;
                    break;

                case ResourceTypes.Iron:
                    EGGUIM.IronInfoUIEnt_TextMeshProUGUICom.Text = text;
                    break;

                case ResourceTypes.Gold:
                    EGGUIM.GoldInfoUIEnt_TextMeshProUGUICom.Text = text;
                    break;

                default:
                    break;
            }
        }
        internal static void SetMainColor(ResourceTypes economyTypes, Color color)
        {
            switch (economyTypes)
            {
                case ResourceTypes.None:
                    throw new Exception();

                case ResourceTypes.Food:
                    EGGUIM.FoodInfoUIEnt_TextMeshProUGUICom.Color = color;
                    break;

                case ResourceTypes.Wood:
                    EGGUIM.WoodInfoUIEnt_TextMeshProUGUICom.Color = color;
                    break;

                case ResourceTypes.Ore:
                    EGGUIM.OreInfoUIEnt_TextMeshProUGUICom.Color = color;
                    break;

                case ResourceTypes.Iron:
                    EGGUIM.IronInfoUIEnt_TextMeshProUGUICom.Color = color;
                    break;

                case ResourceTypes.Gold:
                    EGGUIM.GoldInfoUIEnt_TextMeshProUGUICom.Color = color;
                    break;

                default:
                    break;
            }
        }

        internal static void SetAddingText(ResourceTypes resourceType, string text)
        {
            switch (resourceType)
            {
                case ResourceTypes.None:
                    throw new Exception();

                case ResourceTypes.Food:
                    EGGUIM.FoodInfoUIEnt_AddingTMPUICom.Text = text;
                    break;

                case ResourceTypes.Wood:
                    EGGUIM.WoodInfoUIEnt_AddingTMPUICom.Text = text;
                    break;

                case ResourceTypes.Ore:
                    EGGUIM.OreInfoUIEnt_AddingTMPUICom.Text = text;
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
