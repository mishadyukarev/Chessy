using Chessy.Common;
using UnityEngine;

namespace Chessy.Game
{
    public readonly struct EnergyUIE
    {
        public readonly AnimationVC AnimationC;
        public readonly ImageUIC ImageUIC;
        public readonly TextUIC TextUIC;

        public EnergyUIE(in AnimationVC animationC, in ImageUIC imageC, in TextUIC textC)
        {
            AnimationC = animationC;
            ImageUIC = imageC;
            TextUIC = textC;
        }
    }
}