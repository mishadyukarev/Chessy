using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else.Cell;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.ECS.Game.General.Systems.SupportVision
{
    internal sealed class SyncCellUnitSupVisSystem : IEcsRunSystem
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
                            CellSupVisBarsWorker.ActiveVision(true, SupportStaticTypes.Hp, xy);
                            CellSupVisBarsWorker.SetColor(SupportStaticTypes.Hp, Color.red, xy);

                            float xCordinate = (float)CellUnitsDataWorker.AmountHealth(xy) / CellUnitsDataWorker.MaxAmountHealth(xy);
                            CellSupVisBarsWorker.SetScale(SupportStaticTypes.Hp, new Vector3(xCordinate * 0.67f, 0.13f, 1), xy);


                            if (CellUnitsDataWorker.HaveMaxAmountSteps(xy))
                            {
                                CellSupVisBlocksWorker.EnableCellSupVisBlocksSR(true, CellSupVisBlocksTypes.MaxSteps, xy);
                            }
                            else
                            {
                                CellSupVisBlocksWorker.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.MaxSteps, xy);
                            }

                            if (CellUnitsDataWorker.IsProtectRelaxType(ConditionUnitTypes.Protected, xy))
                            {
                                CellSupVisBlocksWorker.EnableCellSupVisBlocksSR(true, CellSupVisBlocksTypes.Condition, xy);
                                CellSupVisBlocksWorker.SetCellSupVisBlocksColor(Color.yellow, CellSupVisBlocksTypes.Condition, xy);
                            }

                            else if (CellUnitsDataWorker.IsProtectRelaxType(ConditionUnitTypes.Relaxed, xy))
                            {
                                CellSupVisBlocksWorker.EnableCellSupVisBlocksSR(true, CellSupVisBlocksTypes.Condition, xy);
                                CellSupVisBlocksWorker.SetCellSupVisBlocksColor(Color.green, CellSupVisBlocksTypes.Condition, xy);
                            }

                            else
                            {
                                CellSupVisBlocksWorker.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.Condition, xy);
                            }

                            if (CellUnitsDataWorker.HaveOwner(xy))
                            {
                                if (CellUnitsDataWorker.IsMasterClient(xy))
                                {
                                    CellSupVisBarsWorker.SetColor(SupportStaticTypes.Hp, Color.blue, xy);
                                    CellSupVisBlocksWorker.SetCellSupVisBlocksColor(Color.blue, CellSupVisBlocksTypes.MaxSteps, xy);
                                }
                                else
                                {
                                    CellSupVisBarsWorker.SetColor(SupportStaticTypes.Hp, Color.red, xy);
                                    CellSupVisBlocksWorker.SetCellSupVisBlocksColor(Color.red, CellSupVisBlocksTypes.MaxSteps, xy);
                                }
                            }

                            else if (CellUnitsDataWorker.IsBot(xy))
                            {
                                CellSupVisBarsWorker.SetColor(SupportStaticTypes.Hp, Color.red, xy);
                            }
                        }

                        else
                        {
                            CellSupVisBarsWorker.ActiveVision(false, SupportStaticTypes.Hp, xy);

                            CellSupVisBlocksWorker.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.Condition, xy);
                            CellSupVisBlocksWorker.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.MaxSteps, xy);
                            CellSupVisBarsWorker.ActiveVision(false, SupportStaticTypes.Hp, xy);
                        }
                    }

                    else
                    {
                        CellSupVisBarsWorker.ActiveVision(false, SupportStaticTypes.Hp, xy);

                        CellSupVisBlocksWorker.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.Condition, xy);
                        CellSupVisBlocksWorker.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.MaxSteps, xy);
                        CellSupVisBarsWorker.ActiveVision(false, SupportStaticTypes.Hp, xy);
                    }
                }
        }
    }
}
