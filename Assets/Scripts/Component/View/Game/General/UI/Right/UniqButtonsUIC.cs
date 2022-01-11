using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Game
{
    public struct UniqButtonsUIC
    {
        private static Dictionary<UniqueButtonTypes, Button> _buttons;
        private static Dictionary<UniqueButtonTypes, Dictionary<UniqueAbilityTypes, GameObject>> _zones;
        private static Dictionary<UniqueButtonTypes, TextMeshProUGUI> _cooldowns;

        public UniqButtonsUIC(Transform parent)
        {
            _buttons = new Dictionary<UniqueButtonTypes, Button>();
            _zones = new Dictionary<UniqueButtonTypes, Dictionary<UniqueAbilityTypes, GameObject>>();
            _cooldowns = new Dictionary<UniqueButtonTypes, TextMeshProUGUI>();

            for (var uniqBut = UniqueButtonTypes.First; uniqBut < UniqueButtonTypes.End; uniqBut++)
            {
                _buttons.Add(uniqBut, parent.Find(uniqBut.ToString()).GetComponent<Button>());
                _zones.Add(uniqBut, new Dictionary<UniqueAbilityTypes, GameObject>());
                _cooldowns.Add(uniqBut, _buttons[uniqBut].transform.Find("Cooldown").Find("Text (TMP)").GetComponent<TextMeshProUGUI>());

                for (var uniq = UniqueAbilityTypes.First; uniq < UniqueAbilityTypes.End; uniq++)
                {
                    _zones[uniqBut].Add(uniq, _buttons[uniqBut].transform.Find(uniq.ToString()).gameObject);
                }
            }

        }

        public static void AddListener(UniqueButtonTypes uniqBut, UnityAction action) => _buttons[uniqBut].onClick.AddListener(action);
        public static void SetActive(UniqueButtonTypes uniqBut, UniqueAbilityTypes ability)
        {
            if (ability == default)
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
        public static void SetTextCooldown(UniqueButtonTypes uniqBut, string text) => _cooldowns[uniqBut].text = text;
        public static void SetActiveCooldownZone(UniqueButtonTypes uniqBut, bool isActive) => _cooldowns[uniqBut].transform.parent.gameObject.SetActive(isActive);
    }
}