using System;


internal abstract class ObjectPool : IDisposable
{
    public virtual void Dispose()
    {

    }

    internal virtual void Spawn(ResourcesLoad resourcesLoad, Builder builder)
    {

    }
}
