using UnityEngine;

[CreateAssetMenu(menuName = "Sprites", fileName = "Sprites")]
public class SpritesData : ScriptableObject
{
    [SerializeField] private Sprite _black_Sprite = default;
    [SerializeField] private Sprite _white_Sprite = default;

    [Space(20)]
    [SerializeField] private Sprite _kingSprite = default;
    [SerializeField] private Sprite _pawnMainAxe_Sprite = default;
    [SerializeField] private Sprite _hoe_Sprite = default;
    [SerializeField] private Sprite _pick_Sprite = default;
    [SerializeField] private Sprite _sword_Sprite = default;
    [SerializeField] private Sprite _rookSprite = default;
    [SerializeField] private Sprite _rookCrossbowSprite = default;
    [SerializeField] private Sprite _bishopSprite = default;
    [SerializeField] private Sprite _bishopCrossbowSprite = default;

    [Space(20)]
    [SerializeField] private Sprite _citySprite = default;
    [SerializeField] private Sprite _farmSprite = default;
    [SerializeField] private Sprite _woodcutterSprite = default;
    [SerializeField] private Sprite _mineSprite = default;

    [Space(5)]
    [SerializeField] private Sprite _cityBackSprite = default;
    [SerializeField] private Sprite _farmBackSprite = default;
    [SerializeField] private Sprite _woodcutterBackSprite = default;
    [SerializeField] private Sprite _mineBackSprite = default;

    [Space(20)]
    [SerializeField] private Sprite _fertilizerSprite = default;
    [SerializeField] private Sprite _youngForestSprite = default;
    [SerializeField] private Sprite _forestSprite = default;
    [SerializeField] private Sprite _hillSprite = default;
    [SerializeField] private Sprite _mountainSprite = default;


    public Sprite BlackSprite => _black_Sprite;
    public Sprite WhiteSprite => _white_Sprite;


    public Sprite KingSprite => _kingSprite;

    public Sprite CellAxe_Sprite => _pawnMainAxe_Sprite;
    public Sprite HoePawnExtra_Sprite => _hoe_Sprite;
    public Sprite PickPawnExtra_Sprite => _pick_Sprite;
    public Sprite SwordPawnExtra_Sprite => _sword_Sprite;

    public Sprite RookSprite => _rookSprite;
    public Sprite RookCrossbowSprite => _rookCrossbowSprite;
    public Sprite BishopSprite => _bishopSprite;
    public Sprite BishopCrossbowSprite => _bishopCrossbowSprite;


    public Sprite City => _citySprite;
    public Sprite Farm => _farmSprite;
    public Sprite Woodcutter => _woodcutterSprite;
    public Sprite Mine => _mineSprite;


    public Sprite CityBack => _cityBackSprite;
    public Sprite FarmBack => _farmBackSprite;
    public Sprite WoodcutterBack => _woodcutterBackSprite;
    public Sprite MineBack => _mineBackSprite;


    public Sprite Ferilizer => _fertilizerSprite;
    public Sprite YoungForest => _youngForestSprite;
    public Sprite Forest => _forestSprite;
    public Sprite Hill => _hillSprite;
    public Sprite Mountain => _mountainSprite;
}
