using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
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

                    if (CellUnitsDataSystem.IsVisibleUnit(PhotonNetwork.IsMasterClient, xy))
                    {
                        if (CellUnitsDataSystem.HaveAnyUnit(xy))
                        {
                            CellSupVisBarsViewSystem.ActiveVision(true, CellBarTypes.Hp, xy);
                            CellSupVisBarsViewSystem.SetColor(CellBarTypes.Hp, Color.red, xy);

                            float xCordinate = (float)CellUnitsDataSystem.AmountHealth(xy) / CellUnitsDataSystem.MaxAmountHealth(xy);
                            CellSupVisBarsViewSystem.SetScale(CellBarTypes.Hp, new Vector3(xCordinate * 0.67f, 0.13f, 1), xy);


                            if (CellUnitsDataSystem.HaveMaxAmountSteps(xy))
                            {
                                CellBlocksViewSystem.EnableCellSupVisBlocksSR(true, CellBlockTypes.MaxSteps, xy);
                            }
                            else
                            {
                                CellBlocksViewSystem.EnableCellSupVisBlocksSR(false, CellBlockTypes.MaxSteps, xy);
                            }

                            if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Protected, xy))
                            {
                                CellBlocksViewSystem.EnableCellSupVisBlocksSR(true, CellBlockTypes.Condition, xy);
                                CellBlocksViewSystem.SetCellSupVisBlocksColor(Color.yellow, CellBlockTypes.Condition, xy);
                            }

                            else if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Relaxed, xy))
                            {
                                CellBlocksViewSystem.EnableCellSupVisBlocksSR(true, CellBlockTypes.Condition, xy);
                                CellBlocksViewSystem.SetCellSupVisBlocksColor(Color.green, CellBlockTypes.Condition, xy);
                            }

                            else
                            {
                                CellBlocksViewSystem.EnableCellSupVisBlocksSR(false, CellBlockTypes.Condition, xy);
                            }

                            if (CellUnitsDataSystem.HaveOwner(xy))
                            {
                                if (CellUnitsDataSystem.IsMasterClient(xy))
                                {
                                    CellSupVisBarsViewSystem.SetColor(CellBarTypes.Hp, Color.blue, xy);
                                    CellBlocksViewSystem.SetCellSupVisBlocksColor(Color.blue, CellBlockTypes.MaxSteps, xy);
                                }
                                else
                                {
                                    CellSupVisBarsViewSystem.SetColor(CellBarTypes.Hp, Color.red, xy);
                                    CellBlocksViewSystem.SetCellSupVisBlocksColor(Color.red, CellBlockTypes.MaxSteps, xy);
                                }
                            }

                            else if (CellUnitsDataSystem.IsBot(xy))
                            {
                                CellSupVisBarsViewSystem.SetColor(CellBarTypes.Hp, Color.red, xy);
                            }
                        }

                        else
                        {
                            CellSupVisBarsViewSystem.ActiveVision(false, CellBarTypes.Hp, xy);

                            CellBlocksViewSystem.EnableCellSupVisBlocksSR(false, CellBlockTypes.Condition, xy);
                            CellBlocksViewSystem.EnableCellSupVisBlocksSR(false, CellBlockTypes.MaxSteps, xy);
                            CellSupVisBarsViewSystem.ActiveVision(false, CellBarTypes.Hp, xy);
                        }
                    }

                    else
                    {
                        CellSupVisBarsViewSystem.ActiveVision(false, CellBarTypes.Hp, xy);

                        CellBlocksViewSystem.EnableCellSupVisBlocksSR(false, CellBlockTypes.Condition, xy);
                        CellBlocksViewSystem.EnableCellSupVisBlocksSR(false, CellBlockTypes.MaxSteps, xy);
                        CellSupVisBarsViewSystem.ActiveVision(false, CellBarTypes.Hp, xy);
                    }
                }
        }
    }
}
