using UnityEngine;

[CreateAssetMenu(menuName = "SoundConfig", fileName = "SoundConfig")]
public sealed class SoundData : ScriptableObject
{
    [SerializeField] private AudioClip _mistakeAudioClip;
    [SerializeField] private AudioClip _attackAudioClip;
    [SerializeField] private AudioClip _musicAudioClip;

    public AudioClip MistakeAudioClip => _mistakeAudioClip;
    public AudioClip AttackAudioClip => _attackAudioClip;
    public AudioClip MusicAudioClip => _musicAudioClip;
}
