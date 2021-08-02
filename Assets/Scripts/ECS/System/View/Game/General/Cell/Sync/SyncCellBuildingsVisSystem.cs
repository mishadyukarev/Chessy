using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.CellBuildings;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.Game.General.Systems.SyncCellVision
{
    internal sealed class SyncCellBuildingsVisSystem : IEcsRunSystem
    {
        public void Run()
        {
            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    var xy = new int[] { x, y };

                    if (CellBuildDataContainer.HaveAnyBuilding(xy))
                    {
                        CellBuildingViewContainer.SetSpriteFront(CellBuildDataContainer.GetBuildingType(xy), xy);
                        CellBuildingViewContainer.SetEnabledFrontSR(true, xy);

                        CellBuildingViewContainer.SetEnabledBackSR(true, xy);
                        CellBuildingViewContainer.SetSpriteBack(CellBuildDataContainer.GetBuildingType(xy), xy);

                        if (CellBuildDataContainer.HaveOwner(xy))
                        {
                            if (CellBuildDataContainer.IsMasterBuilding(xy))
                            {
                                CellBuildingViewContainer.SetBackColor(Color.blue, xy);
                            }

                            else
                            {
                                CellBuildingViewContainer.SetBackColor(Color.red, xy);
                            }
                        }

                        else if (CellBuildDataContainer.IsBot(xy))
                        {
                            CellBuildingViewContainer.SetBackColor(Color.red, xy);
                        }
                    }
                    else
                    {
                        CellBuildingViewContainer.SetEnabledFrontSR(false, xy);
                        CellBuildingViewContainer.SetEnabledBackSR(false, xy);
                    }
                }
        }
    }
}
