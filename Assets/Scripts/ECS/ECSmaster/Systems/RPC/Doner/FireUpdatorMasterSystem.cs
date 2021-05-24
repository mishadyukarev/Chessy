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
                if (_eGM.CellEffectEnt_CellEffectCom(x, y).HaveFire)
                {
                    _eGM.CellEffectEnt_CellEffectCom(x, y).TimeFire += 1;

                    if (_eGM.CellEffectEnt_CellEffectCom(x, y).TimeFire >= 2)
                    {
                        _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth -= 40;
                        if (!_eGM.CellUnitEnt_CellUnitCom(x, y).HaveHealth)
                        {
                            _eGM.CellUnitEnt_CellUnitCom(x, y).ResetUnit();
                        }
                        if (_eGM.CellEffectEnt_CellEffectCom(x, y).TimeFire >= 3)
                        {
                            switch (_eGM.CellBuildingEnt_BuildingTypeCom(x,y).BuildingType)
                            {
                                case BuildingTypes.None:
                                    break;

                                case BuildingTypes.City:
                                    break;

                                case BuildingTypes.Farm:
                                    _eGM.InfoEnt_BuildingsInfoCom.AmountFarmDict[_eGM.CellBuildingEnt_OwnerCom(x,y).IsMasterClient] -= 1;
                                    break;

                                case BuildingTypes.Woodcutter:
                                    _eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[_eGM.CellBuildingEnt_OwnerCom(x, y).IsMasterClient] -= 1;
                                    break;

                                case BuildingTypes.Mine:
                                    break;

                                default:
                                    break;
                            }
                            _eGM.CellBuildingEnt_CellBuildingCom(x, y).ResetBuilding();
                            _eGM.CellEnvEnt_CellEnvironmentCom(x, y).SetResetEnvironment(false, EnvironmentTypes.Food);
                            _eGM.CellEnvEnt_CellEnvironmentCom(x, y).SetResetEnvironment(false, EnvironmentTypes.Tree);

                            _eGM.CellEffectEnt_CellEffectCom(x, y).SetEffect(false, EffectTypes.Fire);


                            var aroundXYList = _eGM.CellUnitEnt_CellUnitCom(x, y).TryGetXYAround();
                            foreach (var xy in aroundXYList)
                            {
                                if (_eGM.CellEnvEnt_CellEnvironmentCom(xy).HaveTree)
                                {
                                    _eGM.CellEffectEnt_CellEffectCom(xy).SetEffect(true, EffectTypes.Fire);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
