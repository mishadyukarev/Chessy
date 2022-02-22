using ECS;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public struct CellRiverVEs
    {
        static Entity[] _parents;
        static Dictionary<DirectTypes, Entity[]> _rivers;

        public static ref TransformVC Parent(in byte idx) => ref _parents[idx].Get<TransformVC>();
        public static ref SpriteRendererVC River(in DirectTypes dir, in byte idx) => ref _rivers[dir][idx].Get<SpriteRendererVC>();

        public CellRiverVEs(in EcsWorld gameW, in GameObject[] cells)
        {
            _rivers = new Dictionary<DirectTypes, Entity[]>();

            _parents = new Entity[Start_Values.ALL_CELLS_AMOUNT];
            for (var idx = 0; idx < _parents.Length; idx++)
            {
                _parents[idx] = gameW.NewEntity()
                    .Add(new TransformVC(cells[idx].transform.Find("River")));
            }


            for (var dir = DirectTypes.Up; dir < DirectTypes.End; dir++)
            {
                if (dir == DirectTypes.Up || dir == DirectTypes.Right || dir == DirectTypes.Down || dir == DirectTypes.Left)
                {
                    _rivers.Add(dir, new Entity[Start_Values.ALL_CELLS_AMOUNT]);
                    for (byte idx = 0; idx < _rivers[dir].Length; idx++)
                    {
                        var river = cells[idx].transform.Find("River");
                        _rivers[dir][idx] = gameW.NewEntity()
                            .Add(new SpriteRendererVC(river.Find(dir.ToString()).GetComponent<SpriteRenderer>()));
                    }
                }
            }
        }
    }
}