using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct CellBuildEs
    {
        CellBuildingE[] _builds;
        Dictionary<PlayerTypes, CellBuildingVisibleE[]> _owners;

        public CellBuildingE Build(in byte idx) => _builds[idx];
        public CellBuildingVisibleE IsVisible(in PlayerTypes player, in byte idx) => _owners[player][idx];


        public CellBuildEs(in EcsWorld gameW)
        {
            var cells = CellStartValues.ALL_CELLS_AMOUNT;

            _builds = new CellBuildingE[cells];
            _owners = new Dictionary<PlayerTypes, CellBuildingVisibleE[]>();

            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                _owners.Add(player, new CellBuildingVisibleE[cells]);
            }

            for (byte idx = 0; idx < _builds.Length; idx++)
            {
                _builds[idx] = new CellBuildingE(gameW);

                foreach (var item in _owners) _owners[item.Key][idx] = new CellBuildingVisibleE(gameW);
            }
        }

        public bool CanExtract(in byte idx, out int extract, out EnvironmentTypes env, out ResourceTypes res)
        {
            var buildC = Build(idx).BuildTC;
            var ownC = Build(idx).PlayerTC;


            if (Build(idx).BuildTC.Is(BuildingTypes.Farm) && Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Fertilizer, idx).Resources.Have)
            {
                env = EnvironmentTypes.Fertilizer;
                res = ResourceTypes.Food;
            }
            else if (Build(idx).BuildTC.Is(BuildingTypes.Woodcutter) && Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx).Resources.Have)
            {
                env = EnvironmentTypes.AdultForest;
                res = ResourceTypes.Wood;
            }
            else if (Build(idx).BuildTC.Is(BuildingTypes.Mine) && Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Hill, idx).Resources.Have)
            {
                env = EnvironmentTypes.Hill;
                res = ResourceTypes.Ore;
            }
            else
            {
                extract = default;
                env = default;
                res = default;

                return false;
            }



            extract = 10;


            if (Entities.HaveUpgrade(buildC.Build, ownC.Player, UpgradeTypes.PickCenter).HaveUpgrade.Have)
            {
                extract += (int)(extract * 0.5f);
            }


            if (extract > Entities.CellEs.EnvironmentEs.Environment(env, idx).Resources.Amount) extract = Entities.CellEs.EnvironmentEs.Environment(env, idx).Resources.Amount;

            return true;
        }
    }
}