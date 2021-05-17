internal sealed class MainMenu : Main
{
    private static MainMenu _instanceMenu;
    private ResourcesLoadMenu _resourcesLoadManager;
    private StartSpawnMenu _startSpawnMenuManager;
    private MenuManager _menuManager;
    private Builder _builder;
    private Names _names;



    internal static MainMenu InstanceGame => _instanceMenu;


    private void Start()
    {
        _instanceMenu = this;
        _builder = new Builder();
        _names = new Names();
        _resourcesLoadManager = new ResourcesLoadMenu();


        _unityEvents = new UnityEvents(_builder);

        _startSpawnMenuManager = new StartSpawnMenu(_resourcesLoadManager);

        var go = _builder.CreateGameObject(nameof(MenuManager), new System.Type[] { typeof(MenuManager) });
        _menuManager = go.GetComponent<MenuManager>();
        _menuManager.Init(_startSpawnMenuManager);
    }
}
