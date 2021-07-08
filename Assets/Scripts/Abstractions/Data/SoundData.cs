using UnityEngine;

[CreateAssetMenu(menuName = "SoundConfig", fileName = "SoundConfig")]
public sealed class SoundData : ScriptableObject
{
    [SerializeField] private AudioClip _mistakeAudioClip;
    [SerializeField] private AudioClip _attackSwordAudioClip;
    [SerializeField] private AudioClip _attackArcherAC;
    [SerializeField] private AudioClip _musicAudioClip;
    [SerializeField] private AudioClip _pickArcherAudioClip;
    [SerializeField] private AudioClip _pickMeleeAC;
    [SerializeField] private AudioClip _buildingAC;
    [SerializeField] private AudioClip _fireAC;
    [SerializeField] private AudioClip _settingUnitAC;

    public AudioClip MistakeAudioClip => _mistakeAudioClip;
    public AudioClip AttackSwordAudioClip => _attackSwordAudioClip;
    public AudioClip AttackArcherAC => _attackArcherAC;
    public AudioClip MusicAudioClip => _musicAudioClip;
    public AudioClip PickArcherAudioClip => _pickArcherAudioClip;
    public AudioClip PickMeleeAC => _pickMeleeAC;
    public AudioClip BuildingAC => _buildingAC;
    public AudioClip FireAC => _fireAC;
    public AudioClip SettingUnitAC => _settingUnitAC;
}
