using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Game
{
    internal struct DonerUICom
    {
        private Button _doner_Button;
        private GameObject _waitPlayer_GO;

        internal DonerUICom(GameObject downZone_GO)
        {
            _doner_Button = downZone_GO.transform.Find("DonerButton").GetComponent<Button>();

            _waitPlayer_GO = downZone_GO.transform.Find("WaitZone").gameObject;
        }

        internal void AddListener(UnityAction unityAction) => _doner_Button.onClick.AddListener(unityAction);
        internal void SetColor(Color color) => _doner_Button.image.color = color;

        internal void EnableWait() => _waitPlayer_GO.SetActive(true);
        internal void DisableWait() => _waitPlayer_GO.SetActive(false);
    }
}
