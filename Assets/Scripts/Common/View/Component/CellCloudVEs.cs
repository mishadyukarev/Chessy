//using UnityEngine;

//namespace Game.Game
//{
//    public struct CellCloudVEs
//    {
//        SpriteRendererVC[] _cloud;

//        public ref SpriteRendererVC CloudCellVC(in byte idx) => ref _cloud[idx];

//        public CellCloudVEs(in GameObject[] cells)
//        {
//            _cloud = new SpriteRendererVC[Start_Values.ALL_CELLS_AMOUNT];

//            for (var idx = 0; idx < _cloud.Length; idx++)
//            {
//                _cloud[idx] = new SpriteRendererVC(cells[idx].transform.Find("Weather").Find("Cloud").GetComponent<SpriteRenderer>());
//            }
//        }
//    }
//}