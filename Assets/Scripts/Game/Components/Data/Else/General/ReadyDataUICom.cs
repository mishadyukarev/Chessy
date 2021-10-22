using System.Collections.Generic;

namespace Scripts.Game
{
    internal struct ReadyDataUICom
    {
        internal bool IsStartedGame { get; set; }
        private Dictionary<bool, bool> _isActivatedButton;

        internal ReadyDataUICom(Dictionary<bool, bool> dict) : this()
        {
            _isActivatedButton = dict;

            dict.Add(true, default);
            dict.Add(false, default);
        }

        internal bool IsReady(bool key) => _isActivatedButton[key];
        internal void SetIsReady(bool key, bool value) => _isActivatedButton[key] = value;
    }
}
