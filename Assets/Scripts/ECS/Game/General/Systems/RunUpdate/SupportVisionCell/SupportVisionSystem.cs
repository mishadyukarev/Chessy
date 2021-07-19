using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Static;
using Assets.Scripts.Static.Cell;
using UnityEngine;
using static Assets.Scripts.Main;
using static Assets.Scripts.Static.Cell.CellSupportVisionWorker;
using static Assets.Scripts.Static.CellBaseOperations;

internal sealed class SupportVisionSystem : SystemGeneralReduction
{
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.GetXy(SelectorCellTypes.Selected);

    public override void Run()
    {
        base.Run();

        for (int x = 0; x < _eGM.Xamount; x++)
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                DisableSupVis(x, y);

                if (_eGM.SelectorEnt_UnitTypeCom.HaveAnyUnit)
                {
                    if (Instance.IsMasterClient)
                    {
                        if (_eGM.CellEnt_CellBaseCom(x, y).IsStartedCell(Instance.IsMasterClient))
                        {
                            EnableSupVis(SupportVisionTypes.Spawn, x, y);
                        }
                    }

                    else
                    {
                        if (_eGM.CellEnt_CellBaseCom(x, y).IsStartedCell(Instance.IsMasterClient))
                        {
                            EnableSupVis(SupportVisionTypes.Spawn, x, y);
                        }
                    }
                }


                if (_eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).IsActivated(Instance.IsMasterClient))
                {
                    if (_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveAnyUnit)
                    {
                        var unitType = _eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType;

                        if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner || _eGM.CellUnitEnt_CellOwnerBotCom(x, y).HaveBot)
                        {
                            _eGM.CellSupStatEnt_CellSupStatCom(x, y).ActiveVision(true, SupportStaticTypes.Hp);


                            float maxAmountHealth = CellUnitWorker.MaxAmountHealth(unitType);
                            float xCordinate = (float)_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth / maxAmountHealth;
                            _eGM.CellSupStatEnt_CellSupStatCom(x, y).SetScale(SupportStaticTypes.Hp, new Vector3(xCordinate * 0.67f, 0.13f, 1));


                            if (!_eGM.CellUnitEnt_CellOwnerBotCom(x, y).HaveBot)
                            {
                                if (_eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient)
                                {
                                    _eGM.CellSupStatEnt_CellSupStatCom(x, y).SetColor(SupportStaticTypes.Hp, Color.blue);
                                }
                                else
                                {
                                    _eGM.CellSupStatEnt_CellSupStatCom(x, y).SetColor(SupportStaticTypes.Hp, Color.red);
                                }
                            }
                            else
                            {
                                _eGM.CellSupStatEnt_CellSupStatCom(x, y).SetColor(SupportStaticTypes.Hp, Color.red);
                            }
                        }


                        if (_eGM.CellUnitEnt_ProtectRelaxCom(x, y).IsType(ProtectRelaxTypes.Protected))
                        {
                            //_eGM.CellUnitEnt_CellUnitCom(x, y).EnableDefendRelaxSR(true);
                            //_eGM.CellUnitEnt_CellUnitCom(x, y).SetColorDefendRelaxSR(Color.yellow);
                        }

                        else if (_eGM.CellUnitEnt_ProtectRelaxCom(x, y).IsType(ProtectRelaxTypes.Relaxed))
                        {
                            //_eGM.CellUnitEnt_CellUnitCom(x, y).EnableDefendRelaxSR(true);
                            //_eGM.CellUnitEnt_CellUnitCom(x, y).SetColorDefendRelaxSR(Color.green);
                        }


                        if (_eGM.CellUnitEnt_CellUnitCom(x, y).HaveMaxSteps(unitType))
                        {
                            //_eGM.CellUnitEnt_CellUnitCom(x, y).EnableStandartColorSR(true);
                        }
                    }


                    if (_eGM.SelectorEnt_UpgradeModTypeCom.IsUpgradeModType(UpgradeModTypes.Unit))
                    {
                        if (_eGM.CellUnitEnt_UnitTypeCom(x, y).IsUnit(UnitTypes.Pawn)|| _eGM.CellUnitEnt_UnitTypeCom(x, y).IsUnit(UnitTypes.Rook) || _eGM.CellUnitEnt_UnitTypeCom(x, y).IsUnit(UnitTypes.Bishop))
                        {
                            if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner)
                            {
                                if (_eGM.CellUnitEnt_CellOwnerCom(x, y).IsMine)
                                {
                                    EnableSupVis(SupportVisionTypes.Upgrade, x, y);
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

        _eGM.SelectorEnt_SelectorCom.GetAllCells(AvailableCellTypes.Shift).ForEach((xy) => EnableSupVis(SupportVisionTypes.Shift, xy));
        _eGM.SelectorEnt_SelectorCom.GetAllCells(AvailableCellTypes.SimpleAttack).ForEach((xy) => EnableSupVis(SupportVisionTypes.SimpleAttack, xy));
        _eGM.SelectorEnt_SelectorCom.GetAllCells(AvailableCellTypes.UniqueAttack).ForEach((xy) => EnableSupVis(SupportVisionTypes.UniqueAttack, xy));
    }
}
