using UnityEditor;
using UnityEngine;

namespace Chessy.Game
{
    sealed class WorldCountsAmountBuildingInGameS : SystemAbstract, IEcsRunSystem
    {
        internal WorldCountsAmountBuildingInGameS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (var buildT = BuildingTypes.None + 1; buildT < BuildingTypes.End; buildT++)
            {
                E.BuildingsInfo(PlayerTypes.First, LevelTypes.First, buildT).IdxC.Clear();
                E.BuildingsInfo(PlayerTypes.Second, LevelTypes.First, buildT).IdxC.Clear();
            }

            for (byte idx_0 = 0; idx_0 < Start_VALUES.ALL_CELLS_AMOUNT; idx_0++)
            {
                if (E.BuildingTC(idx_0).HaveBuilding)
                {
                    E.BuildingsInfo(E.BuildingPlayerTC(idx_0).Player, E.BuildingLevelTC(idx_0).Level, E.BuildingTC(idx_0).Building).IdxC.Add(idx_0);
                }
            }
        }
    }
}