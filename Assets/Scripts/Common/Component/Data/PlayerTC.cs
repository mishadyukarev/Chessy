namespace Game.Game
{
    public class PlayerTC
    {
        public PlayerTypes Player;

        public bool Is(params PlayerTypes[] players)
        {
            if (players == default) throw new System.Exception();

            foreach (var player in players) if (player == Player) return true;
            return false;
        }

        public PlayerTC() { }
        public PlayerTC(in PlayerTypes player) => Player = player;
    }
}
