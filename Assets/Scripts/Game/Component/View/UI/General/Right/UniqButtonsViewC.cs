using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct UniqButtonsViewC
    {
        private static Dictionary<UniqButtonTypes, Button> _buttons;
        private static Dictionary<UniqButtonTypes, Dictionary<UniqAbilTypes, GameObject>> _zones;
        private static Dictionary<UniqButtonTypes, TextMeshProUGUI> _cooldowns;

        public UniqButtonsViewC(Transform parent)
        {
            _buttons = new Dictionary<UniqButtonTypes, Button>();
            _zones = new Dictionary<UniqButtonTypes, Dictionary<UniqAbilTypes, GameObject>>();
            _cooldowns = new Dictionary<UniqButtonTypes, TextMeshProUGUI>();

            for (var uniqBut = (UniqButtonTypes)1; uniqBut < (UniqButtonTypes)typeof(UniqButtonTypes).GetEnumNames().Length; uniqBut++)
            {
                _buttons.Add(uniqBut, parent.Find(uniqBut.ToString()).GetComponent<Button>());
                _zones.Add(uniqBut, new Dictionary<UniqAbilTypes, GameObject>());
                _cooldowns.Add(uniqBut, _buttons[uniqBut].transform.Find("Cooldown").Find("Text (TMP)").GetComponent<TextMeshProUGUI>());

                for (var uniqAbil = (UniqAbilTypes)1; uniqAbil < (UniqAbilTypes)typeof(UniqAbilTypes).GetEnumNames().Length; uniqAbil++)
                {
                    _zones[uniqBut].Add(uniqAbil, _buttons[uniqBut].transform.Find(uniqAbil.ToString()).gameObject);
                }
            }

        }

        public static void AddListener(UniqButtonTypes uniqBut, UnityAction action) => _buttons[uniqBut].onClick.AddListener(action);
        public static void SetActive(UniqButtonTypes uniqBut, UniqAbilTypes ability)
        {
            if(ability == default)
            {
                _buttons[uniqBut].gameObject.SetActive(false);
            }
            else
            {
                _buttons[uniqBut].gameObject.SetActive(true);

                _zones[uniqBut][ability].SetActive(true);
                foreach (var item_0 in _zones)
                {
                    if (item_0.Key == uniqBut)
                    {
                        foreach (var item_1 in item_0.Value)
                        {
                            if (item_1.Key != ability)
                            {
                                _zones[item_0.Key][item_1.Key].SetActive(false);
                            }
                        }
                    }
                }
            }
        }
        public static void SetTextCooldown(UniqButtonTypes uniqBut, string text) => _cooldowns[uniqBut].text = text;
        public static void SetActiveCooldownZone(UniqButtonTypes uniqBut, bool isActive) => _cooldowns[uniqBut].transform.parent.gameObject.SetActive(isActive);
    }
}