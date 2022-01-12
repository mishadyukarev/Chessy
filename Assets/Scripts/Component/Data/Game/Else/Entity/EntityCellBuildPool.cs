using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct EntityCellBuildPool
    {
        readonly static Entity[] _builds;

        readonly static Dictionary<PlayerTypes, Entity[]> _buildEnts;

        public static ref T Build<T>(in byte idx) where T : struct, IBuildCell => ref _builds[idx].Get<T>();
        public static ref T Build<T>(in PlayerTypes player, in byte idx) where T : struct, IBuildPlayerCellE => ref _buildEnts[player][idx].Get<T>();


        static EntityCellBuildPool()
        {
            _builds = new Entity[CellValues.ALL_CELLS_AMOUNT];
            _buildEnts = new Dictionary<PlayerTypes, Entity[]>();

            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                _buildEnts.Add(player, new Entity[CellValues.ALL_CELLS_AMOUNT]);
            }
        }
        public EntityCellBuildPool(in EcsWorld gameW)
        {
            for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                _builds[idx] = gameW.NewEntity()
                    .Add(new BuildCellEC(idx))
                    .Add(new BuildC())
                    .Add(new PlayerC());

                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _buildEnts[player][idx] = gameW.NewEntity()
                        .Add(new VisibledC());
                }
            }
        }
    }
}