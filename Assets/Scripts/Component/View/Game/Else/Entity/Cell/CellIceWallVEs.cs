//using ECS;
//using UnityEditor;
//using UnityEngine;

//namespace Game.Game
//{
//    public struct CellIceWallVEs
//    {
//        static Entity[] _ents;

//        public static ref SpriteRendererVC SR(in byte idx) => ref _ents[idx].Get<SpriteRendererVC>();

//        public CellIceWallVEs(in EcsWorld gameW, in GameObject[] cells)
//        {
//            _ents = new Entity[cells.Length];

//            for (var idx = 0; idx < _ents.Length; idx++)
//            {
//                _ents[idx] = gameW.NewEntity()
//                    .Add(new SpriteRendererVC(cells[idx].transform.Find("IceWall_SR").GetComponent<SpriteRenderer>()));
//            }
//        }
//    }
//}