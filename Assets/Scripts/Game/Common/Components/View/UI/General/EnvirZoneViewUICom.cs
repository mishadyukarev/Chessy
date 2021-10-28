using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Game
{
    public struct EnvirZoneViewUICom
    {
        private Button _info_Button;
        private Dictionary<ResourceTypes, TextMeshProUGUI> _environment_TextMPs;


        public EnvirZoneViewUICom(GameObject leftZone_GO)
        {
            var environmentZone_GO = leftZone_GO.transform.Find("EnvironmentZone").gameObject;

            _info_Button = environmentZone_GO.transform.Find("EnvironmentInfoButton").GetComponent<Button>();

            _environment_TextMPs = new Dictionary<ResourceTypes, TextMeshProUGUI>();
            _environment_TextMPs.Add(ResourceTypes.Food, environmentZone_GO.transform.Find("FertilizerResources_TextMP").GetComponent<TextMeshProUGUI>());
            _environment_TextMPs.Add(ResourceTypes.Wood, environmentZone_GO.transform.Find("ForestResources_TextMP").GetComponent<TextMeshProUGUI>());
            _environment_TextMPs.Add(ResourceTypes.Ore, environmentZone_GO.transform.Find("OreResources_TextMP").GetComponent<TextMeshProUGUI>());
        }

        public void SetActiveParent(bool isActive) => _info_Button.transform.parent.gameObject.SetActive(isActive);
        public void AddListenerToEnvInfo(UnityAction unityAction) => _info_Button.onClick.AddListener(unityAction);

        public void SetTextResour(ResourceTypes resourceType, string text) => _environment_TextMPs[resourceType].text = text;
    }
}
