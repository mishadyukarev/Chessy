using Assets.Scripts.Workers.Game.Else.Units;
using Photon.Pun;

namespace Assets.Scripts.ECS.Game.General.Systems.SupportVision
{
    internal sealed class SyncCellUnitVisSystem : SystemGeneralReduction
    {
        public override void Run()
        {
            base.Run();

            for (int x = 0; x < _eGM.Xamount; x++)
                for (int y = 0; y < _eGM.Yamount; y++)
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
