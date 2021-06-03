using System;
using System.Collections.Generic;
using static Main;

internal abstract class EntitiesManager : IDisposable
{
    internal List<IDisposable> GameDisposables => Instance.GameDisposables;
    internal virtual void FillEntities()
    {

    }

    public virtual void Dispose()
    {

    }
}
