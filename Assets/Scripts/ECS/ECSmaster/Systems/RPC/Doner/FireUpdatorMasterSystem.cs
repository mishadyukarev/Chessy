using Leopotam.Ecs;
using Photon.Pun;

internal class FireUpdatorMasterSystem : SystemMasterReduction, IEcsRunSystem
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

                    if (_eGM.CellEnt_CellEffectCom(x, y).TimeFire >= 2)
                    {
                        _eGM.CellEnt_CellUnitCom(x, y).AmountHealth -= 40;
                        if (!_eGM.CellEnt_CellUnitCom(x, y).HaveHealth)
                        {
                            _eGM.CellEnt_CellUnitCom(x, y).ResetUnit();
                        }
                        if (_eGM.CellEnt_CellEffectCom(x, y).TimeFire >= 3)
                        {
                            switch (_eGM.CellEnt_CellBuildingCom(x,y).BuildingType)
                            {
                                case BuildingTypes.None:
                                    break;

                                case BuildingTypes.City:
                                    break;

                                case BuildingTypes.Farm:
                                    _eGM.InfoEnt_BuildingsInfoCom.AmountFarmDict[_eGM.CellEnt_CellBuildingCom(x,y).IsMasterClient] -= 1;
                                    break;

                                case BuildingTypes.Woodcutter:
                                    _eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterClient] -= 1;
                                    break;

                                case BuildingTypes.Mine:
                                    break;

                                default:
                                    break;
                            }
                            _eGM.CellEnt_CellBuildingCom(x, y).ResetBuilding();
                            _eGM.CellEnt_CellEnvCom(x, y).SetResetEnvironment(false, EnvironmentTypes.Fertilizer);
                            _eGM.CellEnt_CellEnvCom(x, y).SetResetEnvironment(false, EnvironmentTypes.AdultForest);
                            _eGM.CellEnt_CellEnvCom(x, y).AmountResourcesForest = 0;

                            _eGM.CellEnt_CellEffectCom(x, y).SetEffect(false, EffectTypes.Fire);


                            var aroundXYList = _eGM.CellEnt_CellUnitCom(x, y).TryGetXYAround();
                            foreach (var xy in aroundXYList)
                            {
                                if (_eGM.CellEnt_CellEnvCom(xy).HaveAdultTree)
                                {
                                    _eGM.CellEnt_CellEffectCom(xy).SetEffect(true, EffectTypes.Fire);
                                }
                            }

                            _eGM.CellEnt_CellEffectCom(x, y).TimeFire = 0;
                        }
                    }
                }
            }
        }
    }
}
