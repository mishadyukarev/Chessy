using Photon.Realtime;
using System;

internal struct EndGameComponent
{
    internal bool IsEndGame;

    internal Player PlayerWinner;

    internal void StartFill()
    {
        IsEndGame = default;
        PlayerWinner = default;
    }
}
