using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Game
{
    public struct UniqThirdButViewC
    {
        private static Button _button;

        public UniqThirdButViewC(Transform parent)
        {
            _button = parent.Find("Third").GetComponent<Button>();
        }

        public static void AddListener(UnityAction action) => _button.onClick.AddListener(action);
    }
}