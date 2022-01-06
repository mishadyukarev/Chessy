using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Game
{
    public struct DonerUICom
    {
        private static Button _doner_Button;
        private static GameObject _waitPlayer_GO;

        public DonerUICom(GameObject downZone_GO)
        {
            _doner_Button = downZone_GO.transform.Find("DonerButton").GetComponent<Button>();

            _waitPlayer_GO = downZone_GO.transform.Find("WaitZone").gameObject;
        }

        public static void AddListener(UnityAction unityAction) => _doner_Button.onClick.AddListener(unityAction);
        public static void SetColor(Color color) => _doner_Button.image.color = color;

        public static void EnableWait() => _waitPlayer_GO.SetActive(true);
        public static void DisableWait() => _waitPlayer_GO.SetActive(false);
    }
}
