using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Game
{
    public struct PickUpgZoneViewUIC
    {
        private static GameObject _parent_GO;
        private static Dictionary<UpgButTypes, Button> _buttons;

        public PickUpgZoneViewUIC(Transform center_Trans)
        {
            _parent_GO = center_Trans.Find("PickUpgradeZone").gameObject;

            _buttons = new Dictionary<UpgButTypes, Button>();
            foreach (var upgBut in typeof(UpgButTypes).GetEnumValues())
            {
                if ((UpgButTypes)upgBut != UpgButTypes.None)
                    _buttons.Add((UpgButTypes)upgBut, _parent_GO.transform.Find(upgBut.ToString() + "_Button").GetComponent<Button>());
            }
        }

        public static void SetActiveZone(bool isActive) => _parent_GO.SetActive(isActive);
        public static void SetActive_But(UpgButTypes upgButType, bool isActive) => _buttons[upgButType].gameObject.SetActive(isActive);
    }
}