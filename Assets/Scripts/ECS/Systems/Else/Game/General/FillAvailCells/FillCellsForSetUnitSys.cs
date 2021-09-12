using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.Workers;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.General.FillAvailCells
{
    internal sealed class FillCellsForSetUnitSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataComponent> _cellUnitFilter = default;

        private EcsFilter<CellsForSetUnitComp> _cellsForSetUnitFilter = default;


        public void Run()
        {
            ref var cellsForSetUnitComp = ref _cellsForSetUnitFilter.Get1(0);


            cellsForSetUnitComp.ClearIdxCells(true);
            cellsForSetUnitComp.ClearIdxCells(false);


            foreach (byte curIdx in _xyCellFilter)
            {
                var xy = _xyCellFilter.GetXyCell(curIdx);
                var x = xy[0];
                var y = xy[1];

                ref var curUnitDataFilter = ref _cellUnitFilter.Get1(curIdx);


                if (!curUnitDataFilter.HaveUnit)
                {
                    //if (y < 3 && x > 2 && x < 12)
                    //{
                        cellsForSetUnitComp.AddIdxCell(true, curIdx);
                    //}
                    //else if (y > 7 && x > 2 && x < 12)
                    //{
                        cellsForSetUnitComp.AddIdxCell(false, curIdx);
                    //}

                }
            }
        }
    }
}
