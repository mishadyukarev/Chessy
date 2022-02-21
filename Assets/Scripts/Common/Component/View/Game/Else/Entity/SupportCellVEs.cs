using ECS;
using UnityEngine;

namespace Game.Game
{
    public struct SupportCellVEs
    {
        static Entity[] _supports;

        public static ref C Support<C>(in byte idx) where C : struct, ISupportVE => ref _supports[idx].Get<C>();

        public SupportCellVEs(in EcsWorld gameW, in GameObject[] cells)
        {
            _supports = new Entity[cells.Length];

            for (var idx = 0; idx < _supports.Length; idx++)
            {
                _supports[idx] = gameW.NewEntity()
                    .Add(new SpriteRendererVC(cells[idx].transform.Find("SupportVision").GetComponent<SpriteRenderer>()));
            }
        }
    }

    public interface ISupportVE { }
}
