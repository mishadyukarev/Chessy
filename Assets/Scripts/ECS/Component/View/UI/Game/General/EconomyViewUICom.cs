using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General
{
    internal struct EconomyViewUICom
    {
        private Dictionary<ResourceTypes, TextMeshProUGUI> _amountResources_TextMP;
        private Dictionary<ResourceTypes, TextMeshProUGUI> _amountAddingResources_TextMP;

        internal EconomyViewUICom(GameObject upZone_GO)
        {
            _amountResources_TextMP = new Dictionary<ResourceTypes, TextMeshProUGUI>();
            _amountAddingResources_TextMP = new Dictionary<ResourceTypes, TextMeshProUGUI>();

            for (ResourceTypes resourcesType = 0; resourcesType < (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length; resourcesType++)
            {
                if (resourcesType == ResourceTypes.Food)
                {
                    _amountResources_TextMP.Add(resourcesType, upZone_GO.transform.Find("FoodAmount").GetComponent<TextMeshProUGUI>());
                    _amountAddingResources_TextMP.Add(resourcesType, upZone_GO.transform.Find("FoodAdding_TMP").GetComponent<TextMeshProUGUI>());
                }
                    
                else if(resourcesType == ResourceTypes.Wood)
                {
                    _amountResources_TextMP.Add(resourcesType, upZone_GO.transform.Find("WoodAmount").GetComponent<TextMeshProUGUI>());
                    _amountAddingResources_TextMP.Add(resourcesType, upZone_GO.transform.Find("WoodAdding_TMP").GetComponent<TextMeshProUGUI>());
                }

                else if(resourcesType == ResourceTypes.Ore)
                {
                    _amountResources_TextMP.Add(resourcesType, upZone_GO.transform.Find("OreAmount").GetComponent<TextMeshProUGUI>());
                    _amountAddingResources_TextMP.Add(resourcesType, upZone_GO.transform.Find("OreAdding_TMP").GetComponent<TextMeshProUGUI>());
                }

                else if (resourcesType == ResourceTypes.Iron)
                {
                    _amountResources_TextMP.Add(resourcesType, upZone_GO.transform.Find("IronAmount").GetComponent<TextMeshProUGUI>());
                }

                else if (resourcesType == ResourceTypes.Gold)
                {
                    _amountResources_TextMP.Add(resourcesType, upZone_GO.transform.Find("GoldAmount").GetComponent<TextMeshProUGUI>());
                }
            }
        }

        internal void SetMainText(ResourceTypes resourcesType, string text) => _amountResources_TextMP[resourcesType].text = text;
        internal void SetMainColor(ResourceTypes resourcesType, Color color) => _amountResources_TextMP[resourcesType].color = color;

        internal void SetAddingText(ResourceTypes resourceType, string text) => _amountAddingResources_TextMP[resourceType].text = text;
    }
}
