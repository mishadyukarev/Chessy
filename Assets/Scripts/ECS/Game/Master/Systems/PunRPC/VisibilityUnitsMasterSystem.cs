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
                _eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).SetIsActivated(true, true);
                _eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).SetIsActivated(false, true);



                if (_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveUnit)
                {
                    if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner)
                    {
                        if (_eGM.CellUnitEnt_CellOwnerCom(x, y).IsHim(Instance.MasterClient))
                        {
                            if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.AdultForest))
                            {
                                _eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).SetIsActivated(false, false);

                                List<int[]> list = CellUnitWorker.TryGetXYAround(x, y);
                                foreach (var xy in list)
                                {
                                    if (_eGM.CellUnitEnt_UnitTypeCom(xy).HaveUnit)
                                    {
                                        if (_eGM.CellUnitEnt_CellOwnerCom(xy).HaveOwner)
                                        {
                                            if (!_eGM.CellUnitEnt_CellOwnerCom(xy).IsHim(Instance.MasterClient))
                                            {
                                                _eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).SetIsActivated(false, true);
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
                                _eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).SetIsActivated(true, false);

                                List<int[]> list = CellUnitWorker.TryGetXYAround(x, y);
                                foreach (var xy in list)
                                {
                                    if (_eGM.CellUnitEnt_UnitTypeCom(xy).HaveUnit)
                                    {
                                        if (_eGM.CellUnitEnt_CellOwnerCom(xy).HaveOwner)
                                        {
                                            if (_eGM.CellUnitEnt_CellOwnerCom(xy).IsHim(Instance.MasterClient))
                                            {
                                                _eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).SetIsActivated(true, true);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        _eGM.CellUnitEnt_CellUnitCom(x, y).EnablePlayerSR(_eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).IsActivated(Instance.IsMasterClient), _eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType, _eGM.CellUnitEnt_CellOwnerCom(x, y).Owner);
                    }

                    else if (_eGM.CellUnitEnt_CellOwnerBotCom(x, y).HaveBot)
                    {
                        if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.AdultForest))
                        {
                            _eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).SetIsActivated(true, false);// IsActivatedUnitDict[true] = false;

                            List<int[]> list = CellUnitWorker.TryGetXYAround(x, y);
                            foreach (var xy in list)
                            {
                                if (_eGM.CellUnitEnt_UnitTypeCom(xy).HaveUnit)
                                {
                                    if (_eGM.CellUnitEnt_CellOwnerCom(xy).HaveOwner)
                                    {
                                        if (_eGM.CellUnitEnt_CellOwnerCom(xy).IsHim(Instance.MasterClient))
                                        {
                                            _eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).SetIsActivated(true, true);//.IsActivatedUnitDict[true] = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        _eGM.CellUnitEnt_CellUnitCom(x, y).EnableBotSR(_eGM.CellUnitEnt_ActivatedForPlayersCom(x, y).IsActivated(Instance.IsMasterClient), _eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType);
                    }
                }
            }
        }
    }
}