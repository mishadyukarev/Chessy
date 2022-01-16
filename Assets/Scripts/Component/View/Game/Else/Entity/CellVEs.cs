using ECS;
using UnityEngine;

namespace Game.Game
{
    public readonly struct CellVEs
    {
        static Entity[] _cells;

        public static ref C Cell<C>(in byte idx) where C : struct, ICellVE => ref _cells[idx].Get<C>();

        public CellVEs(in EcsWorld curGameW, in GameObject[] cells)
        {
            _cells = new Entity[cells.Length];

            for (byte idx = 0; idx < _cells.Length; idx++)
            {
                var cellUnder = cells[idx].transform.Find("Cell");

                _cells[idx] = curGameW.NewEntity()
                      .Add(new GameObjectVC(cellUnder.gameObject))
                      .Add(new SpriteRendererVC(cellUnder.GetComponent<SpriteRenderer>()));
            }
        }
    }
    public interface ICellVE { }
}