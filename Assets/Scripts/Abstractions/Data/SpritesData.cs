using UnityEngine;

[CreateAssetMenu(menuName = "Sprites", fileName = "Sprites")]
public class SpritesData : ScriptableObject
{
    [SerializeField] private Sprite _blackSprite;
    [SerializeField] private Sprite _whiteSprite;

    public Sprite BlackSprite => _blackSprite;
    public Sprite WhiteSprite => _whiteSprite;
}
