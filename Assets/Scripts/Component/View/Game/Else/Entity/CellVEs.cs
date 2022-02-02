using ECS;
using UnityEngine;

namespace Game.Game
{
    public readonly struct CellVEs
    {
        readonly Entity _cellParent;
        readonly Entity _cell;

        public ref GameObjectVC CellParent => ref _cellParent.Get<GameObjectVC>();
        public ref GameObjectVC CellGO => ref _cell.Get<GameObjectVC>();
        public ref SpriteRendererVC CellSR => ref _cell.Get<SpriteRendererVC>();


        public readonly CellFireVE FireVE;

        public readonly CellEnvironmentVEs EnvironmentVEs;
        public readonly CellUnitVEs UnitVEs;


        public CellVEs(in byte idx, in GameObject cell, in EcsWorld gameW)
        {
            _cellParent = gameW.NewEntity()
                .Add(new GameObjectVC(cell));

            var cellUnder = cell.transform.Find("Cell");

            _cell = gameW.NewEntity()
                  .Add(new GameObjectVC(cellUnder.gameObject))
                  .Add(new SpriteRendererVC(cellUnder.GetComponent<SpriteRenderer>()));


            FireVE = new CellFireVE(cell, gameW);

 
            EnvironmentVEs = new CellEnvironmentVEs(cell, gameW);
            UnitVEs = new CellUnitVEs(cell.transform, gameW);
        }
    }
}