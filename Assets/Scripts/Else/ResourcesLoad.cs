using UnityEngine;

internal class ResourcesLoad
{
    private Camera _camera;
    private Canvas _canvas;
    private GameObject _inMenuZoneGO;
    private GameObject _inGameZoneGO;

    private PrefabConfig _prefabConfig;
    private SoundConfig _soundConfig;
    private SpritesConfig _spritesConfig;
    private StartValuesGameConfig _startValuesConfig;

    internal Canvas Canvas => _canvas;

    internal GameObject InMenuZoneGO => _inMenuZoneGO;
    internal GameObject InGameZoneGO => _inGameZoneGO;

    internal PrefabConfig PrefabConfig => _prefabConfig;
    internal SoundConfig SoundConfig => _soundConfig;
    internal SpritesConfig SpritesConfig => _spritesConfig;
    internal StartValuesGameConfig StartValuesGameConfig => _startValuesConfig;


    internal ResourcesLoad()
    {
        _canvas = Resources.Load<Canvas>("Canvas");

        _inMenuZoneGO = _canvas.transform.Find("InMenuZone").gameObject;
        _inGameZoneGO = _canvas.transform.Find("InGameZone").gameObject;

        _prefabConfig = Resources.Load<PrefabConfig>("PrefabConfig");
        _soundConfig = Resources.Load<SoundConfig>("SoundConfig");
        _spritesConfig = Resources.Load<SpritesConfig>("Sprites");
        _startValuesConfig = Resources.Load<StartValuesGameConfig>("StartValues");
    }
}
