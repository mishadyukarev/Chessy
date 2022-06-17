using UnityEngine.UI;
using TMPro;

namespace Chessy.Game
{
    readonly struct RightUnitStatUIE
    {
        internal readonly ImageUIC ImageC;
        internal readonly TextUIC TextC;

        internal RightUnitStatUIE(in Image image, in TextMeshProUGUI textMP)
        {
            ImageC = new ImageUIC(image);
            TextC = new TextUIC(textMP);
        }
    }
}