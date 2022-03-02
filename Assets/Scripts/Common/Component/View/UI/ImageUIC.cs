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

        public ImageUIC(in Image image) => Image = image;

        public void SetActive(in bool needActive) => Image.gameObject.SetActive(needActive);
        public void SetActiveParent(in bool needActive) => Image.transform.parent.gameObject.SetActive(needActive);
    }
}