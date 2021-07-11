using Photon.Realtime;
using System;

internal struct EndGameComponent : IDisposable
{
    internal bool IsEndGame;

    internal Player PlayerWinner;

    public void Dispose()
    {
        IsEndGame = false;
        PlayerWinner = default;
    }
}
