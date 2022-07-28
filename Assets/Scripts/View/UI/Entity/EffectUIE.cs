using Chessy.View.Component;
using Chessy.View.UI.Component;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.View.UI.Entity
{
    readonly struct EffectUIE
    {
        internal readonly GameObjectVC GO;
        internal readonly ImageUIC ImageC;
        internal readonly ButtonUIC ButtonC;

        internal EffectUIE(in Transform rightEffectT, in byte number)
        {
            var trans = rightEffectT.Find("Effect_" + number.ToString());

            GO = new GameObjectVC(trans.gameObject);
            ImageC = new ImageUIC(trans.Find("Image").GetComponent<Image>());
            ButtonC = new ButtonUIC(trans.Find("Button+").GetComponent<Button>());
        }
    }
}