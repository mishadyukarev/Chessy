using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct LeftEnvironmentUIEs
    {
        public ButtonUIC InfoButtonC;
        public Dictionary<ResourceTypes, TextUIC> Envs;


        public readonly Chessy.Common.Component.GameObjectVC Zone;

        public LeftEnvironmentUIEs(in Transform leftZone)
        {
            Envs = new Dictionary<ResourceTypes, TextUIC>();

            var envZone = leftZone.Find("Environment+");

            Zone = new Chessy.Common.Component.GameObjectVC(envZone.gameObject);

            InfoButtonC = new ButtonUIC(envZone.Find("EnvironmentInfoButton").GetComponent<Button>());


            Envs.Add(ResourceTypes.Food, new TextUIC(envZone.Find("FertilizerResources_TextMP").GetComponent<TextMeshProUGUI>()));
            Envs.Add(ResourceTypes.Wood, new TextUIC(envZone.Find("ForestResources_TextMP").GetComponent<TextMeshProUGUI>()));
            Envs.Add(ResourceTypes.Ore, new TextUIC(envZone.Find("OreResources_TextMP").GetComponent<TextMeshProUGUI>()));
        }
    }
}