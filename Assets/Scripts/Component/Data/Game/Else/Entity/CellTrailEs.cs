﻿using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellTrailEs
    {
        static Dictionary<DirectTypes, Entity[]> _trails;
        static Dictionary<PlayerTypes, Entity[]> _trailVisibleEnts;

        public static ref T Trail<T>(in byte idx, in DirectTypes dir = default) where T : struct, ICellTrailE => ref _trails[dir][idx].Get<T>();
        public static ref T Trail<T>(in PlayerTypes player, in byte idx) where T : struct, ITrailVisibledCellE => ref _trailVisibleEnts[player][idx].Get<T>();


        public CellTrailEs(in EcsWorld gameW)
        {
            _trails = new Dictionary<DirectTypes, Entity[]>();
            _trailVisibleEnts = new Dictionary<PlayerTypes, Entity[]>();

            for (var dir = DirectTypes.Start; dir < DirectTypes.End; dir++)
            {
                _trails.Add(dir, new Entity[CellValues.ALL_CELLS_AMOUNT]);
            }

            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                _trailVisibleEnts.Add(player, new Entity[CellValues.ALL_CELLS_AMOUNT]);
            }


            byte idx = 0;

            for (idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                for (var dir = DirectTypes.Start; dir < DirectTypes.End; dir++)
                {
                    _trails[dir][idx] = gameW.NewEntity()
                    .Add(new TrailCellEC(idx))
                    .Add(new AmountC());
                }

                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _trailVisibleEnts[player][idx] = gameW.NewEntity()
                        .Add(new IsVisibleC());
                }
            }
        }
    }
    public interface ICellTrailE { }
}