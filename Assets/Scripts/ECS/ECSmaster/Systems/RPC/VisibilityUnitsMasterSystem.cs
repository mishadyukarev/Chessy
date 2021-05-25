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
                _eGM.CellEnt_CellUnitCom(x, y).SetActiveUnit(true, true);
                _eGM.CellEnt_CellUnitCom(x, y).SetActiveUnit(false, true);



                if (_eGM.CellEnt_CellUnitCom(x, y).HaveUnit)
                {
                    if (_eGM.CellEnt_CellUnitCom(x, y).IsHim(Instance.MasterClient))
                    {
                        if (_eGM.CellEnt_CellEnvCom(x, y).HaveAdultTree)
                        {
                            _eGM.CellEnt_CellUnitCom(x, y).SetActiveUnit(false, false);

                            List<int[]> list = _eGM.CellEnt_CellUnitCom(x, y).TryGetXYAround();
                            foreach (var xy in list)
                            {
                                if (_eGM.CellEnt_CellUnitCom(xy).HaveUnit)
                                {
                                    if (!_eGM.CellEnt_CellUnitCom(xy).IsHim(Instance.MasterClient))
                                    {
                                        _eGM.CellEnt_CellUnitCom(x, y).SetActiveUnit(false, true);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (_eGM.CellEnt_CellEnvCom(x, y).HaveAdultTree)
                        {
                            _eGM.CellEnt_CellUnitCom(x, y).SetActiveUnit(true, false);

                            List<int[]> list = _eGM.CellEnt_CellUnitCom(x, y).TryGetXYAround();
                            foreach (var xy in list)
                            {
                                if (_eGM.CellEnt_CellUnitCom(xy).HaveUnit)
                                {
                                    if (_eGM.CellEnt_CellUnitCom(xy).IsHim(Instance.MasterClient))
                                    {
                                        _eGM.CellEnt_CellUnitCom(x, y).SetActiveUnit(true, true);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }


                _eGM.CellEnt_CellUnitCom(x, y).ActiveVisionCell(_eGM.CellEnt_CellUnitCom(x, y).GetActiveUnit(Instance.IsMasterClient), _eGM.CellEnt_CellUnitCom(x, y).UnitType);
            }
        }
    }
}