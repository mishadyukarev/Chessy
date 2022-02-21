using ECS;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public struct CellBarsVEs
    {
        static Dictionary<CellBarTypes, Entity[]> _bars;

        public static ref C Bar<C>(in CellBarTypes bar, in byte idx) where C : struct, IBlockCellVE
        {
            if (!_bars.ContainsKey(bar)) throw new Exception();
            return ref _bars[bar][idx].Get<C>();
        }

        public CellBarsVEs(in EcsWorld gameW, in GameObject[] cells)
        {
            _bars = new Dictionary<CellBarTypes, Entity[]>();

            for (var bar = CellBarTypes.Food; bar < CellBarTypes.End; bar++)
            {
                _bars.Add(bar, new Entity[cells.Length]);
                for (var idx = 0; idx < _bars[bar].Length; idx++)
                {
                    var bars = cells[idx].transform.Find("Bars");
                    var name = bar.ToString();
                    var sr = bars.Find(name).GetComponent<SpriteRenderer>();

                    _bars[bar][idx] = gameW.NewEntity()
                        .Add(new SpriteRendererVC(sr));
                }
            }
        }
    }

    public interface IBarCellVE { }
}
