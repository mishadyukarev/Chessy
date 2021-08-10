using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General
{
    internal struct EnvirZoneViewUICom
    {
        private Button _info_Button;
        private Dictionary<ResourceTypes, TextMeshProUGUI> _environment_TextMPs;


        internal EnvirZoneViewUICom(GameObject leftZone_GO)
        {
            var environmentZone_GO = leftZone_GO.transform.Find("EnvironmentZone").gameObject;

            _info_Button = environmentZone_GO.transform.Find("EnvironmentInfoButton").GetComponent<Button>();

            _environment_TextMPs = new Dictionary<ResourceTypes, TextMeshProUGUI>();
            _environment_TextMPs.Add(ResourceTypes.Food, environmentZone_GO.transform.Find("FertilizerResourcesText").GetComponent<TextMeshProUGUI>());
            _environment_TextMPs.Add(ResourceTypes.Wood, environmentZone_GO.transform.Find("FertilizerResourcesText").GetComponent<TextMeshProUGUI>());
            _environment_TextMPs.Add(ResourceTypes.Ore, environmentZone_GO.transform.Find("FertilizerResourcesText").GetComponent<TextMeshProUGUI>());
        }

        internal void SetActiveParent(bool isActive) => _info_Button.transform.parent.gameObject.SetActive(isActive);
        internal void AddListenerToEnvInfo(UnityAction unityAction) => _info_Button.onClick.AddListener(unityAction);
        internal void SetText(ResourceTypes resourceType, string text) => _environment_TextMPs[resourceType].text = text;
    }
}
