using UnityEngine;

public class ResourcesLoadManager
{
    private GameObject _cellGO;
    private AudioSource _audioSource;
    private Sprite _whiteCellSprite;
    private Sprite _blackCellSprite;
    private StartValuesConfig _startValues;

    public GameObject CellGO => _cellGO;
    public AudioSource AudioSource => _audioSource;
    internal Sprite WhiteCellSprite => _whiteCellSprite;
    internal Sprite BlackCellSprite => _blackCellSprite;
    internal StartValuesConfig StartValuesConfig => _startValues;


    public ResourcesLoadManager()
    {
        _cellGO = Resources.Load<GameObject>("CellPrefab");

        _audioSource = Resources.Load<SoundConfig>("SoundConfig").AudioSource;

        _whiteCellSprite = Resources.Load<SpritesConfig>("Sprites").WhiteSprite;
        _blackCellSprite = Resources.Load<SpritesConfig>("Sprites").BlackSprite;

        _startValues = Resources.Load<StartValuesConfig>("StartValues");
    }
}
