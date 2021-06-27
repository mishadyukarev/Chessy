
using System;

internal struct StartedGameComponent : IDisposable
{
    internal bool IsStartedGame;

    public void Dispose()
    {
        IsStartedGame = false;
    }
}
