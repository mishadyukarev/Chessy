using System;

namespace Chessy.Game
{
    public struct PlayerTC
    {
        public PlayerTypes Player;

        public bool Is(params PlayerTypes[] players)
        {
            if (players == default) throw new Exception();

            foreach (var player in players) if (player == Player) return true;
            return false;
        }

        public PlayerTC(in PlayerTypes player) => Player = player;
    }
}
