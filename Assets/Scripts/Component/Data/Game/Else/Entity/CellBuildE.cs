using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellBuildE
    {
        readonly static Entity[] _builds;

        readonly static Dictionary<PlayerTypes, Entity[]> _buildEnts;

        public static ref T Build<T>(in byte idx) where T : struct, IBuildCell => ref _builds[idx].Get<T>();
        public static ref T Build<T>(in PlayerTypes player, in byte idx) where T : struct, IBuildPlayerCellE => ref _buildEnts[player][idx].Get<T>();


        static CellBuildE()
        {
            _builds = new Entity[CellValues.ALL_CELLS_AMOUNT];
            _buildEnts = new Dictionary<PlayerTypes, Entity[]>();

            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                _buildEnts.Add(player, new Entity[CellValues.ALL_CELLS_AMOUNT]);
            }
        }
        public CellBuildE(in EcsWorld gameW)
        {
            for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                _builds[idx] = gameW.NewEntity()
                    .Add(new BuildCellEC(idx))
                    .Add(new BuildingTC())
                    .Add(new PlayerTC());

                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _buildEnts[player][idx] = gameW.NewEntity()
                        .Add(new IsVisibledC());
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
    }
}