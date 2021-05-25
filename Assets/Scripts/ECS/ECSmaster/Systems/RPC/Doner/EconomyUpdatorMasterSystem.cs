using static MainGame;

internal sealed class EconomyUpdatorMasterSystem : SystemMasterReduction
{
    internal EconomyUpdatorMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public override void Run()
    {
        base.Run();


        for (int x = 0; x < _eGM.Xamount; x++)
        {
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                if (_eGM.CellEnt_CellBuildingCom(x, y).BuildingType == BuildingTypes.City)
                {
                    if (_eGM.CellEnt_CellBuildingCom(x, y).IsHim(Instance.MasterClient))
                    {
                        _eGM.FoodEnt_AmountDictCom.AmountDict[true] += Instance.StartValuesGameConfig.BENEFIT_FOOD_CITY;
                        _eGM.WoodEAmountDictC.AmountDict[true] += Instance.StartValuesGameConfig.BENEFIT_WOOD_CITY;
                    }
                    else
                    {
                        _eGM.FoodEnt_AmountDictCom.AmountDict[false] += Instance.StartValuesGameConfig.BENEFIT_FOOD_CITY;
                        _eGM.WoodEAmountDictC.AmountDict[false] += Instance.StartValuesGameConfig.BENEFIT_WOOD_CITY;
                    }
                }              
            }
        }


        _eGM.FoodEnt_AmountDictCom.AmountDict[true] += (int)(_eGM.InfoEnt_BuildingsInfoCom.AmountFarmDict[true] * (Instance.StartValuesGameConfig.BENEFIT_FOOD_FARM + (0.25f * _eGM.InfoEnt_UpgradeCom.AmountUpgradeFarmDict[true])));
        _eGM.FoodEnt_AmountDictCom.AmountDict[false] += (int)(_eGM.InfoEnt_BuildingsInfoCom.AmountFarmDict[false] * (Instance.StartValuesGameConfig.BENEFIT_FOOD_FARM + (0.25f * _eGM.InfoEnt_UpgradeCom.AmountUpgradeFarmDict[false])));

        _eGM.WoodEAmountDictC.AmountDict[true] += (int)(_eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[true] * (Instance.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER + (0.25f * _eGM.InfoEnt_UpgradeCom.AmountUpgradeWoodcutterDict[true])));
        _eGM.WoodEAmountDictC.AmountDict[false] += (int)(_eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[false] * (Instance.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER + (0.25f * _eGM.InfoEnt_UpgradeCom.AmountUpgradeWoodcutterDict[false])));

        _eGM.OreEAmountDictC.AmountDict[true] += (int)(_eGM.InfoEnt_BuildingsInfoCom.AmountMineDict[true] * (Instance.StartValuesGameConfig.BENEFIT_ORE_MINE + (0.25f * _eGM.InfoEnt_UpgradeCom.AmountUpgradeMineDict[true])));
        _eGM.OreEAmountDictC.AmountDict[false] += (int)(_eGM.InfoEnt_BuildingsInfoCom.AmountMineDict[false] * (Instance.StartValuesGameConfig.BENEFIT_ORE_MINE + (0.25f * _eGM.InfoEnt_UpgradeCom.AmountUpgradeMineDict[false])));

    }
}
