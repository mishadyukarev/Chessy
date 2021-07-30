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
            for (int x = 0; x < CellViewWorker.Xamount; x++)
                for (int y = 0; y < CellViewWorker.Yamount; y++)
                {
                    var xy = new int[] { x, y };

                    if (CellBuildingsDataWorker.HaveAnyBuilding(xy))
                    {
                        CellBuildingsViewWorker.SetSpriteFront(CellBuildingsDataWorker.GetBuildingType(xy), xy);
                        CellBuildingsViewWorker.SetEnabledFrontSR(true, xy);

                        CellBuildingsViewWorker.SetEnabledBackSR(true, xy);
                        CellBuildingsViewWorker.SetSpriteBack(CellBuildingsDataWorker.GetBuildingType(xy), xy);

                        if (CellBuildingsDataWorker.HaveOwner(xy))
                        {
                            if (CellBuildingsDataWorker.IsMasterBuilding(xy))
                            {
                                CellBuildingsViewWorker.SetBackColor(Color.blue, xy);
                            }

                            else
                            {
                                CellBuildingsViewWorker.SetBackColor(Color.red, xy);
                            }
                        }

                        else if (CellBuildingsDataWorker.IsBot(xy))
                        {
                            CellBuildingsViewWorker.SetBackColor(Color.red, xy);
                        }
                    }
                    else
                    {
                        CellBuildingsViewWorker.SetEnabledFrontSR(false, xy);
                        CellBuildingsViewWorker.SetEnabledBackSR(false, xy);
                    }
                }
        }
    }
}
