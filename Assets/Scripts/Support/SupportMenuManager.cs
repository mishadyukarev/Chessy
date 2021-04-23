internal class SupportMenuManager : SupportManager
{
    private ResourcesLoadMenuManager _resourcesLoadManager;

    public ResourcesLoadMenuManager ResourcesLoadMenuManager => _resourcesLoadManager;


    internal SupportMenuManager()
    {
        _resourcesLoadManager = new ResourcesLoadMenuManager();
    }
}
