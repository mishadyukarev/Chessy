using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct RightEffectUIE
    {
        public GameObjectVC GO;
        public ImageUIC ImageUIC;

        internal RightEffectUIE(in Transform rightEffectT, in byte number)
        {
            var trans = rightEffectT.Find("Effect_" + number.ToString());

            GO = new GameObjectVC(trans.gameObject);
            ImageUIC = new ImageUIC(trans.Find("Image").GetComponent<Image>());
        }
    }
}