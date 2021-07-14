using Assets.Scripts;
using Assets.Scripts.Static;

internal sealed class FireUpdatorMasterSystem : SystemMasterReduction
{

    public override void Run()
    {
        base.Run();


        for (int x = 0; x < _eGM.Xamount; x++)
        {
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                if (_eGM.CellEffectEnt_CellEffectCom(x, y).HaveEffect(EffectTypes.Fire))
                {
                    _eGM.CellEffectEnt_CellEffectCom(x, y).AddTimeStepsEffect(EffectTypes.Fire);

                    _eGM.CellUnitEnt_CellUnitCom(x, y).TakeAmountHealth(40);
                    if (!_eGM.CellUnitEnt_CellUnitCom(x, y).HaveHealth)
                    {
                        if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner)
                        {
                            CellUnitWorker.ResetPlayerUnit(true, x, y);
                        }
                        else
                        {
                            CellUnitWorker.ResetBotUnit(x, y);
                        }

                    }


                    if (_eGM.CellEffectEnt_CellEffectCom(x, y).TimeStepsEffect(EffectTypes.Fire) >= 3)
                    {
                        if (_eGM.CellBuildEnt_BuilTypeCom(x, y).HaveBuilding)
                        {
                            CellBuildingWorker.ResetBuilding(true, x, y);
                        }


                        _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.AdultForest);

                        _eGM.CellEffectEnt_CellEffectCom(x, y).ResetEffect(EffectTypes.Fire);


                        var aroundXYList = CellUnitWorker.TryGetXYAround(x, y);
                        foreach (var xy in aroundXYList)
                        {
                            if (_eGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.AdultForest))
                            {
                                _eGM.CellEffectEnt_CellEffectCom(xy).SetEffect(EffectTypes.Fire);
                            }
                        }

                        _eGM.CellEffectEnt_CellEffectCom(x, y).SetTimeStepsEffect(EffectTypes.Fire, 0);
                    }

                }
            }
        }
    }
}
