using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellBuildE
    {
        static Entity[] _builds;

        static Dictionary<PlayerTypes, Entity[]> _buildEnts;

        public static ref T Build<T>(in byte idx) where T : struct, IBuildCell => ref _builds[idx].Get<T>();
        public static ref T IsVisible<T>(in PlayerTypes player, in byte idx) where T : struct, IBuildPlayerCellE => ref _buildEnts[player][idx].Get<T>();


        public CellBuildE(in EcsWorld gameW)
        {
            _builds = new Entity[CellStartValues.ALL_CELLS_AMOUNT];
            _buildEnts = new Dictionary<PlayerTypes, Entity[]>();

            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                _buildEnts.Add(player, new Entity[CellStartValues.ALL_CELLS_AMOUNT]);
            }

            for (byte idx = 0; idx < _builds.Length; idx++)
            {
                _builds[idx] = gameW.NewEntity()
                    .Add(new BuildingTC())
                    .Add(new PlayerTC());

                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _buildEnts[player][idx] = gameW.NewEntity()
                        .Add(new IsVisibleC());
                }
            }
        }

        public static void SetNew(in BuildingTypes build, in PlayerTypes owner, in byte idx)
        {
            if (build == default) throw new Exception("BuildType is None");
            if (Build<BuildingTC>(idx).Is(build)) throw new Exception("It's got yet");
            if (Build<BuildingTC>(idx).Have) Remove(idx);

            Build<BuildingTC>(idx).Build = build;
            Build<PlayerTC>(idx).Player = owner;
            WhereBuildsE.HaveBuild<HaveBuildingC>(build, owner, idx).Have = true;
        }

        public static void Remove(in byte idx)
        {
            if (Build<BuildingTC>(idx).Have)
            {
                WhereBuildsE.HaveBuild<HaveBuildingC>(Build<BuildingTC>(idx).Build, Build<PlayerTC>(idx).Player, idx).Have = false;

                Build<BuildingTC>(idx).Reset();
                Build<PlayerTC>(idx).Reset();
            }
        }

        public static bool CanBuild(in byte idx, in BuildingTypes build, in PlayerTypes who, out MistakeTypes mistake)
        {
            mistake = default;

            var buildC = Build<BuildingTC>(idx);


            if (EntitiesPool.UnitStep.HaveForBuilding(idx, build))
            {
                if (!buildC.Have || buildC.Is(BuildingTypes.Camp))
                {
                    if (!CellEnvironmentEs.Resources(EnvironmentTypes.AdultForest, idx).Have)
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
            var buildC = Build<BuildingTC>(idx);
            var ownC = Build<PlayerTC>(idx);


            if (Build<BuildingTC>(idx).Is(BuildingTypes.Farm) && CellEnvironmentEs.Resources(EnvironmentTypes.Fertilizer, idx).Have)
            {
                env = EnvironmentTypes.Fertilizer;
                res = ResourceTypes.Food;
            }
            else if (Build<BuildingTC>(idx).Is(BuildingTypes.Woodcutter) && CellEnvironmentEs.Resources(EnvironmentTypes.AdultForest, idx).Have)
            {
                env = EnvironmentTypes.AdultForest;
                res = ResourceTypes.Wood;
            }
            else if (Build<BuildingTC>(idx).Is(BuildingTypes.Mine) && CellEnvironmentEs.Resources(EnvironmentTypes.Hill, idx).Have)
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
                extract += (int)(extract *  0.5f);
            }


            if (extract > CellEnvironmentEs.Resources(env, idx).Amount) extract = CellEnvironmentEs.Resources(env, idx).Amount;

            return true;
        }
    }
}