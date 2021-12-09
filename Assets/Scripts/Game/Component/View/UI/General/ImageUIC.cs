using UnityEngine.UI;

namespace Game.Game
{
    public struct ImageUIC
    {
        internal readonly Image Image;

        internal ImageUIC(in Image image) => Image = image;
    }
}