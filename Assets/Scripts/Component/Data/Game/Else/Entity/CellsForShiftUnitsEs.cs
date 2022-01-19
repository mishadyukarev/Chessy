using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct CellsForShiftUnitsEs
    {
        static Dictionary<PlayerTypes, Entity[]> _ents;

        public static ref C CellsForShift<C>(in PlayerTypes player, in byte idx) where C : struct, ICellsForShiftE => ref _ents[player][idx].Get<C>();

        public CellsForShiftUnitsEs(in EcsWorld gameW)
        {
            _ents = new Dictionary<PlayerTypes, Entity[]>();

            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                _ents.Add(player, new Entity[CellStartValues.ALL_CELLS_AMOUNT]);

                for (var idx = 0; idx < _ents[player].Length; idx++)
                {
                    _ents[player][idx] = gameW.NewEntity()
                        .Add(new IdxsC(new List<byte>()));
                }
            }
        }
    }

    public interface ICellsForShiftE { }
}