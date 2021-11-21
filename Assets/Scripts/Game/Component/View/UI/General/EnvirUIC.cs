using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Game
{
    public struct EnvirUIC
    {
        private static Button _info;
        private static Dictionary<ResTypes, TextMeshProUGUI> _envs;


        public EnvirUIC(GameObject leftZone_GO)
        {
            var environmentZone_GO = leftZone_GO.transform.Find("EnvironmentZone").gameObject;

            _info = environmentZone_GO.transform.Find("EnvironmentInfoButton").GetComponent<Button>();

            _envs = new Dictionary<ResTypes, TextMeshProUGUI>();
            _envs.Add(ResTypes.Food, environmentZone_GO.transform.Find("FertilizerResources_TextMP").GetComponent<TextMeshProUGUI>());
            _envs.Add(ResTypes.Wood, environmentZone_GO.transform.Find("ForestResources_TextMP").GetComponent<TextMeshProUGUI>());
            _envs.Add(ResTypes.Ore, environmentZone_GO.transform.Find("OreResources_TextMP").GetComponent<TextMeshProUGUI>());
        }

        public static void SetActiveParent(bool isActive) => _info.transform.parent.gameObject.SetActive(isActive);
        public static void AddListenerToEnvInfo(UnityAction unityAction) => _info.onClick.AddListener(unityAction);

        public static void SetTextResour(ResTypes resourceType, string text) => _envs[resourceType].text = text;
    }
}
