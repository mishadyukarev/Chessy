using TMPro;
using UnityEngine.UI;

namespace Chessy.Model
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