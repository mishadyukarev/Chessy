using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct UniqButtonsUIC
    {
        private static Dictionary<UniqButTypes, Button> _buttons;
        private static Dictionary<UniqButTypes, Dictionary<UniqAbilTypes, GameObject>> _zones;
        private static Dictionary<UniqButTypes, TextMeshProUGUI> _cooldowns;

        public UniqButtonsUIC(Transform parent)
        {
            _buttons = new Dictionary<UniqButTypes, Button>();
            _zones = new Dictionary<UniqButTypes, Dictionary<UniqAbilTypes, GameObject>>();
            _cooldowns = new Dictionary<UniqButTypes, TextMeshProUGUI>();

            for (var uniqBut = UniqButTypes.First; uniqBut < UniqButTypes.End; uniqBut++)
            {
                _buttons.Add(uniqBut, parent.Find(uniqBut.ToString()).GetComponent<Button>());
                _zones.Add(uniqBut, new Dictionary<UniqAbilTypes, GameObject>());
                _cooldowns.Add(uniqBut, _buttons[uniqBut].transform.Find("Cooldown").Find("Text (TMP)").GetComponent<TextMeshProUGUI>());

                for (var uniq = UniqAbilTypes.First; uniq < UniqAbilTypes.End; uniq++)
                {
                    _zones[uniqBut].Add(uniq, _buttons[uniqBut].transform.Find(uniq.ToString()).gameObject);
                }
            }

        }

        public static void AddListener(UniqButTypes uniqBut, UnityAction action) => _buttons[uniqBut].onClick.AddListener(action);
        public static void SetActive(UniqButTypes uniqBut, UniqAbilTypes ability)
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
        public static void SetTextCooldown(UniqButTypes uniqBut, string text) => _cooldowns[uniqBut].text = text;
        public static void SetActiveCooldownZone(UniqButTypes uniqBut, bool isActive) => _cooldowns[uniqBut].transform.parent.gameObject.SetActive(isActive);
    }
}