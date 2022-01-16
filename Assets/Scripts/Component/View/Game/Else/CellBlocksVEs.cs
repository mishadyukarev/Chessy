using ECS;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public struct CellBlocksVEs
    {
        static Dictionary<CellBlockTypes, Entity[]> _blocks;


        public static ref C Block<C>(in CellBlockTypes block, in byte idx) where C : struct, IBlockCellVE
        {
            if (!_blocks.ContainsKey(block)) throw new Exception();
            return ref _blocks[block][idx].Get<C>();
        }
        public CellBlocksVEs(in EcsWorld gameW, in GameObject[] cells)
        {
            _blocks = new Dictionary<CellBlockTypes, Entity[]>();

            for (var block = CellBlockTypes.Condition; block < CellBlockTypes.End; block++)
            {
                _blocks.Add(block, new Entity[cells.Length]);
                for (var idx = 0; idx < _blocks[block].Length; idx++)
                {
                    var blocks = cells[idx].transform.Find("Blocks");
                    var name = block.ToString();
                    var sr = blocks.Find(name).GetComponent<SpriteRenderer>();

                    _blocks[block][idx] = gameW.NewEntity()
                        .Add(new SpriteRendererVC(sr));
                }
            }
        }
    }

    public interface IBlockCellVE { }
}
