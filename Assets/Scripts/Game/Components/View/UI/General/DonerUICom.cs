using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Game
{
    public struct DonerUICom
    {
        private Button _doner_Button;
        private GameObject _waitPlayer_GO;

        public DonerUICom(GameObject downZone_GO)
        {
            _doner_Button = downZone_GO.transform.Find("DonerButton").GetComponent<Button>();

            _waitPlayer_GO = downZone_GO.transform.Find("WaitZone").gameObject;
        }

        public void AddListener(UnityAction unityAction) => _doner_Button.onClick.AddListener(unityAction);
        public void SetColor(Color color) => _doner_Button.image.color = color;

        public void EnableWait() => _waitPlayer_GO.SetActive(true);
        public void DisableWait() => _waitPlayer_GO.SetActive(false);
    }
}
