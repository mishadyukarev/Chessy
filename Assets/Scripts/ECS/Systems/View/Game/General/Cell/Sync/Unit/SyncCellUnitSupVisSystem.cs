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
                            CellSupVisBarsViewSystem.ActiveVision(true, SupportStaticTypes.Hp, xy);
                            CellSupVisBarsViewSystem.SetColor(SupportStaticTypes.Hp, Color.red, xy);

                            float xCordinate = (float)CellUnitsDataSystem.AmountHealth(xy) / CellUnitsDataSystem.MaxAmountHealth(xy);
                            CellSupVisBarsViewSystem.SetScale(SupportStaticTypes.Hp, new Vector3(xCordinate * 0.67f, 0.13f, 1), xy);


                            if (CellUnitsDataSystem.HaveMaxAmountSteps(xy))
                            {
                                CellBlocksViewSystem.EnableCellSupVisBlocksSR(true, CellSupVisBlocksTypes.MaxSteps, xy);
                            }
                            else
                            {
                                CellBlocksViewSystem.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.MaxSteps, xy);
                            }

                            if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Protected, xy))
                            {
                                CellBlocksViewSystem.EnableCellSupVisBlocksSR(true, CellSupVisBlocksTypes.Condition, xy);
                                CellBlocksViewSystem.SetCellSupVisBlocksColor(Color.yellow, CellSupVisBlocksTypes.Condition, xy);
                            }

                            else if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Relaxed, xy))
                            {
                                CellBlocksViewSystem.EnableCellSupVisBlocksSR(true, CellSupVisBlocksTypes.Condition, xy);
                                CellBlocksViewSystem.SetCellSupVisBlocksColor(Color.green, CellSupVisBlocksTypes.Condition, xy);
                            }

                            else
                            {
                                CellBlocksViewSystem.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.Condition, xy);
                            }

                            if (CellUnitsDataSystem.HaveOwner(xy))
                            {
                                if (CellUnitsDataSystem.IsMasterClient(xy))
                                {
                                    CellSupVisBarsViewSystem.SetColor(SupportStaticTypes.Hp, Color.blue, xy);
                                    CellBlocksViewSystem.SetCellSupVisBlocksColor(Color.blue, CellSupVisBlocksTypes.MaxSteps, xy);
                                }
                                else
                                {
                                    CellSupVisBarsViewSystem.SetColor(SupportStaticTypes.Hp, Color.red, xy);
                                    CellBlocksViewSystem.SetCellSupVisBlocksColor(Color.red, CellSupVisBlocksTypes.MaxSteps, xy);
                                }
                            }

                            else if (CellUnitsDataSystem.IsBot(xy))
                            {
                                CellSupVisBarsViewSystem.SetColor(SupportStaticTypes.Hp, Color.red, xy);
                            }
                        }

                        else
                        {
                            CellSupVisBarsViewSystem.ActiveVision(false, SupportStaticTypes.Hp, xy);

                            CellBlocksViewSystem.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.Condition, xy);
                            CellBlocksViewSystem.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.MaxSteps, xy);
                            CellSupVisBarsViewSystem.ActiveVision(false, SupportStaticTypes.Hp, xy);
                        }
                    }

                    else
                    {
                        CellSupVisBarsViewSystem.ActiveVision(false, SupportStaticTypes.Hp, xy);

                        CellBlocksViewSystem.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.Condition, xy);
                        CellBlocksViewSystem.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.MaxSteps, xy);
                        CellSupVisBarsViewSystem.ActiveVision(false, SupportStaticTypes.Hp, xy);
                    }
                }
        }
    }
}
