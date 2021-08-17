using UnityEngine;

[CreateAssetMenu(menuName = "Sprites", fileName = "Sprites")]
public class SpritesData : ScriptableObject
{
    [SerializeField] private Sprite _blackCell_Sprite = default;
    [SerializeField] private Sprite _whiteCell_Sprite = default;

    [Space(20)]
    [SerializeField] private Sprite _kingSprite = default;
    [SerializeField] private Sprite _axe_Sprite = default;
    [SerializeField] private Sprite _hoe_Sprite = default;
    [SerializeField] private Sprite _pick_Sprite = default;
    [SerializeField] private Sprite _sword_Sprite = default;
    [SerializeField] private Sprite _bowRook_Sprite = default;
    [SerializeField] private Sprite _bowBishop_Sprite = default;

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


    public Sprite BlackCell_Sprite => _blackCell_Sprite;
    public Sprite WhiteCell_Sprite => _whiteCell_Sprite;


    public Sprite King_Sprite => _kingSprite;

    public Sprite Axe_Sprite => _axe_Sprite;
    public Sprite Hoe_Sprite => _hoe_Sprite;
    public Sprite Pick_Sprite => _pick_Sprite;
    public Sprite Sword_Sprite => _sword_Sprite;

    public Sprite BowRook_Sprite => _bowRook_Sprite;
    public Sprite BowBishop_Sprite => _bowBishop_Sprite;


    public Sprite City_Sprite => _citySprite;
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
