internal class SupportManager
{
    protected BuilderManager _builderManager;
    protected UnityEvents _unityEvents;
    protected NameManager _nameManager;

    internal BuilderManager BuilderManager => _builderManager;
    internal UnityEvents UnityEvents => _unityEvents;
    internal NameManager NameManager => _nameManager;

    internal SupportManager()
    {
        _builderManager = new BuilderManager();
        _unityEvents = new UnityEvents(_builderManager);
        _nameManager = new NameManager();
    }
}
