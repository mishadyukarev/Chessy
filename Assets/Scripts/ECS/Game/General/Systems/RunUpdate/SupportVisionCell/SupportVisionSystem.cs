using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Static;
using UnityEngine;
using static Assets.Scripts.Main;

internal sealed class SupportVisionSystem : SystemGeneralReduction
{
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;

    public override void Run()
    {
        base.Run();

        _eGM.CellSupVisEnt_CellSupVisCom(_eGM.SelectorEnt_SelectorCom.XyPreviousCell).ActiveVision(false, SupportVisionTypes.Selector);


        if (_eGM.SelectorEnt_SelectorCom.IsSelected)
            _eGM.CellSupVisEnt_CellSupVisCom(XySelectedCell).ActiveVision(true, SupportVisionTypes.Selector);

        else _eGM.CellSupVisEnt_CellSupVisCom(XySelectedCell).ActiveVision(false, SupportVisionTypes.Selector);

        for (int x = 0; x < _eGM.Xamount; x++)
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                if (_eGM.SelectorEnt_UnitTypeCom.HaveUnit)
                {
                    if (!CellBaseOperations.CompareXy(new int[] { x, y }, _eGM.SelectorEnt_SelectorCom.XySelectedCell)/*!_eGM.CellEnt_CellBaseCom(x, y).IsSelected*/ && !_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveUnit
                        && !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.Mountain))
                    {
                        if (Instance.IsMasterClient)
                        {
                            if (_eGM.CellEnt_CellBaseCom(x, y).IsStartedCell(true))
                            {
                                _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(true, SupportVisionTypes.Spawn);
                            }
                        }

                        else
                        {
                            if (_eGM.CellEnt_CellBaseCom(x, y).IsStartedCell(false))
                            {
                                _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(true, SupportVisionTypes.Spawn);
                            }
                        }
                    }
                }

                else if (!CellBaseOperations.CompareXy(new int[] { x, y }, _eGM.SelectorEnt_SelectorCom.XySelectedCell) /*_eGM.CellEnt_CellBaseCom(x, y).IsSelected*/)
                {
                    _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.Spawn);
                }


                if (_eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).IsActivated(Instance.IsMasterClient))
                {
                    if (_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveUnit)
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


                        if (_eGM.CellUnitEnt_ProtectRelaxCom(x, y).IsProtected)
                        {
                            _eGM.CellUnitEnt_CellUnitCom(x, y).EnableDefendRelaxSR(true);
                            _eGM.CellUnitEnt_CellUnitCom(x, y).SetColorDefendRelaxSR(Color.yellow);
                        }

                        else if (_eGM.CellUnitEnt_ProtectRelaxCom(x, y).IsRelaxed)
                        {
                            _eGM.CellUnitEnt_CellUnitCom(x, y).EnableDefendRelaxSR(true);
                            _eGM.CellUnitEnt_CellUnitCom(x, y).SetColorDefendRelaxSR(Color.green);
                        }

                        else
                        {
                            _eGM.CellUnitEnt_CellUnitCom(x, y).EnableDefendRelaxSR(false);
                        }


                        if (_eGM.CellUnitEnt_CellUnitCom(x, y).HaveMaxSteps(unitType))
                        {
                            _eGM.CellUnitEnt_CellUnitCom(x, y).EnableStandartColorSR(true);
                        }

                        else
                        {
                            _eGM.CellUnitEnt_CellUnitCom(x, y).EnableStandartColorSR(false);
                        }
                    }

                    else
                    {
                        _eGM.CellSupStatEnt_CellSupStatCom(x, y).ActiveVision(false, SupportStaticTypes.Hp);
                    }




                    if (_eGM.SelectorEnt_SelectorCom.UpgradeModType == UpgradeModTypes.Unit)
                    {
                        if (_eGM.CellUnitEnt_UnitTypeCom(x, y).IsPawn || _eGM.CellUnitEnt_UnitTypeCom(x, y).IsRook || _eGM.CellUnitEnt_UnitTypeCom(x, y).IsBishop)
                        {
                            if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner)
                            {
                                if (_eGM.CellUnitEnt_CellOwnerCom(x, y).IsMine)
                                {
                                    _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(true, SupportVisionTypes.Upgrade);
                                }

                                else
                                {
                                    _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.Upgrade);
                                }
                            }

                            else
                            {
                                _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.Upgrade);
                            }
                        }

                        else
                        {
                            _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.Upgrade);
                        }
                    }
                    else
                    {
                        _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.Upgrade);
                    }


                    //        switch (_eGM.SelectorEnt_SelectorCom.UpgradeModType)
                    //        {
                    //            case UpgradeModTypes.None:
                    //                _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.Upgrade);
                    //                break;

                    //            case UpgradeModTypes.Unit:
                    //                _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(true, SupportVisionTypes.Upgrade);
                    //                break;

                    //            case UpgradeModTypes.Building:
                    //                break;

                    //            default:
                    //                break;
                    //        }
                    //    }
                    //}

                    //else
                    //{
                    //    _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.Upgrade);
                    //}
                    //    }
                    //}
                    //else
                    //{
                    //    _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.Upgrade);
                    //    _eGM.CellSupStatEnt_CellSupStatCom(x, y).ActiveVision(false, SupportStaticTypes.Hp);
                    //}
                }
            }



        for (int x = 0; x < _eGM.Xamount; x++)
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.WayUnit);
                _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.SimpleAttack);
                _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.UniqueAttack);



            }


        foreach (var xy in _eGM.SelectorEnt_SelectorCom.AvailableCellsForShift)
            _eGM.CellSupVisEnt_CellSupVisCom(xy).ActiveVision(true, SupportVisionTypes.WayUnit);

        foreach (var xy in _eGM.SelectorEnt_SelectorCom.AvailableCellsSimpleAttack)
            _eGM.CellSupVisEnt_CellSupVisCom(xy).ActiveVision(true, SupportVisionTypes.SimpleAttack);

        foreach (var xy in _eGM.SelectorEnt_SelectorCom.AvailableCellsUniqueAttack)
            _eGM.CellSupVisEnt_CellSupVisCom(xy).ActiveVision(true, SupportVisionTypes.UniqueAttack);
    }
}
