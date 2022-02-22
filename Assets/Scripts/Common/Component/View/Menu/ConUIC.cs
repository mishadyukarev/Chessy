﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Menu
{
    public struct ConUIC
    {
        static Button _button;

        public ConUIC(bool isOnline, RectTransform zoneRectTrans)
        {
            if (isOnline)
            {
                _button = zoneRectTrans.transform.Find("ConnectOnline_Button").GetComponent<Button>();
            }
            else _button = zoneRectTrans.transform.Find("ConnectOffline_Button").GetComponent<Button>();
        }

        public static void AddListConnect_Button(UnityAction unityAction) => _button.onClick.AddListener(unityAction);
        public static void SetActive_Button(bool isActive) => _button.gameObject.SetActive(isActive);
    }
}