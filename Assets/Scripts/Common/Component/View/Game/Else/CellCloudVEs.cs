using ECS;
using UnityEngine;

namespace Game.Game
{
    public struct CellCloudVEs : ICloudCellV
    {
        static Entity[] _cloud;

        public static ref T CloudCellVC<T>(in byte idx) where T : struct, ICloudCellV => ref _cloud[idx].Get<T>();

        public CellCloudVEs(in EcsWorld gameW, in GameObject[] cells)
        {
            _cloud = new Entity[StartValues.ALL_CELLS_AMOUNT];

            for (var idx = 0; idx < _cloud.Length; idx++)
            {
                _cloud[idx] = gameW.NewEntity()
                    .Add(new SpriteRendererVC(cells[idx].transform.Find("Weather").Find("Cloud").GetComponent<SpriteRenderer>()));
            }
        }
    }

    public interface ICloudCellV { }
}