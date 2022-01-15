using ECS;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public struct CellTrailVE
    {
        static Dictionary<DirectTypes, Entity[]> _trails;

        public static ref T TrailCellVC<T>(in DirectTypes dir, in byte idx) where T : struct, ITrailCellV => ref _trails[dir][idx].Get<T>();

        public CellTrailVE(in EcsWorld gameW, in GameObject[] cells)
        {
            _trails = new Dictionary<DirectTypes, Entity[]>();


            for (var dir = DirectTypes.First; dir < DirectTypes.End; dir++)
            {
                _trails.Add(dir, new Entity[CellValues.ALL_CELLS_AMOUNT]);

                for (byte idx = 0; idx < _trails[dir].Length; idx++)
                {
                    var parent_Trans = cells[idx].transform.Find("TrailZone");

                    _trails[dir][idx] = gameW.NewEntity()
                        .Add(new ParentTransformVC(parent_Trans))
                        .Add(new SpriteRendererVC(parent_Trans.Find(dir.ToString()).GetComponent<SpriteRenderer>()));
                }
            }
        }
    }

    public interface ITrailCellV { }
}