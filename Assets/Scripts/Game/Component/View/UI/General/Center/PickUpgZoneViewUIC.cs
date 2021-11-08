using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct PickUpgZoneViewUIC
    {
        private static GameObject _parent_GO;
        private static Dictionary<PickUpgradeTypes, Button> _buttons;

        public static bool IsActiveZone => _parent_GO.activeSelf;

        public PickUpgZoneViewUIC(Transform center_Trans)
        {
            _parent_GO = center_Trans.Find("PickUpgradeZone").gameObject;

            _buttons = new Dictionary<PickUpgradeTypes, Button>();
            foreach (var upgBut in typeof(PickUpgradeTypes).GetEnumValues())
            {
                if ((PickUpgradeTypes)upgBut != PickUpgradeTypes.None)
                    _buttons.Add((PickUpgradeTypes)upgBut, _parent_GO.transform.Find(upgBut.ToString() + "_Button").GetComponent<Button>());
            }
        }

        public static void SetActiveZone(bool isActive) => _parent_GO.SetActive(isActive);
        public static void SetActive_But(PickUpgradeTypes upgButType, bool isActive) => _buttons[upgButType].gameObject.SetActive(isActive);

        public static void AddList_But(PickUpgradeTypes upgButType, UnityAction unityAction) => _buttons[upgButType].onClick.AddListener(unityAction);
    }
}