using Assets.Scripts.Workers.Game.Else.Units;
using Leopotam.Ecs;
using Photon.Pun;

namespace Assets.Scripts.ECS.Game.General.Systems.SupportVision
{
    internal sealed class SyncCellUnitVisSystem : IEcsRunSystem
    {
        public void Run()
        {
            for (int x = 0; x < CellViewWorker.Xamount; x++)
                for (int y = 0; y < CellViewWorker.Yamount; y++)
                {
                    int[] xy = new int[] { x, y };

                    if (CellUnitsDataWorker.IsVisibleUnit(PhotonNetwork.IsMasterClient, xy))
                    {
                        if (CellUnitsDataWorker.HaveAnyUnit(xy))
                        {
                            CellUnitsViewWorker.EnableUnitSR(true, xy);
                            CellUnitsViewWorker.SetSprite(CellUnitsDataWorker.UnitType(xy), xy);

                        }

                        else
                        {
                            CellUnitsViewWorker.EnableUnitSR(false, xy);
                        }
                    }
                }
        }
    }
}
