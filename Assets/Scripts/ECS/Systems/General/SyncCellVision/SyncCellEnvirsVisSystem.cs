using Assets.Scripts.Workers.Game.Else.CellEnvir;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Game.General.Systems.SyncCellVision
{
    internal sealed class SyncCellEnvirsVisSystem : IEcsRunSystem
    {
        public void Run()
        {
            for (int x = 0; x < CellWorker.Xamount; x++)
                for (int y = 0; y < CellWorker.Yamount; y++)
                {
                    var xy = new int[] { x, y };


                    for (int curNumberEnvirType = 1; curNumberEnvirType <= (int)EnvironmentTypes.Mountain; curNumberEnvirType++)
                    {
                        if (CellEnvirDataWorker.HaveEnvironment((EnvironmentTypes)curNumberEnvirType, xy))
                        {
                            CellEnvirVisWorker.ActiveEnvirVis(true, (EnvironmentTypes)curNumberEnvirType, xy);
                        }
                        else
                        {
                            CellEnvirVisWorker.ActiveEnvirVis(false, (EnvironmentTypes)curNumberEnvirType, xy);
                        }
                    }
                }
        }

    }
}
