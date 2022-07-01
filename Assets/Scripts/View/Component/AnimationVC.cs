using UnityEngine;

namespace Chessy.View.Component
{
    public struct AnimationVC
    {
        public readonly Animation Animation;

        public AnimationVC(in Animation animation) => Animation = animation;

        public void Play() => Animation.Play();
    }
}