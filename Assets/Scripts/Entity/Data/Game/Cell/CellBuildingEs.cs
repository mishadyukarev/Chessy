using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellBuildingEs
    {
        readonly Dictionary<PlayerTypes, CellBuildingVisibleE> _owners;

        public readonly CellBuildingE BuildingE;
        public CellBuildingVisibleE BuildingVisE(in PlayerTypes player) => _owners[player];

        public CellBuildingEs(in CellEs cellEs, in EcsWorld gameW)
        {
            BuildingE = new CellBuildingE(cellEs, gameW);

            _owners = new Dictionary<PlayerTypes, CellBuildingVisibleE>();
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _owners.Add(player, new CellBuildingVisibleE(gameW));
            }
        }
    }
}