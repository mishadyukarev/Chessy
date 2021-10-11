using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Menu
{
    internal struct LikeGameZoneCom
    {
        private GameObject _likeGameZone_GO;
        private Button _exit_Button;

        internal LikeGameZoneCom(Transform centerZone_Trans)
        {
            _likeGameZone_GO = centerZone_Trans.Find("LikeGameZone").gameObject;
            _exit_Button = _likeGameZone_GO.transform.Find("Exit_Button").GetComponent<Button>();
        }

        internal void SetActiveZone(bool isActive) => _likeGameZone_GO.gameObject.SetActive(isActive);
        internal void AddListenerExit_But(UnityAction unityAction) => _exit_Button.onClick.AddListener(unityAction);
    }
}