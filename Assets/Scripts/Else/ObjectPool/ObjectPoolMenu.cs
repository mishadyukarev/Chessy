using static Main;

internal class ObjectPoolMenu : ObjectPool
{
    internal override void Spawn(ResourcesLoad resourcesLoad, Builder builder)
    {
        base.Spawn(resourcesLoad, builder);
        Instance.MenuDisposables.Add(this);
    }
}
