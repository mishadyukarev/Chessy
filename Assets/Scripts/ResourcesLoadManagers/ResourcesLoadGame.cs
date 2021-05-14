using UnityEngine;

internal class ResourcesLoadGame : ResourcesLoad
{
    private PrefabConfig _prefabConfig;
    private SoundConfig _soundConfig;
    private SpritesConfig _spritesConfig;
    private StartValuesGameConfig _startValuesConfig;


    internal PrefabConfig PrefabConfig => _prefabConfig;
    internal SoundConfig SoundConfig => _soundConfig;
    internal SpritesConfig SpritesConfig => _spritesConfig;
    internal StartValuesGameConfig StartValuesConfig => _startValuesConfig;


    public ResourcesLoadGame()
    {
        _prefabConfig = Resources.Load<PrefabConfig>("PrefabConfig");
        _soundConfig = Resources.Load<SoundConfig>("SoundConfig");
        _spritesConfig = Resources.Load<SpritesConfig>("Sprites");
        _startValuesConfig = Resources.Load<StartValuesGameConfig>("StartValues");

        _canvas = Resources.Load<Canvas>("CanvasGame");
    }
}
