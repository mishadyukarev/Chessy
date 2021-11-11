﻿using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct ScoutUIC
    {
        private static Button _button;
        private static GameObject _cooldown_go;
        private static TextMeshProUGUI _cooldown_tmp;

        public ScoutUIC(Transform down)
        {
            _button = down.Find("Scout_Button").GetComponent<Button>();

            _cooldown_go = _button.transform.Find("Cooldown").gameObject;
            _cooldown_tmp = _cooldown_go.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        }

        public static void AddListScout(UnityAction unityAction) => _button.onClick.AddListener(unityAction);
        public static void SetActiveScout(bool isActive, int cooldown)
        {
            _button.gameObject.SetActive(isActive);

            if (isActive && cooldown > 0)
            {
                _cooldown_go.SetActive(true);
                _cooldown_tmp.text = cooldown.ToString();     
            }
            else
            {
                _cooldown_go.SetActive(false);
            }
        }
    }
}