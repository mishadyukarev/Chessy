using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellBuildEs
    {
        readonly Dictionary<PlayerTypes, CellBuildingVisibleE> _owners;

        public readonly CellBuildingE BuildingE;
        public CellBuildingVisibleE BuildingVisE(in PlayerTypes player) => _owners[player];

        public CellBuildEs(in byte idx, in EcsWorld gameW)
        {
            BuildingE = new CellBuildingE(idx, gameW);

            _owners = new Dictionary<PlayerTypes, CellBuildingVisibleE>();
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _owners.Add(player, new CellBuildingVisibleE(gameW));
            }
        }
    }
}