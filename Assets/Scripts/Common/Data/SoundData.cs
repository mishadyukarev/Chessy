using UnityEngine;

[CreateAssetMenu(menuName = "SoundConfig", fileName = "SoundConfig")]
public sealed class SoundData : ScriptableObject
{
    [SerializeField] private AudioClip _mistakeAudioClip = default;
    [SerializeField] private AudioClip _attackSwordAudioClip = default;
    [SerializeField] private AudioClip _attackArcherAC = default;
    [SerializeField] private AudioClip _musicAudioClip = default;
    [SerializeField] private AudioClip _pickArcherAudioClip = default;
    [SerializeField] private AudioClip _pickMeleeAC = default;
    [SerializeField] private AudioClip _buildingAC = default;
    [SerializeField] private AudioClip _fireAC = default;
    [SerializeField] private AudioClip _settingUnitAC = default;
    [SerializeField] private AudioClip _buyAC = default;
    [SerializeField] private AudioClip _melting_clip = default;
    [SerializeField] private AudioClip _destroy_clip = default;
    [SerializeField] private AudioClip _upgradeUnitMelee_clip = default;
    [SerializeField] private AudioClip _seeding_clip = default;
    [SerializeField] private AudioClip _shiftUnit_clip = default;
    [SerializeField] private AudioClip _truce_clip = default;
    [SerializeField] private AudioClip _clickToTable_clip = default;

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
    public AudioClip Truce_Clip => _truce_clip;
    public AudioClip ClickToTable_Clip => _clickToTable_clip;
}
