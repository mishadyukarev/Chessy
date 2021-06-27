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
                _eGM.CellUnitEnt_CellUnitCom(x, y).IsActivatedUnitDict[true] = true;
                _eGM.CellUnitEnt_CellUnitCom(x, y).IsActivatedUnitDict[false] = true;



                if (_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveUnit)
                {
                    if (_eGM.CellUnitEnt_CellOwnerCom(x, y).IsHim(Instance.MasterClient))
                    {
                        if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveAdultTree)
                        {
                            _eGM.CellUnitEnt_CellUnitCom(x, y).IsActivatedUnitDict[false] = false;

                            List<int[]> list = _cellM.CellUnitWorker.TryGetXYAround(x, y);
                            foreach (var xy in list)
                            {
                                if (_eGM.CellUnitEnt_UnitTypeCom(xy).HaveUnit)
                                {
                                    if (!_eGM.CellUnitEnt_CellOwnerCom(xy).IsHim(Instance.MasterClient))
                                    {
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).IsActivatedUnitDict[false] = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveAdultTree)
                        {
                            _eGM.CellUnitEnt_CellUnitCom(x, y).IsActivatedUnitDict[true] = false;

                            List<int[]> list = _cellM.CellUnitWorker.TryGetXYAround(x, y);
                            foreach (var xy in list)
                            {
                                if (_eGM.CellUnitEnt_UnitTypeCom(xy).HaveUnit)
                                {
                                    if (_eGM.CellUnitEnt_CellOwnerCom(xy).IsHim(Instance.MasterClient))
                                    {
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).IsActivatedUnitDict[true] = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }


                _eGM.CellUnitEnt_CellUnitCom(x, y).EnableSR(_eGM.CellUnitEnt_CellUnitCom(x, y).IsActivatedUnitDict[Instance.IsMasterClient], _eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType);
            }
        }
    }
}