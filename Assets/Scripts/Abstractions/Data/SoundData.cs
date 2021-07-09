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
    [SerializeField] private AudioClip _buyAC;
    [SerializeField] private AudioClip _melting_clip;
    [SerializeField] private AudioClip _destroy_clip;
    [SerializeField] private AudioClip _upgradeUnitMelee_clip;
    [SerializeField] private AudioClip _seeding_clip;
    [SerializeField] private AudioClip _shiftUnit_clip;

    public AudioClip MistakeAudioClip => _mistakeAudioClip;
    public AudioClip AttackSwordAudioClip => _attackSwordAudioClip;
    public AudioClip AttackArcherAC => _attackArcherAC;
    public AudioClip MusicAudioClip => _musicAudioClip;
    public AudioClip PickArcherAudioClip => _pickArcherAudioClip;
    public AudioClip PickMeleeAC => _pickMeleeAC;
    public AudioClip BuildingAC => _buildingAC;
    public AudioClip FireAC => _fireAC;
    public AudioClip SettingUnitAC => _settingUnitAC;
    public AudioClip BuyAC => _buyAC;
    public AudioClip Melting_Clip => _melting_clip;
    public AudioClip Destroy_Clip => _destroy_clip;
    public AudioClip UpgradeUnitMelee_Clip => _upgradeUnitMelee_clip;
    public AudioClip Seeding_Clip => _seeding_clip;
    public AudioClip ShiftUnit_Clip => _shiftUnit_clip;
}
