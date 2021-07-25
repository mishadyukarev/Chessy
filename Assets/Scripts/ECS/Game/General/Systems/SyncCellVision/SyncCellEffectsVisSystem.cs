using Assets.Scripts.Workers.Game.Else.Fire;

namespace Assets.Scripts.ECS.Game.General.Systems.SyncCellVision
{
    internal sealed class SyncCellEffectsVisSystem : SystemGeneralReduction
    {
        public override void Run()
        {
            base.Run();

            for (int x = 0; x < _eGM.Xamount; x++)
                for (int y = 0; y < _eGM.Yamount; y++)
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