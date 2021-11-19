using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Game
{
    public struct EnvirZoneViewUICom
    {
        private static Button _info_Button;
        private static Dictionary<ResTypes, TextMeshProUGUI> _environment_TextMPs;


        public EnvirZoneViewUICom(GameObject leftZone_GO)
        {
            var environmentZone_GO = leftZone_GO.transform.Find("EnvironmentZone").gameObject;

            _info_Button = environmentZone_GO.transform.Find("EnvironmentInfoButton").GetComponent<Button>();

            _environment_TextMPs = new Dictionary<ResTypes, TextMeshProUGUI>();
            _environment_TextMPs.Add(ResTypes.Food, environmentZone_GO.transform.Find("FertilizerResources_TextMP").GetComponent<TextMeshProUGUI>());
            _environment_TextMPs.Add(ResTypes.Wood, environmentZone_GO.transform.Find("ForestResources_TextMP").GetComponent<TextMeshProUGUI>());
            _environment_TextMPs.Add(ResTypes.Ore, environmentZone_GO.transform.Find("OreResources_TextMP").GetComponent<TextMeshProUGUI>());
        }

        public static void SetActiveParent(bool isActive) => _info_Button.transform.parent.gameObject.SetActive(isActive);
        public static void AddListenerToEnvInfo(UnityAction unityAction) => _info_Button.onClick.AddListener(unityAction);

        public static void SetTextResour(ResTypes resourceType, string text) => _environment_TextMPs[resourceType].text = text;
    }
}
