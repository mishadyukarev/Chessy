using Photon.Realtime;

internal struct EndGameDataUIComponent
{
    internal bool IsEndGame { get; set; }
    internal bool IsOwnerWinner { get; set; }
    internal Player PlayerWinner { get; set; }
    internal bool IsBotWinner { get; set; }
}
