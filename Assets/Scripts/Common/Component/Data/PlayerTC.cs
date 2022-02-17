namespace Game.Game
{
    public struct PlayerTC
    {
        public PlayerTypes Player;

        public bool Is(params PlayerTypes[] players)
        {
            if (players == default) throw new System.Exception();

            foreach (var player in players) if (player == Player) return true;
            return false;
        }

        public PlayerTypes NextPlayerFrom(PlayerTypes player)
        {
            if (player == PlayerTypes.First) return PlayerTypes.Second;
            else return PlayerTypes.First;
        }

        public PlayerTC(in PlayerTypes player) => Player = player;
    }
}
