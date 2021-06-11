using UnityEngine;

[CreateAssetMenu(menuName = "SoundConfig", fileName = "SoundConfig")]
internal sealed class SoundConfig : ScriptableObject
{
    [SerializeField] private AudioClip _mistakeAudioClip;
    [SerializeField] private AudioClip _attackAudioClip;
    [SerializeField] private AudioClip _musicAudioClip;

    internal AudioClip MistakeAudioClip => _mistakeAudioClip;
    internal AudioClip AttackAudioClip => _attackAudioClip;
    internal AudioClip MusicAudioClip => _musicAudioClip;
}
