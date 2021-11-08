using System.Collections.Generic;

namespace Chessy.Game
{
    public struct ReadyDataUIC
    {
        private static Dictionary<PlayerTypes, bool> _isActivatedButton;

        public static bool IsStartedGame { get; set; }


        public ReadyDataUIC(Dictionary<PlayerTypes, bool> dict) : this()
        {
            _isActivatedButton = dict;

            dict.Add(PlayerTypes.First, false);
            dict.Add(PlayerTypes.Second, false);

            IsStartedGame = false;
        }

        public static bool IsReady(PlayerTypes player) => _isActivatedButton[player];
        public static void SetIsReady(PlayerTypes player, bool value) => _isActivatedButton[player] = value;
    }
}
