using UnityEngine;

internal class ResourcesLoad
{
    private Camera _camera;
    private Canvas _canvas;

    private PrefabConfig _prefabConfig;
    private SoundConfig _soundConfig;
    private SpritesConfig _spritesConfig;
    private StartValuesGameConfig _startValuesConfig;

    internal Camera Camera => _camera;
    internal Canvas Canvas => _canvas;

    internal PrefabConfig PrefabConfig => _prefabConfig;
    internal SoundConfig SoundConfig => _soundConfig;
    internal SpritesConfig SpritesConfig => _spritesConfig;
    internal StartValuesGameConfig StartValuesGameConfig => _startValuesConfig;


    internal ResourcesLoad()
    {
        _camera = Resources.Load<Camera>("Camera");
        _canvas = Resources.Load<Canvas>("Canvas");

        _prefabConfig = Resources.Load<PrefabConfig>("PrefabConfig");
        _soundConfig = Resources.Load<SoundConfig>("SoundConfig");
        _spritesConfig = Resources.Load<SpritesConfig>("Sprites");
        _startValuesConfig = Resources.Load<StartValuesGameConfig>("StartValues");
    }
}
