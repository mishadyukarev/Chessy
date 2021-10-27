using System.Collections.Generic;

namespace Scripts.Game
{
    internal struct ReadyDataUIC
    {
        private static Dictionary<bool, bool> _isActivatedButton;

        internal static bool IsStartedGame { get; set; }


        internal ReadyDataUIC(Dictionary<bool, bool> dict) : this()
        {
            _isActivatedButton = dict;

            dict.Add(true, default);
            dict.Add(false, default);
        }

        internal static bool IsReady(bool key) => _isActivatedButton[key];
        internal static void SetIsReady(bool key, bool value) => _isActivatedButton[key] = value;
    }
}
