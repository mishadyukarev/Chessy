using static MainGame;

internal struct BuildingsInfoComponent
{
    internal bool IsBuildedCityMaster;
    internal bool IsBuildedCityOther;
    internal int[] XYsettedCityMaster;
    internal int[] XYsettedCityOther;

    internal int AmountFarmMaster;
    internal int AmountFarmOther;

    internal int AmountWoodcutterMaster;
    internal int AmountWoodcutterOther;

    internal int AmountMineMaster;
    internal int AmountMineOther;

    internal BuildingsInfoComponent(StartValuesGameConfig nameValueManager)
    {
        IsBuildedCityMaster = default;
        IsBuildedCityOther = default;
        XYsettedCityMaster = new int[nameValueManager.XY_FOR_ARRAY];
        XYsettedCityOther = new int[nameValueManager.XY_FOR_ARRAY];

        AmountFarmMaster = 0;
        AmountFarmOther = 0;

        AmountWoodcutterMaster = 0;
        AmountWoodcutterOther = 0;

        AmountMineMaster = 0;
        AmountMineOther = 0;
    }

    internal bool IsSettedCurrentCity
    {
        get
        {
            if (InstanceGame.IsMasterClient) return IsBuildedCityMaster;
            else return IsBuildedCityOther;
        }
        set
        {
            if (InstanceGame.IsMasterClient) IsBuildedCityMaster = value;
            else IsBuildedCityOther = value;
        }
    }


    internal int[] XYCurrentCity
    {
        get
        {
            if (InstanceGame.IsMasterClient) return XYsettedCityMaster;
            else return XYsettedCityOther;
        }
        set
        {
            if (InstanceGame.IsMasterClient) XYsettedCityMaster = value;
            else XYsettedCityOther = value;
        }
    }

}
