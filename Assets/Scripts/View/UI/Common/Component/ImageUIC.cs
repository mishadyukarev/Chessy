using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct ImageUIC
    {
        public Image Image;

        public Sprite Sprite
        {
            get => Image.sprite;
            set => Image.sprite = value;
        }
        public GameObject GameObject => Image.gameObject;
        public Transform Transform => Image.transform;

        public ImageUIC(in Image image) => Image = image;

        public void SetActive(in bool needActive)
        {
            if (needActive != GameObject.activeSelf) 
                GameObject.SetActive(needActive);
        }
        public void SetActiveParent(in bool needActive) => Transform.parent.gameObject.SetActive(needActive);
    }
}