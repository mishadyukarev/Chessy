using Leopotam.Ecs;
using System.Collections.Generic;
using static MainGame;


internal class VisibilityUnitsMasterSystem : CellGeneralReduction, IEcsRunSystem
{
    internal VisibilityUnitsMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public void Run()
    {
        for (int x = 0; x < _eGM.Xcount; x++)
        {
            for (int y = 0; y < _eGM.Ycount; y++)
            {
                _eGM.CellUnitComponent(x, y).IsActiveUnitMaster = true;
                _eGM.CellUnitComponent(x, y).IsActiveUnitOther = true;



                if (_eGM.CellUnitComponent(x, y).HaveUnit)
                {
                    if (_eGM.CellUnitComponent(x, y).IsHisUnit(InstanceGame.MasterClient))
                    {
                        if (_eGM.CellEnvironmentComponent(x, y).HaveTree)
                        {
                            _eGM.CellUnitComponent(x, y).IsActiveUnitOther = false;

                            List<int[]> list = InstanceGame.CellManager.CellFinderWay.TryGetXYAround(new int[] { x, y });
                            foreach (var xy in list)
                            {
                                if (_eGM.CellUnitComponent(xy).HaveUnit)
                                {
                                    if (!_eGM.CellUnitComponent(xy).IsHisUnit(InstanceGame.MasterClient))
                                    {
                                        _eGM.CellUnitComponent(x, y).IsActiveUnitOther = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (_eGM.CellEnvironmentComponent(x, y).HaveTree)
                        {
                            _eGM.CellUnitComponent(x, y).IsActiveUnitMaster = false;

                            List<int[]> list = InstanceGame.CellManager.CellFinderWay.TryGetXYAround(new int[] { x, y });
                            foreach (var xy in list)
                            {
                                if (_eGM.CellUnitComponent(xy).HaveUnit)
                                {
                                    if (_eGM.CellUnitComponent(xy).IsHisUnit(InstanceGame.MasterClient))
                                    {
                                        _eGM.CellUnitComponent(x, y).IsActiveUnitMaster = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }


                _eGM.CellUnitComponent(x, y).ActiveVisionCell(_eGM.CellUnitComponent(x, y).IsActiveUnitMaster, _eGM.CellUnitComponent(x, y).UnitType);
            }
        }
    }
}