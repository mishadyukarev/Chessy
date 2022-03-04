using UnityEngine.UI;
using TMPro;

namespace Chessy.Game
{
    public struct RightUnitStatUIE
    {
        public ImageUIC ImageUIC;
        public TextUIC TextUIC;

        public RightUnitStatUIE(in Image image, in TextMeshProUGUI textMP)
        {
            ImageUIC = new ImageUIC(image);
            TextUIC = new TextUIC(textMP);
        }
    }
}