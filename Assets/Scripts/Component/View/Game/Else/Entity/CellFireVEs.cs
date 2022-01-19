using ECS;
using UnityEngine;

namespace Game.Game
{
    public struct CellFireVEs
    {
        static Entity[] _fires;

        public static ref T FireCellVC<T>(in byte idx) where T : struct, IFireCellVE => ref _fires[idx].Get<T>();

        public CellFireVEs(in EcsWorld gameW, in GameObject[] cells)
        {
            _fires = new Entity[CellStartValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < _fires.Length; idx++)
            {
                _fires[idx] = gameW.NewEntity()
                    .Add(new SpriteRendererVC(cells[idx].transform.Find("Fire").GetComponent<SpriteRenderer>()));
            }
        }
    }

    public interface IFireCellVE { }
}