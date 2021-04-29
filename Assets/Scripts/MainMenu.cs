internal sealed class MainMenu : Main
{
    private static MainMenu _instanceMenu;
    private SupportMenuManager _supportMenuManager;
    private StartSpawnMenu _startSpawnMenuManager;
    private MenuManager _menuManager;

    internal static MainMenu InstanceGame => _instanceMenu;


    private void Start()
    {
        _instanceMenu = this;
        _supportMenuManager = new SupportMenuManager();

        _unityEvents = new UnityEvents(_supportMenuManager.Builder);

        _startSpawnMenuManager = new StartSpawnMenu(_supportMenuManager);

        var go = _supportMenuManager.Builder.CreateGameObject(nameof(MenuManager), new System.Type[] { typeof(MenuManager) });
        _menuManager = go.GetComponent<MenuManager>();
        _menuManager.Init(_startSpawnMenuManager);
    }
}
