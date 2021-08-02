using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Game.General.Systems.SyncCellVision
{
    internal sealed class SyncCellEnvirsVisSystem : IEcsRunSystem
    {
        public void Run()
        {
            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    var xy = new int[] { x, y };


                    for (int curNumberEnvirType = 1; curNumberEnvirType <= (int)EnvironmentTypes.Mountain; curNumberEnvirType++)
                    {
                        if (CellEnvrDataSystem.HaveEnvironment((EnvironmentTypes)curNumberEnvirType, xy))
                        {
                            CellEnvViewSystem.ActiveEnvirVis(true, (EnvironmentTypes)curNumberEnvirType, xy);
                        }
                        else
                        {
                            CellEnvViewSystem.ActiveEnvirVis(false, (EnvironmentTypes)curNumberEnvirType, xy);
                        }
                    }
                }
        }

    }
}
