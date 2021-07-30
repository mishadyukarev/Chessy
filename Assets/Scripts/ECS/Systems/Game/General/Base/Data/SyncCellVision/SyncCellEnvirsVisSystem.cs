using Assets.Scripts.Workers.Game.Else.CellEnvir;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Game.General.Systems.SyncCellVision
{
    internal sealed class SyncCellEnvirsVisSystem : IEcsRunSystem
    {
        public void Run()
        {
            for (int x = 0; x < CellViewWorker.Xamount; x++)
                for (int y = 0; y < CellViewWorker.Yamount; y++)
                {
                    var xy = new int[] { x, y };


                    for (int curNumberEnvirType = 1; curNumberEnvirType <= (int)EnvironmentTypes.Mountain; curNumberEnvirType++)
                    {
                        if (CellEnvirDataWorker.HaveEnvironment((EnvironmentTypes)curNumberEnvirType, xy))
                        {
                            CellEnvirViewWorker.ActiveEnvirVis(true, (EnvironmentTypes)curNumberEnvirType, xy);
                        }
                        else
                        {
                            CellEnvirViewWorker.ActiveEnvirVis(false, (EnvironmentTypes)curNumberEnvirType, xy);
                        }
                    }
                }
        }

    }
}
