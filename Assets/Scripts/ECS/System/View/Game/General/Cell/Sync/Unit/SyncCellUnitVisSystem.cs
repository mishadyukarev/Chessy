using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Workers.Game.Else.Units;
using Leopotam.Ecs;
using Photon.Pun;

namespace Assets.Scripts.ECS.Game.General.Systems.SupportVision
{
    internal sealed class SyncCellUnitVisSystem : IEcsRunSystem
    {
        public void Run()
        {
            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    int[] xy = new int[] { x, y };

                    if (CellUnitsDataContainer.IsVisibleUnit(PhotonNetwork.IsMasterClient, xy))
                    {
                        if (CellUnitsDataContainer.HaveAnyUnit(xy))
                        {
                            CellUnitViewContainer.EnableUnitSR(true, xy);
                            CellUnitViewContainer.SetSprite(CellUnitsDataContainer.UnitType(xy), xy);

                        }

                        else
                        {
                            CellUnitViewContainer.EnableUnitSR(false, xy);
                        }
                    }

                    else
                    {
                        CellUnitViewContainer.EnableUnitSR(false, xy);
                    }
                }
        }
    }
}
