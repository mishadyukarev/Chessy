using ECS;
using UnityEngine;

namespace Game.Game
{
    public struct CellBuildingVEs
    {
        static Entity[] _buildingsFront;
        static Entity[] _buildingsBack;

        public static ref C BuildFront<C>(in byte idx) where C : struct, IBuildCellV => ref _buildingsFront[idx].Get<C>();
        public static ref C BuildBack<C>(in byte idx) where C : struct, IBuildCellV => ref _buildingsBack[idx].Get<C>();

        public CellBuildingVEs(in EcsWorld gameW, in GameObject[] cells)
        {
            _buildingsFront = new Entity[CellStartValues.ALL_CELLS_AMOUNT];
            _buildingsBack = new Entity[CellStartValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < _buildingsFront.Length; idx++)
            {
                var build = cells[idx].transform.Find("Building");

                _buildingsFront[idx] = gameW.NewEntity()
                     .Add(new SpriteRendererVC(build.GetComponent<SpriteRenderer>()));

                _buildingsBack[idx] = gameW.NewEntity()
                    .Add(new SpriteRendererVC(build.transform.Find("BackBuilding").GetComponent<SpriteRenderer>()));
            }
        }
    }

    public interface IBuildCellV { }
}