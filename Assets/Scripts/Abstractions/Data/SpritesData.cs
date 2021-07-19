using UnityEngine;

[CreateAssetMenu(menuName = "Sprites", fileName = "Sprites")]
public class SpritesData : ScriptableObject
{
    [SerializeField] private Sprite _black_Sprite;
    [SerializeField] private Sprite _white_Sprite;

    [Space(20)]
    [SerializeField] private Sprite _kingSprite;
    [SerializeField] private Sprite _pawnSprite;
    [SerializeField] private Sprite _pawnSwordSprite;
    [SerializeField] private Sprite _rookSprite;
    [SerializeField] private Sprite _rookCrossbowSprite;
    [SerializeField] private Sprite _bishopSprite;
    [SerializeField] private Sprite _bishopCrossbowSprite;

    [Space(20)]
    [SerializeField] private Sprite _citySprite;
    [SerializeField] private Sprite _farmSprite;
    [SerializeField] private Sprite _woodcutterSprite;
    [SerializeField] private Sprite _mineSprite;

    [Space(20)]
    [SerializeField] private Sprite _fertilizerSprite;
    [SerializeField] private Sprite _youngForestSprite;
    [SerializeField] private Sprite _forestSprite;
    [SerializeField] private Sprite _hillSprite;
    [SerializeField] private Sprite _mountainSprite;


    public Sprite BlackSprite => _black_Sprite;
    public Sprite WhiteSprite => _white_Sprite;

    public Sprite KingSprite => _kingSprite;
    public Sprite PawnSprite => _pawnSprite;
    public Sprite PawnSwordSprite => _pawnSwordSprite;
    public Sprite RookSprite => _rookSprite;
    public Sprite RookCrossbowSprite => _rookCrossbowSprite;
    public Sprite BishopSprite => _bishopSprite;
    public Sprite BishopCrossbowSprite => _bishopCrossbowSprite;

    public Sprite City => _citySprite;
    public Sprite Farm => _farmSprite;
    public Sprite Woodcutter => _woodcutterSprite;
    public Sprite Mine => _mineSprite;

    public Sprite Ferilizer => _fertilizerSprite;
    public Sprite YoungForest => _youngForestSprite;
    public Sprite Forest => _forestSprite;
    public Sprite Hill => _hillSprite;
    public Sprite Mountain => _mountainSprite;
}
