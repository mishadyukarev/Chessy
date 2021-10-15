using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Menu
{
    internal struct LikeGameUICom
    {
        private GameObject _likeGameZone_GO;
        private Button _likeGame_But;
        private Button _exit_Button;

        internal LikeGameUICom(Transform centerZone_Trans)
        {
            _likeGameZone_GO = centerZone_Trans.Find("LikeGameZone").gameObject;
            _likeGame_But = _likeGameZone_GO.transform.Find("LikeGame_Button").GetComponent<Button>();
            _exit_Button = _likeGameZone_GO.transform.Find("Exit_Button").GetComponent<Button>();
        }

        internal void SetActiveZone(bool isActive) => _likeGameZone_GO.gameObject.SetActive(isActive);

        internal void AddListLikeGame_But(UnityAction unityAction) => _likeGame_But.onClick.AddListener(unityAction);
        internal void AddListenerExit_But(UnityAction unityAction) => _exit_Button.onClick.AddListener(unityAction);
    }
}