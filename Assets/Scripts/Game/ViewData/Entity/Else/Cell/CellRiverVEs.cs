using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public struct CellRiverVEs
    {
        static TransformVC[] _parents;
        static Dictionary<DirectTypes, SpriteRendererVC[]> _rivers;

        public static ref TransformVC Parent(in byte idx) => ref _parents[idx];
        public static ref SpriteRendererVC River(in DirectTypes dir, in byte idx) => ref _rivers[dir][idx];

        public CellRiverVEs(in GameObject[] cells)
        {
            _rivers = new Dictionary<DirectTypes, SpriteRendererVC[]>();

            _parents = new TransformVC[Start_Values.ALL_CELLS_AMOUNT];
            for (var idx = 0; idx < _parents.Length; idx++)
            {
                _parents[idx] = new TransformVC(cells[idx].transform.Find("River"));
            }


            for (var dir = DirectTypes.Up; dir < DirectTypes.End; dir++)
            {
                if (dir == DirectTypes.Up || dir == DirectTypes.Right || dir == DirectTypes.Down || dir == DirectTypes.Left)
                {
                    _rivers.Add(dir, new SpriteRendererVC[Start_Values.ALL_CELLS_AMOUNT]);
                    for (byte idx = 0; idx < _rivers[dir].Length; idx++)
                    {
                        var river = cells[idx].transform.Find("River");
                        _rivers[dir][idx] = new SpriteRendererVC(river.Find(dir.ToString()).GetComponent<SpriteRenderer>());
                    }
                }
            }
        }
    }
}