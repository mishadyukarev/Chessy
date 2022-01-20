using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct CellUnitVisibleEs
    {
        static Dictionary<PlayerTypes, Entity[]> _unitEnts;

        public static ref IsVisibleC Visible(in PlayerTypes player, in byte idx)
        {
            if (!_unitEnts.ContainsKey(player)) throw new Exception();
            if(idx >= _unitEnts[player].Length) throw new Exception();

            return ref _unitEnts[player][idx].Get<IsVisibleC>();
        }

        public CellUnitVisibleEs(in EcsWorld gameW)
        {
            _unitEnts = new Dictionary<PlayerTypes, Entity[]>();

            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _unitEnts.Add(player, new Entity[CellStartValues.ALL_CELLS_AMOUNT]);

                for (byte idx = 0; idx < _unitEnts[player].Length; idx++)
                {
                    _unitEnts[player][idx] = gameW.NewEntity()
                        .Add(new IsVisibleC());
                }
            }
        }
    }
}