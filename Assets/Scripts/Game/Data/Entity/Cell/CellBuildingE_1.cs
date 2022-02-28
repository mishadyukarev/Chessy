using System.Collections.Generic;

namespace Chessy.Game
{
    public struct CellBuildingEs
    {
        readonly Dictionary<PlayerTypes, bool> _isVisibled;

        public CellBuildingMainE MainE;
        public CellBuildingExtractE ExtractE;

        public bool IsVisible(in PlayerTypes player) => _isVisibled[player];


        public CellBuildingEs(in byte types) : this()
        {
            _isVisibled = new Dictionary<PlayerTypes, bool>();
            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                _isVisibled.Add(playerT, default);
            }
        }

        public void SetVisible(in PlayerTypes player, in bool isVisibled) => _isVisibled[player] = isVisibled;
    }
}