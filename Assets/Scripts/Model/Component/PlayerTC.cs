using System;

namespace Chessy.Game
{
    public struct PlayerTC
    {
        public PlayerTypes PlayerT { get; internal set; }

        public bool Is(params PlayerTypes[] players)
        {
            if (players == default) throw new Exception();

            foreach (var player in players) if (player == PlayerT) return true;
            return false;
        }
    }
}
