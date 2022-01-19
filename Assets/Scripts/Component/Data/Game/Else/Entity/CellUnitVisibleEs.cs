using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct CellUnitVisibleEs
    {
        static Dictionary<PlayerTypes, Entity[]> _unitEnts;

        public static ref C Visible<C>(in PlayerTypes player, in byte idx) where C : struct, IUnitPlayerCellE => ref _unitEnts[player][idx].Get<C>();

        public CellUnitVisibleEs(in EcsWorld gameW)
        {
            _unitEnts = new Dictionary<PlayerTypes, Entity[]>();

            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                _unitEnts.Add(player, new Entity[CellStartValues.ALL_CELLS_AMOUNT]);
            }
            for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _unitEnts[player][idx] = gameW.NewEntity()
                        .Add(new IsVisibleC());
                }
            }
        }
    }

    public interface IUnitPlayerCellE { }
}