namespace Scripts.Game
{
    public struct EndGameDataUIC
    {
        public static PlayerTypes PlayerWinner { get; set; }
        public EndGameDataUIC(PlayerTypes playerType) => PlayerWinner = playerType;
    }
}