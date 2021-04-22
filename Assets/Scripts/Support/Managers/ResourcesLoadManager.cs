using UnityEngine;

public class ResourcesLoadManager
{
    private GameObject _cellGO;
    private AudioSource _audioSource;
    private Sprite _whiteCellSprite;
    private Sprite _blackCellSprite;
    private StartValuesGameConfig _startValues;
    private Canvas _canvas;
    private GameObject _backGroundCollider2D;

    public GameObject CellGO => _cellGO;
    internal AudioClip AudioClip;
    public AudioSource AudioSource => _audioSource;
    internal Sprite WhiteCellSprite => _whiteCellSprite;
    internal Sprite BlackCellSprite => _blackCellSprite;
    internal StartValuesGameConfig StartValuesConfig => _startValues;
    internal Canvas Canvas => _canvas;
    internal GameObject BackGroundCollider2D => _backGroundCollider2D;
    internal Camera Camera;


    public ResourcesLoadManager()
    {
        _cellGO = Resources.Load<GameObject>("CellPrefab");

        _audioSource = Resources.Load<SoundConfig>("SoundConfig").AudioSource;

        _whiteCellSprite = Resources.Load<SpritesConfig>("Sprites").WhiteSprite;
        _blackCellSprite = Resources.Load<SpritesConfig>("Sprites").BlackSprite;

        _startValues = Resources.Load<StartValuesGameConfig>("StartValues");

        AudioClip = Resources.Load<AudioClip>("Clip1");

        _canvas = Resources.Load<Canvas>("Canvas");

        _backGroundCollider2D = Resources.Load<GameObject>("BackGround");
        Camera = Resources.Load<Camera>("Camera");
    }
}
