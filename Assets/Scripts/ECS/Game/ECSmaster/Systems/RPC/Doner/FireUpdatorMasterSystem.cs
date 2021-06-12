internal sealed class FireUpdatorMasterSystem : SystemMasterReduction
{

    public override void Run()
    {
        base.Run();


        for (int x = 0; x < _eGM.Xamount; x++)
        {
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                if (_eGM.CellEffectEnt_CellEffectCom(x, y).HaveFire)
                {
                    _eGM.CellEffectEnt_CellEffectCom(x, y).TimeFire += 1;

                    _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth -= 40;
                    if (!_eGM.CellUnitEnt_CellUnitCom(x, y).HaveHealth)
                        _cM.CellUnitWorker.ResetUnit(x, y);

                    if (_eGM.CellEffectEnt_CellEffectCom(x, y).TimeFire >= 3)
                    {
                        if(_eGM.CellBuildEnt_BuilTypeCom(x, y).HaveBuilding)
                        {
                            _cM.CellBuildingWorker.ResetBuilding(x, y);
                        }


                        _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.AdultForest);

                        _eGM.CellEffectEnt_CellEffectCom(x, y).SetResetEffect(false, EffectTypes.Fire);


                        var aroundXYList = _cM.CellUnitWorker.TryGetXYAround(x, y);
                        foreach (var xy in aroundXYList)
                        {
                            if (_eGM.CellEnvEnt_CellEnvCom(xy).HaveAdultTree)
                            {
                                _eGM.CellEffectEnt_CellEffectCom(xy).SetResetEffect(true, EffectTypes.Fire);
                            }
                        }

                        _eGM.CellEffectEnt_CellEffectCom(x, y).TimeFire = 0;
                    }

                }
            }
        }
    }
}
