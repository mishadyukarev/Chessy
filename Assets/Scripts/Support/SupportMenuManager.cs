internal class SupportMenuManager : SupportManager
{
    private ResourcesLoadMenu _resourcesLoadManager = new ResourcesLoadMenu();

    public ResourcesLoadMenu ResourcesLoadMenuManager => _resourcesLoadManager;
}
