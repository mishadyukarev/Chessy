using UnityEngine;

internal class ResourcesLoadGame : ResourcesLoad
{
    private GameObject _cellGO;
    private AudioClip _audioClip;
    private Sprite _whiteCellSprite;
    private Sprite _blackCellSprite;
    private StartValuesGameConfig _startValues;
    private GameObject _backGroundCollider2D;


    public GameObject CellGO => _cellGO;
    internal AudioClip AudioClip => _audioClip;
    internal Sprite WhiteCellSprite => _whiteCellSprite;
    internal Sprite BlackCellSprite => _blackCellSprite;
    internal StartValuesGameConfig StartValuesConfig => _startValues;
    internal GameObject BackGroundCollider2D => _backGroundCollider2D;


    public ResourcesLoadGame()
    {
        _cellGO = Resources.Load<GameObject>("CellPrefab");

        _whiteCellSprite = Resources.Load<SpritesConfig>("Sprites").WhiteSprite;
        _blackCellSprite = Resources.Load<SpritesConfig>("Sprites").BlackSprite;

        _startValues = Resources.Load<StartValuesGameConfig>("StartValues");

        _audioClip = Resources.Load<SoundConfig>("SoundConfig").AudioClip;

        _canvas = Resources.Load<Canvas>("CanvasGame");

        _backGroundCollider2D = Resources.Load<GameObject>("BackGround");
    }
}
