using Photon.Realtime;

internal struct EndGameComponent
{
    internal bool IsEndGame { get; set; }

    internal Player PlayerWinner { get; set; }
}
