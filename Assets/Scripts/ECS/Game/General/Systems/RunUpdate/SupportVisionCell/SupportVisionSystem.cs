using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using UnityEngine;
using static Assets.Scripts.Main;

internal sealed class SupportVisionSystem : SystemGeneralReduction
{
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;

    public override void Run()
    {
        base.Run();

        _eGM.CellSupVisEnt_CellSupVisCom(_eGM.SelectorEnt_SelectorCom.XyPreviousCell).ActiveVision(false, SupportVisionTypes.Selector);


        if (_eGM.CellEnt_CellBaseCom(XySelectedCell).IsSelected)
            _eGM.CellSupVisEnt_CellSupVisCom(XySelectedCell).ActiveVision(true, SupportVisionTypes.Selector);

        else _eGM.CellSupVisEnt_CellSupVisCom(XySelectedCell).ActiveVision(false, SupportVisionTypes.Selector);


        for (int x = 0; x < _eGM.Xamount; x++)
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                if (_eGM.SelectorEnt_UnitTypeCom.HaveUnit)
                {
                    if (!_eGM.CellEnt_CellBaseCom(x, y).IsSelected && !_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveUnit
                        && !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveMountain)
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

                else if (!_eGM.CellEnt_CellBaseCom(x, y).IsSelected)
                {
                    _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.Spawn);
                }


                if (_eGM.CellUnitEnt_CellUnitCom(x, y).IsActivatedUnitDict[Instance.IsMasterClient])
                {
                    if (_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveUnit)
                    {
                        if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner || _eGM.CellUnitEnt_CellOwnerBotCom(x, y).HaveBot)
                        {
                            _eGM.CellSupStatEnt_CellSupStatCom(x, y).ActiveVision(true, SupportStaticTypes.Hp);

                            float xCordinate = (float)_eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth / (float)CellUnitWorker.MaxAmountHealth(x, y);
                            _eGM.CellSupStatEnt_CellSupStatCom(x, y).SetScale(SupportStaticTypes.Hp, new Vector3(xCordinate * 0.67f, 0.13f, 1));


                            if(!_eGM.CellUnitEnt_CellOwnerBotCom(x, y).HaveBot)
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



                        if (_eGM.CellUnitEnt_CellUnitCom(x, y).IsProtected)
                        {
                            _eGM.CellUnitEnt_CellUnitCom(x, y).EnableDefendRelaxSR(true);
                            _eGM.CellUnitEnt_CellUnitCom(x, y).SetColorDefendRelaxSR(Color.yellow);
                        }
                        else if (_eGM.CellUnitEnt_CellUnitCom(x, y).IsRelaxed)
                        {
                            _eGM.CellUnitEnt_CellUnitCom(x, y).EnableDefendRelaxSR(true);
                            _eGM.CellUnitEnt_CellUnitCom(x, y).SetColorDefendRelaxSR(Color.green);
                        }
                        else
                        {
                            _eGM.CellUnitEnt_CellUnitCom(x, y).EnableDefendRelaxSR(false);
                        }


                        if (CellUnitWorker.HaveMaxSteps(x, y))
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

                    

                    if (_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType != UnitTypes.King)
                    {
                        if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner)
                        {
                            if (_eGM.CellUnitEnt_CellOwnerCom(x, y).IsMine)
                            {
                                switch (_eGM.SelectorEnt_SelectorCom.UpgradeModType)
                                {
                                    case UpgradeModTypes.None:
                                        _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.Upgrade);
                                        break;

                                    case UpgradeModTypes.Unit:
                                        _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(true, SupportVisionTypes.Upgrade);
                                        break;

                                    case UpgradeModTypes.Building:
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }

                        else
                        {
                            _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.Upgrade);
                        }
                    }
                }
                else
                {
                    _eGM.CellSupStatEnt_CellSupStatCom(x, y).ActiveVision(false, SupportStaticTypes.Hp);
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
