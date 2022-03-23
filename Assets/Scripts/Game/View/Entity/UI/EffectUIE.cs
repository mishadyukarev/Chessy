using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game.Entity.View.UI.Right
{
    public struct EffectUIE
    {
        public Chessy.Common.Component.GameObjectVC GO;
        public ImageUIC ImageUIC;

        internal EffectUIE(in Transform rightEffectT, in byte number)
        {
            var trans = rightEffectT.Find("Effect_" + number.ToString());

            GO = new Chessy.Common.Component.GameObjectVC(trans.gameObject);
            ImageUIC = new ImageUIC(trans.Find("Image").GetComponent<Image>());
        }
    }
}