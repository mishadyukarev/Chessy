using Chessy.View.Component;
using Chessy.View.UI.Component;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.View.UI.Entity
{
    public readonly struct BookLittleUIE
    {
        public readonly ButtonUIC ButtonC;
        public readonly AnimationVC AnimationVC;

        public BookLittleUIE(in Button button, in Animation animation)
        {
            ButtonC = new ButtonUIC(button);
            AnimationVC = new AnimationVC(animation);
        }
    }
}