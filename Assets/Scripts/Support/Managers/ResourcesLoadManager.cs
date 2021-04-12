using UnityEngine;

public class ResourcesLoadManager
{
    private GameObject _cellGO;
    private AudioSource _audioSource;
    private SpriteRenderer _whiteCellSpriteRender;
    private SpriteRenderer _blackCellSpriteRender;

    public GameObject CellGO => _cellGO;
    public AudioSource AudioSource => _audioSource;
    internal SpriteRenderer WhiteCellSpriteRender => _whiteCellSpriteRender;
    internal SpriteRenderer BlackCellSpriteRender => _blackCellSpriteRender;


    public ResourcesLoadManager()
    {
        _cellGO = Resources.Load<GameObject>("CellPrefab");

        _audioSource = Resources.Load<GameObject>("SoundsDataPrefab").GetComponent<AudioSource>();

        _whiteCellSpriteRender = Resources.Load<GameObject>("White").GetComponent<SpriteRenderer>();
        _blackCellSpriteRender = Resources.Load<GameObject>("Black").GetComponent<SpriteRenderer>();
    }
}
