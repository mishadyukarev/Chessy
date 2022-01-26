using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellBuildEs
    {
        static CellBuildingE[] _builds;
        static Dictionary<PlayerTypes, CellBuildingVisibleE[]> _owners;

        public static CellBuildingE Build(in byte idx) => _builds[idx];
        public static CellBuildingVisibleE IsVisible(in PlayerTypes player, in byte idx) => _owners[player][idx];


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

        public static void SetNew(in BuildingTypes build, in PlayerTypes owner, in byte idx)
        {
            if (build == default) throw new Exception("BuildType is None");
            if (Build(idx).BuildTC.Is(build)) throw new Exception("It's got yet");
            if (Build(idx).BuildTC.Have) Remove(idx);

            Build(idx).BuildTC.Build = build;
            Build(idx).PlayerTC.Player = owner;
            WhereBuildsE.HaveBuild<HaveBuildingC>(build, owner, idx).Have = true;
        }

        public static void Remove(in byte idx)
        {
            if (Build(idx).BuildTC.Have)
            {
                WhereBuildsE.HaveBuild<HaveBuildingC>(Build(idx).BuildTC.Build, Build(idx).PlayerTC.Player, idx).Have = false;

                Build(idx).BuildTC.Reset();
                Build(idx).PlayerTC.Reset();
            }
        }

        public static bool CanBuild(in byte idx, in BuildingTypes build, in PlayerTypes who, out MistakeTypes mistake)
        {
            mistake = default;

            var buildC = Build(idx).BuildTC;


            if (CellUnitEs.Step(idx).AmountC.Amount >= CellUnitStepValues.NeedSteps(build))
            {
                if (!buildC.Have || buildC.Is(BuildingTypes.Camp))
                {
                    if (!CellEnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx).Resources.Have)
                    {
                        return true;
                    }
                    else
                    {
                        mistake = MistakeTypes.NeedOtherPlace;
                        return false;
                    }
                }
                else
                {
                    mistake = MistakeTypes.NeedOtherPlace;
                    return false;
                }
            }
            else
            {
                mistake = MistakeTypes.NeedMoreSteps;
                return false;
            }
        }

        public static bool CanExtract(in byte idx, out int extract, out EnvironmentTypes env, out ResourceTypes res)
        {
            var buildC = Build(idx).BuildTC;
            var ownC = Build(idx).PlayerTC;


            if (Build(idx).BuildTC.Is(BuildingTypes.Farm) && CellEnvironmentEs.Environment(EnvironmentTypes.Fertilizer, idx).Resources.Have)
            {
                env = EnvironmentTypes.Fertilizer;
                res = ResourceTypes.Food;
            }
            else if (Build(idx).BuildTC.Is(BuildingTypes.Woodcutter) && CellEnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx).Resources.Have)
            {
                env = EnvironmentTypes.AdultForest;
                res = ResourceTypes.Wood;
            }
            else if (Build(idx).BuildTC.Is(BuildingTypes.Mine) && CellEnvironmentEs.Environment(EnvironmentTypes.Hill, idx).Resources.Have)
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


            if (BuildingUpgradesEs.HaveUpgrade<HaveUpgradeC>(buildC.Build, ownC.Player, UpgradeTypes.PickCenter).Have)
            {
                extract += (int)(extract * 0.5f);
            }


            if (extract > CellEnvironmentEs.Environment(env, idx).Resources.Amount) extract = CellEnvironmentEs.Environment(env, idx).Resources.Amount;

            return true;
        }
    }
}