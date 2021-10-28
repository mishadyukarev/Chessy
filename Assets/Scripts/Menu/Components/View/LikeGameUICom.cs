using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Menu
{
    public struct LikeGameUICom
    {
        private static GameObject _likeGameZone_GO;
        private static Button _likeGame_But;
        private static Button _exit_Button;

        public LikeGameUICom(Transform centerZone_Trans)
        {
            _likeGameZone_GO = centerZone_Trans.Find("LikeGameZone").gameObject;
            _likeGame_But = _likeGameZone_GO.transform.Find("LikeGame_Button").GetComponent<Button>();
            _exit_Button = _likeGameZone_GO.transform.Find("Exit_Button").GetComponent<Button>();
        }

        public static void SetActiveZone(bool isActive) => _likeGameZone_GO.gameObject.SetActive(isActive);

        public static void AddListLikeGame_But(UnityAction unityAction) => _likeGame_But.onClick.AddListener(unityAction);
        public static void AddListenerExit_But(UnityAction unityAction) => _exit_Button.onClick.AddListener(unityAction);
    }
}