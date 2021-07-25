using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.CellBuildings;
using UnityEngine;

namespace Assets.Scripts.ECS.Game.General.Systems.SyncCellVision
{
    internal sealed class SyncCellBuildingsVisSystem : SystemGeneralReduction
    {
        public override void Run()
        {
            base.Run();

            for (int x = 0; x < _eGM.Xamount; x++)
                for (int y = 0; y < _eGM.Yamount; y++)
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
