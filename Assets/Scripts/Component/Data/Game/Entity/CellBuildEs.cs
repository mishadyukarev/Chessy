using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellBuildEs
    {
        readonly CellBuildingE[] _builds;
        readonly Dictionary<PlayerTypes, CellBuildingVisibleE[]> _owners;

        public CellBuildingE BuildingE(in byte idx) => _builds[idx];
        public CellBuildingVisibleE BuildingE(in PlayerTypes player, in byte idx) => _owners[player][idx];

        public CellBuildEs(in EcsWorld gameW)
        {
            var cells = CellStartValues.ALL_CELLS_AMOUNT;

            _builds = new CellBuildingE[cells];
            _owners = new Dictionary<PlayerTypes, CellBuildingVisibleE[]>();

            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _owners.Add(player, new CellBuildingVisibleE[cells]);
            }

            for (byte idx = 0; idx < _builds.Length; idx++)
            {
                _builds[idx] = new CellBuildingE(idx, gameW);

                foreach (var item in _owners) _owners[item.Key][idx] = new CellBuildingVisibleE(gameW);
            }
        }
    }
}