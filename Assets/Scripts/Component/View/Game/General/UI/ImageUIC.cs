using UnityEngine.UI;

namespace Game.Game
{
    public struct ImageUIC
    {
        public readonly Image Image;

        public ImageUIC(in Image image) => Image = image;
    }
}