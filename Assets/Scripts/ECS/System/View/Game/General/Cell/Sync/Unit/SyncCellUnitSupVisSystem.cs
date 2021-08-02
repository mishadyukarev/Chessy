using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
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

            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    int[] xy = new int[] { x, y };

                    if (CellUnitsDataContainer.IsVisibleUnit(PhotonNetwork.IsMasterClient, xy))
                    {
                        if (CellUnitsDataContainer.HaveAnyUnit(xy))
                        {
                            CellSupVisBarsContainer.ActiveVision(true, SupportStaticTypes.Hp, xy);
                            CellSupVisBarsContainer.SetColor(SupportStaticTypes.Hp, Color.red, xy);

                            float xCordinate = (float)CellUnitsDataContainer.AmountHealth(xy) / CellUnitsDataContainer.MaxAmountHealth(xy);
                            CellSupVisBarsContainer.SetScale(SupportStaticTypes.Hp, new Vector3(xCordinate * 0.67f, 0.13f, 1), xy);


                            if (CellUnitsDataContainer.HaveMaxAmountSteps(xy))
                            {
                                CellBlocksViewContainer.EnableCellSupVisBlocksSR(true, CellSupVisBlocksTypes.MaxSteps, xy);
                            }
                            else
                            {
                                CellBlocksViewContainer.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.MaxSteps, xy);
                            }

                            if (CellUnitsDataContainer.IsConditionType(ConditionUnitTypes.Protected, xy))
                            {
                                CellBlocksViewContainer.EnableCellSupVisBlocksSR(true, CellSupVisBlocksTypes.Condition, xy);
                                CellBlocksViewContainer.SetCellSupVisBlocksColor(Color.yellow, CellSupVisBlocksTypes.Condition, xy);
                            }

                            else if (CellUnitsDataContainer.IsConditionType(ConditionUnitTypes.Relaxed, xy))
                            {
                                CellBlocksViewContainer.EnableCellSupVisBlocksSR(true, CellSupVisBlocksTypes.Condition, xy);
                                CellBlocksViewContainer.SetCellSupVisBlocksColor(Color.green, CellSupVisBlocksTypes.Condition, xy);
                            }

                            else
                            {
                                CellBlocksViewContainer.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.Condition, xy);
                            }

                            if (CellUnitsDataContainer.HaveOwner(xy))
                            {
                                if (CellUnitsDataContainer.IsMasterClient(xy))
                                {
                                    CellSupVisBarsContainer.SetColor(SupportStaticTypes.Hp, Color.blue, xy);
                                    CellBlocksViewContainer.SetCellSupVisBlocksColor(Color.blue, CellSupVisBlocksTypes.MaxSteps, xy);
                                }
                                else
                                {
                                    CellSupVisBarsContainer.SetColor(SupportStaticTypes.Hp, Color.red, xy);
                                    CellBlocksViewContainer.SetCellSupVisBlocksColor(Color.red, CellSupVisBlocksTypes.MaxSteps, xy);
                                }
                            }

                            else if (CellUnitsDataContainer.IsBot(xy))
                            {
                                CellSupVisBarsContainer.SetColor(SupportStaticTypes.Hp, Color.red, xy);
                            }
                        }

                        else
                        {
                            CellSupVisBarsContainer.ActiveVision(false, SupportStaticTypes.Hp, xy);

                            CellBlocksViewContainer.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.Condition, xy);
                            CellBlocksViewContainer.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.MaxSteps, xy);
                            CellSupVisBarsContainer.ActiveVision(false, SupportStaticTypes.Hp, xy);
                        }
                    }

                    else
                    {
                        CellSupVisBarsContainer.ActiveVision(false, SupportStaticTypes.Hp, xy);

                        CellBlocksViewContainer.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.Condition, xy);
                        CellBlocksViewContainer.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.MaxSteps, xy);
                        CellSupVisBarsContainer.ActiveVision(false, SupportStaticTypes.Hp, xy);
                    }
                }
        }
    }
}
