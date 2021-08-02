using UnityEngine;

namespace Assets.Scripts.ECS.Game.Components
{
    internal struct AudioSourceComponent
    {
        internal AudioSource AudioSource { get; private set; }

        internal AudioSourceComponent(AudioSource audioSource) => AudioSource = audioSource;
    }
}
