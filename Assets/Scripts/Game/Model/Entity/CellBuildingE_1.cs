using Chessy.Game.Entity.Model.Cell;
using System.Collections.Generic;

namespace Chessy.Game
{
    public sealed class CellBuildingEs
    {
        readonly Dictionary<PlayerTypes, bool> _isVisibled = new Dictionary<PlayerTypes, bool>();

        public readonly BuildingE MainE = new BuildingE();
        public readonly CellBuildingExtractE ExtractE = new CellBuildingExtractE();

        public bool IsVisible(in PlayerTypes player) => _isVisibled[player];


        public CellBuildingEs()
        {
            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                _isVisibled.Add(playerT, default);
        }

        public void SetVisible(in PlayerTypes player, in bool isVisibled) => _isVisibled[player] = isVisibled;
    }
}