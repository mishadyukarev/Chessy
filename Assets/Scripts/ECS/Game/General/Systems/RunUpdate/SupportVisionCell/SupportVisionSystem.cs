using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Game.Else.Cell;
using Photon.Pun;
using UnityEngine;

internal sealed class SupportVisionSystem : SystemGeneralReduction
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);

    public override void Run()
    {
        base.Run();

        for (int x = 0; x < _eGM.Xamount; x++)
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                int[] xy = new int[] { x, y };

                CellSupVisBlocksWorker.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.Condition, xy);
                CellSupVisBlocksWorker.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.MaxSteps, xy);
                CellSupVisWorker.DisableSupVis(xy);

                if (SelectorWorker.HaveAnySelectorUnit)
                {
                    if (Main.Instance.IsMasterClient)
                    {
                        if (InfoCellWorker.IsStartedCell(PhotonNetwork.IsMasterClient, xy))
                        {
                            CellSupVisWorker.EnableSupVis(SupportVisionTypes.Spawn, xy);
                        }
                    }

                    else
                    {
                        if (InfoCellWorker.IsStartedCell(PhotonNetwork.IsMasterClient, xy))
                        {
                            CellSupVisWorker.EnableSupVis(SupportVisionTypes.Spawn, xy);
                        }
                    }
                }


                if (CellUnitWorker.IsVisibleUnit(PhotonNetwork.IsMasterClient, xy))
                {
                    if (CellUnitWorker.HaveAnyUnit(xy))
                    {
                        var unitType = CellUnitWorker.UnitType(xy);

                        if (CellUnitWorker.HaveOwner(xy) || CellUnitWorker.IsBot(xy))
                        {
                            CellSupVisBarsWorker.ActiveVision(true, SupportStaticTypes.Hp, xy);


                            float maxAmountHealth = CellUnitWorker.MaxAmountHealth(unitType);
                            float xCordinate = (float)CellUnitWorker.AmountHealth(xy) / maxAmountHealth;

                            CellSupVisBarsWorker.SetScale(SupportStaticTypes.Hp, new Vector3(xCordinate * 0.67f, 0.13f, 1), xy);

                            if (!CellUnitWorker.IsBot(xy))
                            {
                                if (CellUnitWorker.IsMasterClient(xy))
                                {
                                    CellSupVisBarsWorker.SetColor(SupportStaticTypes.Hp, Color.blue, xy);
                                }
                                else
                                {
                                    CellSupVisBarsWorker.SetColor(SupportStaticTypes.Hp, Color.red, xy);
                                }
                            }
                            else
                            {
                                CellSupVisBarsWorker.SetColor(SupportStaticTypes.Hp, Color.red, xy);
                            }
                        }


                        if (CellUnitWorker.IsProtectRelaxType(ConditionTypes.Protected, xy))
                        {
                            CellSupVisBlocksWorker.EnableCellSupVisBlocksSR(true, CellSupVisBlocksTypes.Condition, xy);
                            CellSupVisBlocksWorker.SetCellSupVisBlocksColor(Color.yellow, CellSupVisBlocksTypes.Condition, xy);
                        }

                        else if (CellUnitWorker.IsProtectRelaxType(ConditionTypes.Relaxed, xy))
                        {
                            CellSupVisBlocksWorker.EnableCellSupVisBlocksSR(true, CellSupVisBlocksTypes.Condition, xy);
                            CellSupVisBlocksWorker.SetCellSupVisBlocksColor(Color.green, CellSupVisBlocksTypes.Condition, xy);
                        }


                        if (CellUnitWorker.HaveMaxAmountSteps(xy))
                        {
                            CellSupVisBlocksWorker.EnableCellSupVisBlocksSR(true, CellSupVisBlocksTypes.MaxSteps, xy);
                        }
                        else
                        {
                            CellSupVisBlocksWorker.EnableCellSupVisBlocksSR(false, CellSupVisBlocksTypes.MaxSteps, xy);
                        }
                    }


                    if (SelectorWorker.IsUpgradeModType(UpgradeModTypes.Unit))
                    {
                        if (CellUnitWorker.IsUnitType(UnitTypes.Pawn, xy) || CellUnitWorker.IsUnitType(UnitTypes.Rook, xy) || CellUnitWorker.IsUnitType(UnitTypes.Bishop, xy))
                        {
                            if (CellUnitWorker.HaveOwner(xy))
                            {
                                if (CellUnitWorker.IsMine(xy))
                                {
                                    CellSupVisWorker.EnableSupVis(SupportVisionTypes.Upgrade, xy);
                                }
                            }
                        }
                    }
                }
            }

        if (SelectorWorker.IsSelectedCell)
        {
            CellSupVisWorker.EnableSupVis(SupportVisionTypes.Selector, XySelectedCell);
        }


        //foreach (var xyUnit in InfoUnitsWorker.GetLixtXyUnits(UnitTypes.King, true))
        //{
        //    Debug.Log(xyUnit[0]);
        //    Debug.Log(xyUnit[1]);
        //    CellSupportVisionWorker.EnableSupVis(SupportVisionTypes., xyUnit);
        //}




        //InfoUnitsWorker.Get


        AvailableCellsEntsWorker.GetAllCells(AvailableCellTypes.Shift).ForEach((xy) => CellSupVisWorker.EnableSupVis(SupportVisionTypes.Shift, xy));
        AvailableCellsEntsWorker.GetAllCells(AvailableCellTypes.SimpleAttack).ForEach((xy) => CellSupVisWorker.EnableSupVis(SupportVisionTypes.SimpleAttack, xy));
        AvailableCellsEntsWorker.GetAllCells(AvailableCellTypes.UniqueAttack).ForEach((xy) => CellSupVisWorker.EnableSupVis(SupportVisionTypes.UniqueAttack, xy));
    }
}
