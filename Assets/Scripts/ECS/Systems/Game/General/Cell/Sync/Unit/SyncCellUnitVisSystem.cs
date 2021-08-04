using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
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

                    if (CellUnitsDataSystem.IsVisibleUnit(PhotonNetwork.IsMasterClient, xy))
                    {
                        if (CellUnitsDataSystem.HaveAnyUnit(xy))
                        {
                            CellUnitViewSystem.EnableUnitSR(true, xy);
                            CellUnitViewSystem.SetSprite(CellUnitsDataSystem.UnitType(xy), xy);

                        }

                        else
                        {
                            CellUnitViewSystem.EnableUnitSR(false, xy);
                        }
                    }

                    else
                    {
                        CellUnitViewSystem.EnableUnitSR(false, xy);
                    }
                }
        }
    }
}
