using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using UnityEngine;
using static Assets.Scripts.Main;
using static Assets.Scripts.Static.Cell.CellSupportVisionWorker;
using static Assets.Scripts.Static.Cell.CellSupportStaticWorker;
using static Assets.Scripts.CellUnitWorker;
using static Assets.Scripts.CellWorker;
using Assets.Scripts.Static;

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

                DisableSupVis(xy);

                if (_eGM.SelectorEnt_UnitTypeCom.HaveAnyUnit)
                {
                    if (Instance.IsMasterClient)
                    {
                        if (IsStartedCell(Instance.IsMasterClient, xy))
                        {
                            EnableSupVis(SupportVisionTypes.Spawn, xy);
                        }
                    }

                    else
                    {
                        if (IsStartedCell(Instance.IsMasterClient, xy))
                        {
                            EnableSupVis(SupportVisionTypes.Spawn, xy);
                        }
                    }
                }

                
                if (IsActivated(Instance.IsMasterClient, xy))
                {
                    if (HaveAnyUnitOnCell(xy))
                    {
                        var unitType = UnitType(xy);

                        if (HaveOwner(xy) || IsBotOnCell(xy))
                        {
                            ActiveVision(true, SupportStaticTypes.Hp, xy);


                            float maxAmountHealth = CellUnitWorker.MaxAmountHealth(unitType);
                            float xCordinate = (float)AmountHealth(xy) / maxAmountHealth;

                            SetScale(SupportStaticTypes.Hp, new Vector3(xCordinate * 0.67f, 0.13f, 1), xy);

                            if (!IsBotOnCell(xy))
                            {
                                if (IsMasterClient(xy))
                                {
                                    SetColor(SupportStaticTypes.Hp, Color.blue, xy);
                                }
                                else
                                {
                                    SetColor(SupportStaticTypes.Hp, Color.red, xy);
                                }
                            }
                            else
                            {
                                SetColor(SupportStaticTypes.Hp, Color.red, xy);
                            }
                        }


                        if (IsTypeProtectRelax(ProtectRelaxTypes.Protected, xy))
                        {
                            //_eGM.CellUnitEnt_CellUnitCom(x, y).EnableDefendRelaxSR(true);
                            //_eGM.CellUnitEnt_CellUnitCom(x, y).SetColorDefendRelaxSR(Color.yellow);
                        }

                        else if (IsTypeProtectRelax(ProtectRelaxTypes.Relaxed, xy))
                        {
                            //_eGM.CellUnitEnt_CellUnitCom(x, y).EnableDefendRelaxSR(true);
                            //_eGM.CellUnitEnt_CellUnitCom(x, y).SetColorDefendRelaxSR(Color.green);
                        }


                        if (HaveMaxAmountSteps(unitType, xy))
                        {
                            //_eGM.CellUnitEnt_CellUnitCom(x, y).EnableStandartColorSR(true);
                        }
                    }


                    if (_eGM.SelectorEnt_UpgradeModTypeCom.IsUpgradeModType(UpgradeModTypes.Unit))
                    {
                        if (IsUnitType(UnitTypes.Pawn, xy) || IsUnitType(UnitTypes.Rook, xy) || IsUnitType(UnitTypes.Bishop, xy))
                        {
                            if (HaveOwner(xy)) 
                            {
                                if (IsMine(xy))
                                {
                                    EnableSupVis(SupportVisionTypes.Upgrade, xy);
                                }
                            }
                        }
                    }
                }
            }

        if (_eGM.SelectorEnt_SelectorCom.IsSelected)
        {
            EnableSupVis(SupportVisionTypes.Selector, XySelectedCell);
        }

        SelectorWorker.GetAllCells(AvailableCellTypes.Shift).ForEach((xy) => EnableSupVis(SupportVisionTypes.Shift, xy));
        SelectorWorker.GetAllCells(AvailableCellTypes.SimpleAttack).ForEach((xy) => EnableSupVis(SupportVisionTypes.SimpleAttack, xy));
        SelectorWorker.GetAllCells(AvailableCellTypes.UniqueAttack).ForEach((xy) => EnableSupVis(SupportVisionTypes.UniqueAttack, xy));
    }
}
