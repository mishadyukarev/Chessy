using UnityEngine;

[CreateAssetMenu(menuName = "SoundConfig", fileName = "SoundConfig")]
public sealed class SoundData : ScriptableObject
{
    [SerializeField] private AudioClip _mistakeAudioClip;
    [SerializeField] private AudioClip _attackSwordAudioClip;
    [SerializeField] private AudioClip _musicAudioClip;
    [SerializeField] private AudioClip _pickArcherAudioClip;

    public AudioClip MistakeAudioClip => _mistakeAudioClip;
    public AudioClip AttackSwordAudioClip => _attackSwordAudioClip;
    public AudioClip MusicAudioClip => _musicAudioClip;
    public AudioClip PickArcherAudioClip => _pickArcherAudioClip;
}
