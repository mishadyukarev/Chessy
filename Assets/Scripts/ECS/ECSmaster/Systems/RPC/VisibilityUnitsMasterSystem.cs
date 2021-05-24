using Leopotam.Ecs;
using System.Collections.Generic;
using static MainGame;


internal class VisibilityUnitsMasterSystem : SystemGeneralReduction, IEcsRunSystem
{
    internal VisibilityUnitsMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public override void Run()
    {
        base.Run();


        for (int x = 0; x < _eGM.Xamount; x++)
        {
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                _eGM.CellUnitEnt_CellUnitCom(x, y).SetActiveUnit(true, true);
                _eGM.CellUnitEnt_CellUnitCom(x, y).SetActiveUnit(false, true);



                if (_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveUnit)
                {
                    if (_eGM.CellUnitEnt_OwnerCom(x, y).IsHim(Instance.MasterClient))
                    {
                        if (_eGM.CellEnvEnt_CellEnvironmentCom(x, y).HaveTree)
                        {
                            _eGM.CellUnitEnt_CellUnitCom(x, y).SetActiveUnit(false, false);

                            List<int[]> list = _eGM.CellUnitEnt_CellUnitCom(x, y).TryGetXYAround();
                            foreach (var xy in list)
                            {
                                if (_eGM.CellUnitEnt_UnitTypeCom(xy).HaveUnit)
                                {
                                    if (!_eGM.CellUnitEnt_OwnerCom(xy).IsHim(Instance.MasterClient))
                                    {
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).SetActiveUnit(false, true);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (_eGM.CellEnvEnt_CellEnvironmentCom(x, y).HaveTree)
                        {
                            _eGM.CellUnitEnt_CellUnitCom(x, y).SetActiveUnit(true, false);

                            List<int[]> list = _eGM.CellUnitEnt_CellUnitCom(x, y).TryGetXYAround();
                            foreach (var xy in list)
                            {
                                if (_eGM.CellUnitEnt_UnitTypeCom(xy).HaveUnit)
                                {
                                    if (_eGM.CellUnitEnt_OwnerCom(xy).IsHim(Instance.MasterClient))
                                    {
                                        _eGM.CellUnitEnt_CellUnitCom(x, y).SetActiveUnit(true, true);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }


                _eGM.CellUnitEnt_CellUnitCom(x, y).ActiveVisionCell(_eGM.CellUnitEnt_CellUnitCom(x, y).GetActiveUnit(Instance.IsMasterClient), _eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType);
            }
        }
    }
}