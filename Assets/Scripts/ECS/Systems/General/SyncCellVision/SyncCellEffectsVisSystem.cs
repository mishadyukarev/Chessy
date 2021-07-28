using Assets.Scripts.Workers.Game.Else.Fire;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Game.General.Systems.SyncCellVision
{
    internal sealed class SyncCellEffectsVisSystem : IEcsRunSystem
    {
        public void Run()
        {
            for (int x = 0; x < CellWorker.Xamount; x++)
                for (int y = 0; y < CellWorker.Yamount; y++)
                {
                    var xy = new int[] { x, y };

                    if (CellFireDataWorker.HaveFire(xy))
                    {
                        CellFireVisWorker.EnableSR(xy);
                    }
                    else
                    {
                        CellFireVisWorker.DisableSR(xy);
                    }
                }
        }
    }
}