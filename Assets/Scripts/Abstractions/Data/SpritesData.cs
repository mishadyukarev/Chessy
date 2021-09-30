using Assets.Scripts.Abstractions.Enums;
using System;
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
    [SerializeField] private Sprite _crossbowRook_Sprite = default;
    [SerializeField] private Sprite _crossbowBishop_Sprite = default;

    [Space(20)]
    [SerializeField] private Sprite _citySprite = default;
    [SerializeField] private Sprite _noneCitySprite = default;
    [SerializeField] private Sprite _farmSprite = default;
    [SerializeField] private Sprite _woodcutterSprite = default;
    [SerializeField] private Sprite _mineSprite = default;

    [Space(5)]
    [SerializeField] private Sprite _cityBackSprite = default;
    [SerializeField] private Sprite _farmBackSprite = default;
    [SerializeField] private Sprite _woodcutterBackSprite = default;
    [SerializeField] private Sprite _mineBackSprite = default;

    [Space(20)]
    [SerializeField] private Sprite _youngForest_Sprite = default;

    [Space(20)]
    [SerializeField] private Sprite _fire_Sprite = default;
    [SerializeField] private Sprite _noneFire_Sprite = default;

    [Space(20)]
    [SerializeField] private Sprite _circularAttack_Sprite = default;


    public Sprite GetSprite(SpriteGameTypes spriteGameType)
    {
        switch (spriteGameType)
        {
            case SpriteGameTypes.None:
                throw new Exception();

            case SpriteGameTypes.YoungForest:
                return _youngForest_Sprite;

            case SpriteGameTypes.Fire:
                return _fire_Sprite;

            case SpriteGameTypes.NoneFire:
                return _noneFire_Sprite;

            case SpriteGameTypes.CircularAttack:
                return _circularAttack_Sprite;

            case SpriteGameTypes.City:
                return _citySprite;

            case SpriteGameTypes.NoneCity:
                return _noneCitySprite;

            default:
                throw new Exception();
        }
    }



    public Sprite BlackCell_Sprite => _blackCell_Sprite;
    public Sprite WhiteCell_Sprite => _whiteCell_Sprite;


    public Sprite King_Sprite => _kingSprite;

    public Sprite Axe_Sprite => _axe_Sprite;
    public Sprite Hoe_Sprite => _hoe_Sprite;
    public Sprite Pick_Sprite => _pick_Sprite;
    public Sprite Sword_Sprite => _sword_Sprite;

    public Sprite BowRook_Sprite => _bowRook_Sprite;
    public Sprite BowBishop_Sprite => _bowBishop_Sprite;
    public Sprite CrossbowRook_Sprite => _crossbowRook_Sprite;
    public Sprite CrossbowBishop_Sprite => _crossbowBishop_Sprite;


    public Sprite City_Sprite => _citySprite;
    public Sprite Farm => _farmSprite;
    public Sprite Woodcutter => _woodcutterSprite;
    public Sprite Mine => _mineSprite;


    public Sprite CityBack => _cityBackSprite;
    public Sprite FarmBack => _farmBackSprite;
    public Sprite WoodcutterBack => _woodcutterBackSprite;
    public Sprite MineBack => _mineBackSprite;
}
