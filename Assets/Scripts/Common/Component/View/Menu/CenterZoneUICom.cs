﻿using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Menu
{
    public struct CenterZoneUICom
    {
        private static TextMeshProUGUI _log_TextMP;

        private static Slider _musicSlider;
        private static Toggle _hint_Toggle;

        private static Button _shop_Button;
        private static Button _discord_But;

        private static Button _likeGame_Button;
        private static Button _exit_Button;


        public static float MusicVolume => _musicSlider.value;
        public static bool IsOn => _hint_Toggle.isOn;

        public CenterZoneUICom(Transform centerZone_Trans, float value, bool isOn)
        {
            _log_TextMP = centerZone_Trans.Find("Log_TextMP").GetComponent<TextMeshProUGUI>();

            _musicSlider = centerZone_Trans.Find("Slider").GetComponent<Slider>();
            _musicSlider.value = value;
            _hint_Toggle = centerZone_Trans.Find("Hint_Toggle").GetComponent<Toggle>();

            _discord_But = centerZone_Trans.transform.Find("JoinDiscordButton").GetComponent<Button>();
            _shop_Button = centerZone_Trans.Find("Help_Button").GetComponent<Button>();

            _likeGame_Button = centerZone_Trans.Find("LikeGame_Button").GetComponent<Button>();
            _exit_Button = centerZone_Trans.Find("QuitButton").GetComponent<Button>();

            _hint_Toggle.isOn = isOn;
        }

        public static void SetLogText(string text) => _log_TextMP.text = text;

        public static void AddListDiscord_But(UnityAction unityAction) => _discord_But.onClick.AddListener(unityAction);
        public static void AddListShop_But(UnityAction unityAction) => _shop_Button.onClick.AddListener(unityAction);
        public static void AddListLikeGame_But(UnityAction unityAction) => _likeGame_Button.onClick.AddListener(unityAction);
        public static void AddListQuit_But(UnityAction unityAction) => _exit_Button.onClick.AddListener(unityAction);
    }
}