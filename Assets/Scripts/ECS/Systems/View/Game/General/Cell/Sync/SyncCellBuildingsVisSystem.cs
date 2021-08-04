using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
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

                    if (CellBuildDataSystem.BuildTypeCom(xy).HaveBuild)
                    {
                        CellBuildViewSystem.SetSpriteFront(CellBuildDataSystem.BuildTypeCom(xy).BuildingType, xy);
                        CellBuildViewSystem.SetEnabledFrontSR(true, xy);

                        CellBuildViewSystem.SetEnabledBackSR(true, xy);
                        CellBuildViewSystem.SetSpriteBack(CellBuildDataSystem.BuildTypeCom(xy).BuildingType, xy);

                        if (CellBuildDataSystem.OwnerCom(xy).HaveOwner)
                        {
                            if (CellBuildDataSystem.OwnerCom(xy).IsMasterClient)
                            {
                                CellBuildViewSystem.SetBackColor(Color.blue, xy);
                            }

                            else
                            {
                                CellBuildViewSystem.SetBackColor(Color.red, xy);
                            }
                        }

                        else if (CellBuildDataSystem.OwnerBotCom(xy).IsBot)
                        {
                            CellBuildViewSystem.SetBackColor(Color.red, xy);
                        }
                    }
                    else
                    {
                        CellBuildViewSystem.SetEnabledFrontSR(false, xy);
                        CellBuildViewSystem.SetEnabledBackSR(false, xy);
                    }
                }
        }
    }
}
