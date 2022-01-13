namespace Game.Game
{
    public struct PlayerC : IUnitCellE, IBuildCell
    {
        public PlayerTypes Player;

        public bool Is(params PlayerTypes[] players)
        {
            foreach (var player in players) if (player == Player) return true;
            return false;
        }

        public PlayerC(in PlayerTypes player) => Player = player;


        public void Set(PlayerC playerC) => Player = playerC.Player;
        public void Reset() => Player = PlayerTypes.None;
    }
}
