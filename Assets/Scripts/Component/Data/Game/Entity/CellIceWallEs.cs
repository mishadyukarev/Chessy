﻿using ECS;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public struct CellIceWallEs
    {
        static Entity[] _ents;

        public static ref AmountC Hp(in byte idx) => ref _ents[idx].Get<AmountC>();

        public CellIceWallEs(in EcsWorld gameW)
        {
            _ents = new Entity[CellStartValues.ALL_CELLS_AMOUNT];

            for (var idx = 0; idx < _ents.Length; idx++)
            {
                _ents[idx] = gameW.NewEntity()
                    .Add(new AmountC());
            }
        }
    }
}