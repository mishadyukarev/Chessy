using UnityEngine;

[CreateAssetMenu(menuName = "SoundConfig", fileName = "SoundConfig")]
internal class SoundConfig : ScriptableObject
{
    [SerializeField] internal AudioSource AudioSource = default;
}
