using Assets.Scripts;
using System.Collections.Generic;
using static Assets.Scripts.Main;


internal sealed class VisibilityUnitsMasterSystem : SystemGeneralReduction
{

    public override void Run()
    {
        base.Run();


        for (int x = 0; x < _eGM.Xamount; x++)
        {
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                _eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).SetActivated(true, true);
                _eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).SetActivated(false, true);



                if (_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveAnyUnit)
                {
                    if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner)
                    {
                        if (_eGM.CellUnitEnt_CellOwnerCom(x, y).IsHim(Instance.MasterClient))
                        {
                            if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.AdultForest))
                            {
                                _eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).SetActivated(false, false);

                                List<int[]> list = CellUnitWorker.TryGetXYAround(x, y);
                                foreach (var xy in list)
                                {
                                    if (_eGM.CellUnitEnt_UnitTypeCom(xy).HaveAnyUnit)
                                    {
                                        if (_eGM.CellUnitEnt_CellOwnerCom(xy).HaveOwner)
                                        {
                                            if (!_eGM.CellUnitEnt_CellOwnerCom(xy).IsHim(Instance.MasterClient))
                                            {
                                                _eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).SetActivated(false, true);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.AdultForest))
                            {
                                _eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).SetActivated(true, false);

                                List<int[]> list = CellUnitWorker.TryGetXYAround(x, y);
                                foreach (var xy in list)
                                {
                                    if (_eGM.CellUnitEnt_UnitTypeCom(xy).HaveAnyUnit)
                                    {
                                        if (_eGM.CellUnitEnt_CellOwnerCom(xy).HaveOwner)
                                        {
                                            if (_eGM.CellUnitEnt_CellOwnerCom(xy).IsHim(Instance.MasterClient))
                                            {
                                                _eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).SetActivated(true, true);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        var isActivatedVisionCellUnit = _eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).IsActivated(Instance.IsMasterClient);

                        if (isActivatedVisionCellUnit)
                        {
                            var unitType = _eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType;
                            _eGM.CellUnitEnt_CellUnitCom(x, y).EnablePlayerSRAndSetColor(unitType, _eGM.CellUnitEnt_CellOwnerCom(x, y).Owner);
                        }
                        else
                        {
                            _eGM.CellUnitEnt_CellUnitCom(x, y).SwitchSR(false, _eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType);
                        }
                    }

                    else if (_eGM.CellUnitEnt_CellOwnerBotCom(x, y).HaveBot)
                    {
                        if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.AdultForest))
                        {
                            _eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).SetActivated(true, false);// IsActivatedUnitDict[true] = false;

                            List<int[]> list = CellUnitWorker.TryGetXYAround(x, y);
                            foreach (var xy in list)
                            {
                                if (_eGM.CellUnitEnt_UnitTypeCom(xy).HaveAnyUnit)
                                {
                                    if (_eGM.CellUnitEnt_CellOwnerCom(xy).HaveOwner)
                                    {
                                        if (_eGM.CellUnitEnt_CellOwnerCom(xy).IsHim(Instance.MasterClient))
                                        {
                                            _eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).SetActivated(true, true);//.IsActivatedUnitDict[true] = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        var isActivatedVisionCellUnit = _eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).IsActivated(Instance.IsMasterClient);

                        if (isActivatedVisionCellUnit)
                        {
                            var unitType = _eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType;
                            _eGM.CellUnitEnt_CellUnitCom(x, y).EnablePlayerSRAndSetColor(unitType, _eGM.CellUnitEnt_CellOwnerCom(x, y).Owner);
                        }
                        else
                        {
                            _eGM.CellUnitEnt_CellUnitCom(x, y).SwitchSR(false, _eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType);
                        }
                    }
                }
            }
        }
    }
}