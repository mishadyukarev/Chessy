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
            _builds = new Entity[CellValues.ALL_CELLS_AMOUNT];
            _buildEnts = new Dictionary<PlayerTypes, Entity[]>();

            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                _buildEnts.Add(player, new Entity[CellValues.ALL_CELLS_AMOUNT]);
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


            if (CellUnitStepEs.Have(idx, build))
            {
                if (!buildC.Have || buildC.Is(BuildingTypes.Camp))
                {
                    if (!CellEnvironmentEs.Environment<HaveEnvironmentC>(EnvironmentTypes.AdultForest, idx).Have)
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

        public static bool CanExtract(in byte idx, out int extract, out EnvironmentTypes env, out ResTypes res)
        {
            var ownC = Build<PlayerTC>(idx);


            if (Build<BuildingTC>(idx).Is(BuildingTypes.Farm) && CellEnvironmentEs.Environment<HaveEnvironmentC>(EnvironmentTypes.Fertilizer, idx).Have)
            {
                env = EnvironmentTypes.Fertilizer;
                res = ResTypes.Food;
            }
            else if (Build<BuildingTC>(idx).Is(BuildingTypes.Woodcutter) && CellEnvironmentEs.Environment<HaveEnvironmentC>(EnvironmentTypes.AdultForest, idx).Have)
            {
                env = EnvironmentTypes.AdultForest;
                res = ResTypes.Wood;
            }
            else if (Build<BuildingTC>(idx).Is(BuildingTypes.Mine) && CellEnvironmentEs.Environment<HaveEnvironmentC>(EnvironmentTypes.Hill, idx).Have)
            {
                env = EnvironmentTypes.Hill;
                res = ResTypes.Ore;
            }
            else
            {
                extract = default;
                env = default;
                res = default;

                return false;
            }



            extract = 10;
            //extract += (int)(extract * BuildsUpgC.PercUpg(Build<BuildingC>(_idx).Build, ownC.Player));


            if (extract > CellEnvironmentEs.Environment<AmountC>(env, idx).Amount) extract = CellEnvironmentEs.Environment<AmountC>(env, idx).Amount;

            return true;
        }
    }
}