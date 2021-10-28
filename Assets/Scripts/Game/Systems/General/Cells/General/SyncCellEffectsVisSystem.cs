using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class SyncCellEffectsVisSystem : IEcsRunSystem
    {
        private EcsFilter<CellFireDataComponent, CellFireViewComponent> _cellFireFilter;

        public void Run()
        {
            foreach (var idx in _cellFireFilter)
            {
                if (_cellFireFilter.Get1(idx).HaveFire)
                {
                    _cellFireFilter.Get2(idx).EnableSR();
                }

                else
                {
                    _cellFireFilter.Get2(idx).DisableSR();
                }
            }

            //for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
            //    for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
            //    {
            //        var xy = new int[] { x, y };

            //        if (CellFireDataSystem.HaveFireCom(xy).HaveFire)
            //        {

            //        }
            //        else
            //        {
            //            CellFireViewSystem.DisableSR(xy);
            //        }
            //    }
        }
    }
}