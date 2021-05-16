using UnityEngine;

[CreateAssetMenu(menuName = "SoundConfig", fileName = "SoundConfig")]
internal class SoundConfig : ScriptableObject
{
    [SerializeField] internal AudioClip MistakeAudioClip = default;
    [SerializeField] internal AudioClip AttackAudioClip = default;
}
