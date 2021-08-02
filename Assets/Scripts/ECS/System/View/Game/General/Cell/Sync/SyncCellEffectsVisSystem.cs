using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Workers.Game.Else.Fire;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Game.General.Systems.SyncCellVision
{
    internal sealed class SyncCellEffectsVisSystem : IEcsRunSystem
    {
        public void Run()
        {
            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    var xy = new int[] { x, y };

                    if (CellFireDataContainer.HaveFire(xy))
                    {
                        CellFireViewContainer.EnableSR(xy);
                    }
                    else
                    {
                        CellFireViewContainer.DisableSR(xy);
                    }
                }
        }
    }
}