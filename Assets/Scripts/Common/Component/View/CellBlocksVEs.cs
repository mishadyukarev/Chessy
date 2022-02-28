//using ECS;
//using System;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Game.Game
//{
//    public struct CellBlocksVEs
//    {
//        Dictionary<CellBlockTypes, SpriteRendererVC[]> _blocks;


//        public SpriteRendererVC Block(in CellBlockTypes block, in byte idx)
//        {
//            if (!_blocks.ContainsKey(block)) throw new Exception();
//            return _blocks[block][idx];
//        }
//        public CellBlocksVEs(in GameObject[] cells)
//        {
//            _blocks = new Dictionary<CellBlockTypes, SpriteRendererVC[]>();

//            for (var block = CellBlockTypes.Condition; block < CellBlockTypes.End; block++)
//            {
//                _blocks.Add(block, new SpriteRendererVC[cells.Length]);
//                for (var idx = 0; idx < _blocks[block].Length; idx++)
//                {
//                    var blocks = cells[idx].transform.Find("Blocks");
//                    var name = block.ToString();
//                    var sr = blocks.Find(name).GetComponent<SpriteRenderer>();

//                    _blocks[block][idx] = new SpriteRendererVC(sr);
//                }
//            }
//        }
//    }
//}
