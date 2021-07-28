using Assets.Scripts.Workers.Game.Else.Units;
using Leopotam.Ecs;
using Photon.Pun;

namespace Assets.Scripts.ECS.Game.General.Systems.SupportVision
{
    internal sealed class SyncCellUnitVisSystem : IEcsRunSystem
    {
        public void Run()
        {
            for (int x = 0; x < CellWorker.Xamount; x++)
                for (int y = 0; y < CellWorker.Yamount; y++)
                {
                    int[] xy = new int[] { x, y };

                    if (CellUnitsDataWorker.IsVisibleUnit(PhotonNetwork.IsMasterClient, xy))
                    {
                        if (CellUnitsDataWorker.HaveAnyUnit(xy))
                        {
                            CellUnitsVisWorker.EnableUnitSR(true, xy);
                            CellUnitsVisWorker.SetSprite(CellUnitsDataWorker.UnitType(xy), xy);

                        }

                        else
                        {
                            CellUnitsVisWorker.EnableUnitSR(false, xy);
                        }
                    }
                }
        }
    }
}
