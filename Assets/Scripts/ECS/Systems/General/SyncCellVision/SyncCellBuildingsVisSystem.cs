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
            for (int x = 0; x < CellWorker.Xamount; x++)
                for (int y = 0; y < CellWorker.Yamount; y++)
                {
                    var xy = new int[] { x, y };

                    if (CellBuildingsDataWorker.HaveAnyBuilding(xy))
                    {
                        CellBuildingsVisWorker.SetSpriteFront(CellBuildingsDataWorker.GetBuildingType(xy), xy);
                        CellBuildingsVisWorker.SetEnabledFrontSR(true, xy);

                        CellBuildingsVisWorker.SetEnabledBackSR(true, xy);
                        CellBuildingsVisWorker.SetSpriteBack(CellBuildingsDataWorker.GetBuildingType(xy), xy);

                        if (CellBuildingsDataWorker.HaveOwner(xy))
                        {
                            if (CellBuildingsDataWorker.IsMasterBuilding(xy))
                            {
                                CellBuildingsVisWorker.SetBackColor(Color.blue, xy);
                            }

                            else
                            {
                                CellBuildingsVisWorker.SetBackColor(Color.red, xy);
                            }
                        }

                        else if (CellBuildingsDataWorker.IsBot(xy))
                        {
                            CellBuildingsVisWorker.SetBackColor(Color.red, xy);
                        }
                    }
                    else
                    {
                        CellBuildingsVisWorker.SetEnabledFrontSR(false, xy);
                        CellBuildingsVisWorker.SetEnabledBackSR(false, xy);
                    }
                }
        }
    }
}
