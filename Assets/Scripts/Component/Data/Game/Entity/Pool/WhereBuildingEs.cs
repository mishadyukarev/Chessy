using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct WhereBuildingEs
    {
        readonly Dictionary<string, HaveBuildE> _builds;

        string Key(in BuildingTypes build, in PlayerTypes owner, in byte idx) => build.ToString() + owner + idx;

        public HaveBuildE HaveBuild(in BuildingTypes build, in PlayerTypes owner, in byte idx) => _builds[Key(build, owner, idx)];
        public HaveBuildE HaveBuild(in string key) => _builds[key];
        public HaveBuildE HaveBuild(in CellBuildingE cellBuildingE, in byte idx) => _builds[Key(cellBuildingE.BuildTC.Build, cellBuildingE.Owner.Player, idx)];
        public HaveBuildE HaveBuild(in CellBuildEs buildEs, in byte idx) => _builds[Key(buildEs.BuildingE(idx).BuildTC.Build, buildEs.BuildingE(idx).Owner.Player, idx)];

        public bool IsSetted(in BuildingTypes build, in PlayerTypes owner, out byte idx)
        {
            for (idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
            {
                if (HaveBuild(build, owner, idx).HaveBuilding.Have)
                {
                    return true;
                }
            }
            return false;
        }

        public HashSet<string> Keys
        {
            get
            {
                var hash = new HashSet<string>();
                foreach (var item in _builds) hash.Add(item.Key);
                return hash;
            }
        }

        public WhereBuildingEs(in EcsWorld gameW)
        {
            _builds = new Dictionary<string, HaveBuildE>();

            for (var build = BuildingTypes.None + 1; build < BuildingTypes.End; build++)
            {
                for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
                {
                    for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
                    {
                        _builds.Add(Key(build, player, idx), new HaveBuildE(gameW));
                    }
                }
            }
        }
    }
}