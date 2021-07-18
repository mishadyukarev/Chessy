using UnityEngine;

[CreateAssetMenu(menuName = "Sprites", fileName = "Sprites")]
public class SpritesData : ScriptableObject
{
    [SerializeField] private Sprite _black_Sprite;
    [SerializeField] private Sprite _white_Sprite;

    public Sprite BlackSprite => _black_Sprite;
    public Sprite WhiteSprite => _white_Sprite;
}
