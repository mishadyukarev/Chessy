using Chessy.Common;
using UnityEngine;

namespace Chessy.Game
{
    public readonly struct EnergyUIE
    {
        public readonly AnimationVC AnimationC;
        public readonly ImageUIC ImageUIC;
        public readonly TextUIC TextUIC;
        internal readonly ButtonUIC ButtonC;

        public EnergyUIE(in AnimationVC animationC, in ImageUIC imageC, in TextUIC textC, in ButtonUIC buttonC)
        {
            AnimationC = animationC;
            ImageUIC = imageC;
            TextUIC = textC;
            ButtonC = buttonC;
        }
    }
}