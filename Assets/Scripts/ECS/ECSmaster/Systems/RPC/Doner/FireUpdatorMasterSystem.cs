using Leopotam.Ecs;

internal sealed class FireUpdatorMasterSystem : SystemMasterReduction
{
    internal FireUpdatorMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public override void Run()
    {
        base.Run();


        for (int x = 0; x < _eGM.Xamount; x++)
        {
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                if (_eGM.CellEnt_CellEffectCom(x, y).HaveFire)
                {
                    _eGM.CellEnt_CellEffectCom(x, y).TimeFire += 1;

                    _eGM.CellEnt_CellUnitCom(x, y).AmountHealth -= 40;
                    if (!_eGM.CellEnt_CellUnitCom(x, y).HaveHealth)
                        _eGM.CellEnt_CellUnitCom(x, y).ResetUnit();

                    if (_eGM.CellEnt_CellEffectCom(x, y).TimeFire >= 3)
                    {
                        _cellWorker.ResetBuilding(x, y);

                        _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.Fertilizer);
                        _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.AdultForest);

                        _eGM.CellEnt_CellEffectCom(x, y).SetResetEffect(false, EffectTypes.Fire);


                        var aroundXYList = _eGM.CellEnt_CellUnitCom(x, y).TryGetXYAround();
                        foreach (var xy in aroundXYList)
                        {
                            if (_eGM.CellEnvEnt_CellEnvCom(xy).HaveAdultTree)
                            {
                                _eGM.CellEnt_CellEffectCom(xy).SetResetEffect(true, EffectTypes.Fire);
                            }
                        }

                        _eGM.CellEnt_CellEffectCom(x, y).TimeFire = 0;
                    }

                }
            }
        }
    }
}
