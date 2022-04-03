using System.Collections.Generic;

namespace Chessy.Game.Model.Component
{
    public struct VisibleC
    {
        readonly Dictionary<PlayerTypes, bool> _isVisible;
        public bool IsVisible(in PlayerTypes player) => _isVisible[player];

        internal VisibleC(in Dictionary<PlayerTypes, bool> isVisibled)
        {
            _isVisible = new Dictionary<PlayerTypes, bool>();
            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                _isVisible.Add(playerT, false);
            }

            //_isVisibled = isVisibled;
        }
        internal void Set(in PlayerTypes playerT, in bool isVisible) => _isVisible[playerT] = isVisible;
    }
}