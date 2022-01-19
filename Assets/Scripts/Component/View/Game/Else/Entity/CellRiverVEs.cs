using ECS;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public struct CellRiverVEs
    {
        static Dictionary<DirectTypes, Entity[]> _rivers;

        public static ref C River<C>(in DirectTypes dir, in byte idx) where C : struct, IRiverCellVE => ref _rivers[dir][idx].Get<C>();

        public CellRiverVEs(in EcsWorld gameW, in GameObject[] cells)
        {
            _rivers = new Dictionary<DirectTypes, Entity[]>();

            for (var dir = DirectTypes.Up; dir < DirectTypes.End; dir++)
            {
                if (dir == DirectTypes.Up || dir == DirectTypes.Right || dir == DirectTypes.Down || dir == DirectTypes.Left)
                {
                    _rivers.Add(dir, new Entity[CellStartValues.ALL_CELLS_AMOUNT]);
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
    //    public void Rotate(PlayerTypes player)
    //    {
    //        switch (player)
    //        {
    //            case PlayerTypes.None: throw new Exception();
    //            case PlayerTypes.First:
    //                _parent_Trans.localEulerAngles = new Vector3(0, 0, 0);
    //                break;
    //            case PlayerTypes.Second:
    //                _parent_Trans.localEulerAngles = new Vector3(0, 0, 180);
    //                break;
    //            default: throw new Exception();
    //        }

    //    }
    //}


    public interface IRiverCellVE { }
}