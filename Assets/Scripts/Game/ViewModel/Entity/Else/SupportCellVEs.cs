using UnityEngine;

namespace Chessy.Game
{
    public struct SupportCellVEs
    {
        static SpriteRendererVC[] _supports;

        public static SpriteRendererVC Support(in byte idx) => _supports[idx];

        public SupportCellVEs(in GameObject[] cells)
        {
            _supports = new SpriteRendererVC[cells.Length];

            for (var idx = 0; idx < _supports.Length; idx++)
            {
                _supports[idx] = new SpriteRendererVC(cells[idx].transform.Find("SupportVision").GetComponent<SpriteRenderer>());
            }
        }
    }
}
