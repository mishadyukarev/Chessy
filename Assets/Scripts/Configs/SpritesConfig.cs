using UnityEngine;

[CreateAssetMenu(menuName = "Sprites", fileName = "Sprites")]
internal class SpritesConfig : ScriptableObject
{
    [SerializeField] internal Sprite BlackSprite = default;
    [SerializeField] internal Sprite WhiteSprite = default;
}
