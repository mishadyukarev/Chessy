using UnityEngine;

internal sealed class MainMenu : Main
{
    private static MainMenu _instanceMenu;
    private SupportMenuManager _supportMenuManager;
    private StartSpawnMenuManager _startSpawnMenuManager;
    private MenuManager _menuManager;

    internal static MainMenu InstanceGame => _instanceMenu;


    private void Start()
    {
        _instanceMenu = this;

        _supportMenuManager = new SupportMenuManager();

        _startSpawnMenuManager = new StartSpawnMenuManager(_supportMenuManager);

        var go = _supportMenuManager.BuilderManager.CreateGameObject(nameof(MenuManager), new System.Type[] { typeof(MenuManager) });
        _menuManager = go.GetComponent<MenuManager>();
        _menuManager.Init(_startSpawnMenuManager);
    }
}
