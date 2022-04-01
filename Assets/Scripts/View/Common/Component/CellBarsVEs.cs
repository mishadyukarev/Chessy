//using System;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Game.Game
//{
//    public struct CellBarsVEs
//    {
//        static Dictionary<CellBarTypes, SpriteRendererVC[]> _bars;

//        public SpriteRendererVC Bar(in CellBarTypes bar, in byte idx)
//        {
//            if (!_bars.ContainsKey(bar)) throw new Exception();
//            return _bars[bar][idx];
//        }

//        public CellBarsVEs(in GameObject[] cells)
//        {
//            _bars = new Dictionary<CellBarTypes, SpriteRendererVC[]>();

//            for (var bar = CellBarTypes.Food; bar < CellBarTypes.End; bar++)
//            {
//                _bars.Add(bar, new SpriteRendererVC[cells.Length]);
//                for (var idx = 0; idx < _bars[bar].Length; idx++)
//                {
//                    var bars = cells[idx].transform.Find("Bars");
//                    var name = bar.ToString();
//                    var sr = bars.Find(name).GetComponent<SpriteRenderer>();

//                    _bars[bar][idx] = new SpriteRendererVC(sr);
//                }
//            }
//        }
//    }
//}
