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
        public Color Color
        {
            get => Image.color;
            set => Image.color = value;
        }
        public float FillAmount
        {
            get => Image.fillAmount;
            set => Image.fillAmount = value;
        }
        public bool IsEnabled
        {
            get => Image.enabled;
            set => Image.enabled = value;
        }

        public ImageUIC(in Image image) => Image = image;

        public void SetActive(in bool needActive) => Image.gameObject.SetActive(needActive);
        public void SetActiveParent(in bool needActive) => Image.transform.parent.gameObject.SetActive(needActive);
    }
}