using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Workers.Game.Else.CellEnvir;
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
                        if (CellEnvirDataContainer.HaveEnvironment((EnvironmentTypes)curNumberEnvirType, xy))
                        {
                            CellEnvirViewContainer.ActiveEnvirVis(true, (EnvironmentTypes)curNumberEnvirType, xy);
                        }
                        else
                        {
                            CellEnvirViewContainer.ActiveEnvirVis(false, (EnvironmentTypes)curNumberEnvirType, xy);
                        }
                    }
                }
        }

    }
}
