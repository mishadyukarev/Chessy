namespace Chessy.Game
{
    public struct PlyerWinnerC
    {
        public static PlayerTypes PlayerWinner { get; set; }
        public PlyerWinnerC(PlayerTypes playerType) => PlayerWinner = playerType;
    }
}