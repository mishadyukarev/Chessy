using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
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

                    if (CellFireDataSystem.HaveFireCom(xy).HaveFire)
                    {
                        CellFireViewSystem.EnableSR(xy);
                    }
                    else
                    {
                        CellFireViewSystem.DisableSR(xy);
                    }
                }
        }
    }
}