namespace Game.Game
{
    public struct PlayerTC : IUnitCellE, IBuildCell, IWhoseMoveE
    {
        public PlayerTypes Player;

        public bool Is(params PlayerTypes[] players)
        {
            foreach (var player in players) if (player == Player) return true;
            return false;
        }

        public PlayerTC(in PlayerTypes player) => Player = player;


        public void Set(PlayerTC playerC) => Player = playerC.Player;
        public void Reset() => Player = PlayerTypes.None;
    }
}
