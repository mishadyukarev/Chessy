namespace Game.Game
{
    public struct PlayerWinnerC
    {
        public static PlayerTypes PlayerWinner { get; set; }
        public PlayerWinnerC(PlayerTypes playerType) => PlayerWinner = playerType;
    }
}