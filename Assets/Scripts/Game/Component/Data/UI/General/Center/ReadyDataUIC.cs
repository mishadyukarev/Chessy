using System.Collections.Generic;

namespace Scripts.Game
{
    public struct ReadyDataUIC
    {
        private static Dictionary<bool, bool> _isActivatedButton;

        public static bool IsStartedGame { get; set; }


        public ReadyDataUIC(Dictionary<bool, bool> dict) : this()
        {
            _isActivatedButton = dict;

            dict.Add(true, default);
            dict.Add(false, default);
        }

        public static bool IsReady(bool key) => _isActivatedButton[key];
        public static void SetIsReady(bool key, bool value) => _isActivatedButton[key] = value;
    }
}
