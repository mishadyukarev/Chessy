using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct ButtonUIC
    {
        public readonly Button Button;


        public GameObject GameObject => Button.gameObject;

        public Image Image
        {
            get => Button.image;
            set => Button.image = value;
        }

        public ButtonUIC(in Button button) => Button = button;

        public void AddListener(UnityAction action) => Button.onClick.AddListener(action);
        public void SetActive(in bool needActive) => Button.gameObject.SetActive(needActive);
        public void SetActiveParent(in bool needActive) => Button.transform.parent.gameObject.SetActive(needActive);

        public T AddComponent<T>() where T : Component => GameObject.AddComponent<T>();
    }
}